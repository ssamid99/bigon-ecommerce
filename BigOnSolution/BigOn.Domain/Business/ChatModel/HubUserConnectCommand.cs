using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.Models.DataContents;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigOn.Domain.Business.ChatModel
{
    public class HubUserConnectCommand : IRequest<bool>
    {
        public HubCallerContext HubContext { get; set; }
        public IGroupManager HubGroups { get; set; }
        public class HubUserConnectCommandHandler : IRequestHandler<HubUserConnectCommand, bool>
        {
            private readonly BigOnDbContext db;
            private readonly IActionContextAccessor ctx;

            public HubUserConnectCommandHandler(BigOnDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<bool> Handle(HubUserConnectCommand request, CancellationToken cancellationToken)
            {
                var httpContext = request.HubContext.GetHttpContext();
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    return false;
                }
                var userConnectionId = request.HubContext.ConnectionId;

                var userId = httpContext.User.GetCurrentUserId();

                var userGroups = await db.UserGroups
                     .Include(ug=>ug.Group)
                     .Where(up=>up.UserId == userId)
                     .ToListAsync(cancellationToken);

                foreach(var item in userGroups)
                {
                    await request.HubGroups.AddToGroupAsync(userConnectionId, item.Group.Name, cancellationToken);
                }
                return true;
            }
        }
    }
}
