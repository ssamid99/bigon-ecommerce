function chatBox() {

    this.context = {
        inbox: [
            //{
            //    friendId: 0,
            //    friendName: "Demo",
            //    lastMessage: "bye",
            //    unread: 3,
            //    date: '2023-07-13 04:00'
            //},
        ],
    };


    this.receiveMessage= (inboxMessage)=>{

        let anotherMessages = context.inbox.filter(i => i.friendId != inboxMessage.friendId);

        anotherMessages.shift(inboxMessage);
        this.context.inbox = anotherMessages;

        this.reloadMessage();
    }

    this.reloadMessage= ()=>{

        let messageList = document.querySelector('#messageList');

        if (messageList == undefined) return;

        messageList.innerHTML('');

        this.context.inbox.forEach(item => {
            let div = document.createElement('div');
            div.className = 'media userlist-box';
            div.setAttribute = ("data-id", item.friendId);
            div.setAttribute = ("title", item.friendName);

            let a = document.createElement('a');
            a.className = 'media-left';
            a.innerHtml = `<img class="media-object" src="../files/assets/images/avatar-1.jpg"
                                 alt="Generic placeholder image" />
                            <div class="live-status bg-success"></div>`;

            div.appendChild(a);

            let divMediaBody = document.createElement('div');
            divMediaBody.className = 'media-body';
            divMediaBody.innerHTML = `<div class="f-13 chat-header">${item.friendName}</div>`;

            div.appendChild(divMediaBody);
            messageList.appendChild(div);
        });
    }
}


