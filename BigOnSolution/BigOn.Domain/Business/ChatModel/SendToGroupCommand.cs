using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities.Chat;
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
    public class SendToGroupCommand : IRequest<Message>
    {
        public HubCallerContext HubContext { get; set; }
        public string GroupName { get; set; }
        public string Text { get; set; }
        public class SendToGroupCommandHandler : IRequestHandler<SendToGroupCommand, Message>
        {
            private readonly BigOnDbContext db;
            private readonly IActionContextAccessor ctx;

            public SendToGroupCommandHandler(BigOnDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Message> Handle(SendToGroupCommand request, CancellationToken cancellationToken)
            {
                var httpContext = request.HubContext.GetHttpContext();
                
                var group = await db.Groups.FirstOrDefaultAsync(g => g.Name.Equals(request.GroupName) && g.DeletedDate == null, cancellationToken);

                var message = new Message();
                message.FromId = httpContext.User.GetCurrentUserId();
                message.GroupId = group.Id;
                message.Text = request.Text;

                await db.Messages.AddAsync(message, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return message;
            }
        }
    }
}
