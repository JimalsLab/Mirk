
    $(".btn_dispo").click(function () {
        bootbox.prompt({
            title: "Change your disponibility ?",
            inputType: 'select',
            inputOptions: [
                {
                    text: 'Available',
                    value: '',
                },
                {
                    text: 'Away',
                    value: '1',
                },
                {
                    text: 'Occupied',
                    value: '2',
                },
                {
                    text: 'Offline',
                    value: '3',
                }
            ],
            callback: function (result) {
                switch(result) {
                    case "1":
                        $('.btn_dispo').css("background-color", "#ffcc66");
                        break;
                    case "2":
                        $('.btn_dispo').css("background-color", "#af473c");
                        break;
                    case "3":
                        $('.btn_dispo').css("background-color","DarkGrey");
                        break;
                    default:
                        $('.btn_dispo').css("background-color", "#418b7a");
                }
                $.ajax({
                    type: "POST",
                    url: "/Main/SetAvailability",
                    dataType: "json",
                    data: { 'availability': result }
                });

            }
        });
    });

    $('#change_nickname').click(function () {
        bootbox.prompt("Choose a nickname", function (result) {
            $.ajax({
                type: "POST",
                url: "/Main/SetNickname",
                dataType: "json",
                data: {
                    'nickname': result,
                    'urll': window.location.href
                }
            });
            setTimeout(function () {
                window.location.reload();
            }, 1300);
        });
    });

    $(".btn_menu_optn_right").click(function () {
        bootbox.confirm({
            message: "<b>Account Options</b><p>Do you want to disconnect ?</p>",
            buttons: {
                confirm: {
                    label: 'Logout',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'Cancel',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result === true) {
                    $.ajax({
                        type: "POST",
                        url: "/Home/disconnect",
                    });
                    setTimeout(function () {
                        window.location.href = '/Home';
                    }, 1000);
                    
                }
            }
        });
    });

    function reload() {
        location.reload();
    }   

    function toggleFG() {
            $(".friends_btn").toggleClass("active");
            $(".groups_btn").toggleClass("active");
            $(".allfriends").toggle();
            $(".allgroups").toggle();
    }

    $("#grp").load(function () {
        alert(URL.toString());
    });

    $(".sendfeeback").click(function () {
        bootbox.alert({
            message: "Thanks for the feedback :)",
            size: 'small',
            backdrop: true
        });
    });