
$( document ).ready(function () {

    chat = $.connection.chatHub;

    chat.client.addNewMessageToPage = function (name, message, chatName, senderId) {
        console.log(chatName);
        console.log(friendId.textContent);
        console.log(currUserId.textContent);
        
        if (message !== "" &&
            (((friendId.textContent === chatName || friendId.textContent === senderId) && (currUserId.textContent === chatName || currUserId.textContent === senderId))) ||
            groupId.textContent === chatName) {
            $.ajax({
                type: "POST",
                url: "/Main/TreatMessage",
                dataType: "json",
                data: {
                    'uname': name,
                    'msg': message,
                    'sender': senderId,
                    'group': chatName,
                    'urll': window.location.href
                },
                success: function (txt) {
                    if (name === $('.menu_pseudo').text()) {
                        $('#Conversation').append('<div class="text_spacing keepspaces"><strong class="msg_sender">' + htmlEncode(name) + '</strong>  ' + txt + '</div>');
                    }
                    else {
                        $('#Conversation').append('<div class="text_spacing keepspaces"><strong class="msg_others">' + htmlEncode(name) + '</strong>  ' + txt + '</div>');
                    }
                    updateScroll();
                }
            });

        }
    };

    $('#message').focus();

    $.connection.hub.start().done(function () {
        $('#sendMessage').click(function () {
            chat.server.send(currUserName.textContent, mainimputbox.value, friendId.textContent !== "0" ? friendId.textContent : groupId.textContent, currUserId.textContent);
            $('#mainimputbox').val('').focus();
        });
    });
});


function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

$("#mainimputbox").keydown(function (e) {
    // Enter was pressed without shift key
    if (e.keyCode === 13 && !e.shiftKey) {
        // prevent default behavior
        e.preventDefault();
        $('#sendMessage').trigger("click");
    }
});

function updateScroll() {
    var element = document.getElementById("Conversation");
    element.scrollTop = element.scrollHeight;
}