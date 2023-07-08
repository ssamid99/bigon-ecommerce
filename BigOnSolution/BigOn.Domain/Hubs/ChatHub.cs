using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.Business.ChatModel;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator mediator;

        public ChatHub(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var command = new HubUserConnectCommand
            {
                HubContext = Context,
                HubGroups = Groups
            };
            await mediator.Send(command);
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
            await Clients.Group(command.GroupName).SendAsync("ReceiveFromOperator", userId, command.Text);
        }

        public async Task SendFromOperator(string message)
        {
            var httpcontext = Context.GetHttpContext();
            var userId = httpcontext.User.GetCurrentUserId();
            //save to db
            await Clients.All.SendAsync("ReceiveFromClient", userId, message);
        }
    }
}
