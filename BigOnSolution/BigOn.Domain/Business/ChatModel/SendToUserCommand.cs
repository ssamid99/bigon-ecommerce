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
    public class SendToUserCommand : IRequest<Message>
    {
        public HubCallerContext HubContext { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public class SendToUserCommandHandler : IRequestHandler<SendToUserCommand, Message>
        {
            private readonly BigOnDbContext db;
            private readonly IActionContextAccessor ctx;

            public SendToUserCommandHandler(BigOnDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Message> Handle(SendToUserCommand request, CancellationToken cancellationToken)
            {
                var httpContext = request.HubContext.GetHttpContext();

                if (httpContext.User.Identity.IsAuthenticated)
                {
                    var message = new Message();
                    message.FromId = httpContext.User.GetCurrentUserId();
                    message.ToId = request.UserId;
                    message.Text = request.Text;

                    await db.Messages.AddAsync(message, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);
                    return message;
                }
                return null;
            }
        }
    }
}
