// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var currentGroupId = null;

let pusherKey = sessionStorage.getItem('pusher')

var pusher = new Pusher(pusherKey, {
    cluster: 'us2',
    encrypted: true
});

var channel = pusher.subscribe('group_chat');
channel.bind('new_group', function (data) {
    reloadGroup();
});

// Create new group button
$("#CreateNewGroupButton").click(function () {
    let UserNames = $("input[name='UserName[]']:checked")
        .map(function () {
            return $(this).val();
        }).get();

    let data = {
        GroupName: $("#GroupName").val(),
        UserNames: UserNames
    };

    $.ajax({
        type: "POST",
        url: "/api/group",
        data: JSON.stringify(data),
        success: (data) => {
            $('#CreateNewGroup').modal('hide');
        },
        dataType: 'json',
        contentType: 'application/json'
    });

});

// When a user clicks on a group, Load messages for that particular group.
    $("#groups").on("click", ".group", function(){
        let group_id = $(this).attr("data-group_id");

        $('.group').css({"border-style": "none", cursor:"pointer"});
        $(this).css({"border-style": "inset", cursor:"default"});

        $("#currentGroup").val(group_id); // update the current group_id to a html form...
        currentGroupId =  group_id;

        // get all messages for the group and populate it...
        $.get( "/api/message/"+group_id, function( data ) {
            let message = "";

            data.forEach(function(data){
                    let position = ( data.addedBy == $("#UserName").val() ) ? " float-right" : "";
                    message += `<div class="row chat_message` + position +`"><b>`+ data.addedBy +`: </b>`+ data.message +` </div>`;
            });

            $(".chat_body").html(message);
        });
        if( !pusher.channel('private-'+group_id) ){ // check the user have subscribed to the channel before.
            let group_channel = pusher.subscribe('private-'+group_id);

            group_channel.bind('new_message', function(data) { 
                 if( currentGroupId == data.new_message.GroupId){

                      $(".chat_body").append(`<div class="row chat_message"><b>`+ data.new_message.AddedBy +`: </b>`+ data.new_message.message +` </div>`);
                 }
              });  
        }
    });

// Add functionality to send message button
$("#SendMessage").click(function () {
    $.ajax({
        type: "POST",
        url: "/api/message",
        data: JSON.stringify({
            AddedBy: $("#UserName").val(),
            GroupId: parseInt(currentGroupId),//$("#currentGroup").val(),
            message: $("#Message").val(),
            SocketId: pusher.connection.socket_id
        }),
        success: (data) => {
            $(".chat_body").append(`<div class="row chat_message float-right"><b>`
                + data.data.addedBy + `: </b>` + $("#Message").val() + `</div>`
            );

            $("#Message").val('');
        },
        dataType: 'json',
        contentType: 'application/json'
    });
});

function reloadGroup() {
    $.get("/api/group", function (data) {
        let groups = "";

        data.forEach(function (group) {
            groups += `<div class="group" data-group_id="`
                + group.groupId + `">` + group.groupName +
                `</div>`;
        });

        $("#groups").html(groups);
    });
}