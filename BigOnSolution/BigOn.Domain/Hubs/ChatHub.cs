using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.Business.ChatModel;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator mediator;
        static ConcurrentDictionary<int, string> clients = new ConcurrentDictionary<int, string>();

        public ChatHub(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var httpContext = Context.GetHttpContext();
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userId = httpContext.User.GetCurrentUserId();
                var command = new HubUserConnectCommand
                {
                    HubContext = Context,
                    HubGroups = Groups
                };
                await mediator.Send(command);
                clients.AddOrUpdate(userId, Context.ConnectionId, (k, v) => v);
            }
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);

        }

        public async Task SendFromClient(string message)
        {
            var command = new SendToGroupCommand
            {
                HubContext = Context,
                GroupName = "CallCenter",
                Text = message
            };
            var response = await mediator.Send(command);
            var httpcontext = Context.GetHttpContext();
            var userId = httpcontext.User.GetCurrentUserId();
            //save to db
            await Clients.GroupExcept(command.GroupName, Context.ConnectionId).SendAsync("ReceiveFromOperator", userId, command.Text);
        }

        public async Task SendFromOperator(string message, int toUserId)
        {
            var httpcontext = Context.GetHttpContext();
            var userId = httpcontext.User.GetCurrentUserId();
            var command = new SendToUserCommand
            {
                HubContext = Context,
                UserId = toUserId,
                Text = message
            };
            var response = await mediator.Send(command);
            if (response != null && clients.TryGetValue(response.ToId.Value, out string toUserConnectionId))
            {
                await Clients.User(toUserConnectionId).SendAsync("ReceiveFromClient", response.ToId.Value, response.Text);
            }
        }
    }
}
