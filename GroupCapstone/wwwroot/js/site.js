// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var currentGroupId = null;

let pusherKey = sessionStorage.getItem('pusher');

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

$("#APISearchButton").click(function(event) {
    var searchLocation = document.forms.yelpForm["location"].value;
    var searchType = document.forms.yelpForm["type"].value;

    //Yelp api call
    $.ajax({
        url: `https://localhost:44334/Home/GetBusinesses?location=${searchLocation}&type=${searchType}`,
        type: 'GET',
        dataType: 'json',
        success: function (data, textStatus, jQqhr) {

            console.log(data);
            
            // Clear results table
            $("#apiResults").empty();

            // Load table skeleton
            $("#apiResults").html(
                `<tr>
                    <th style="width: 200px; text-align: center;">Result</th>
                    <th style="width: 200px; text-align: center;">Location</th>
                    <th style="width: 75px; text-align: center;">Rating</th>
                    <th style="width: 75px; text-align: center;">Price</th>
                    <th style="text-align: center;">Action Buttons</th>
                </tr>`
            );

            // Load API Results
            $.each(data.businesses, function (key, value) {
                $("#apiResults").append(
                    `<tr style="border-top: 1px solid black; margin-top: 10px">
                        <td>${value.name}</td>
                        <td>${value.location.address1}</td>
                        <td>${value.rating}</td>
                        <td>${value.price}</td>
                        <td>
                            <button type="button" class="btn btn-secondary btn-sm" value="">Share to group</button>
                            <button type="button" class="btn btn-secondary btn-sm" data-toggle="modal" data-target="#ViewDetails">View Details</button>
                        </td>
                    </tr>`
                )
            })
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        },
    });
    event.preventDefault();
})