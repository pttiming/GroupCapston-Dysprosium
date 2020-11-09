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
$("#groups").on("click", ".group", function () {
    let group_id = $(this).attr("data-group_id");

    $('.group').css({ "border-style": "none", cursor: "pointer" });
    $(this).css({ "border-style": "inset", cursor: "default" });

    $("#currentGroup").val(group_id); // update the current group_id to a html form...
    currentGroupId = group_id;

    // get all messages for the group and populate it...
    $.get("/api/message/" + group_id, function (data) {
        let message = "";

        data.forEach(function (data) {
            let position = (data.addedBy == $("#UserName").val()) ? " float-right" : "";
            message += `<div class="row chat_message` + position + `"><b>` + data.addedBy + `: </b>` + data.message + ` </div>`;
        });

        $(".chat_body").html(message);
    });
    if (!pusher.channel('private-' + group_id)) { // check the user have subscribed to the channel before.
        let group_channel = pusher.subscribe('private-' + group_id);

        group_channel.bind('new_message', function (data) {
            if (currentGroupId == data.new_message.GroupId) {

                $(".chat_body").append(`<div class="row chat_message"><b>` + data.new_message.AddedBy + `: </b>` + data.new_message.message + ` </div>`);
            }
        });
    }
});

// Add functionality to send message button
$("#SendMessage").click(function () {
    if (currentGroupId == null) {
        alert("Please select a Group to send the message to!")
    }
    else {
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
    }
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

$("#APISearchButton").click(function (event) {
    var searchLocation = document.forms.yelpForm["location"].value;
    var searchType = document.forms.yelpForm["type"].value;

    //Yelp api call
    $.ajax({
        url: `https://localhost:44334/Home/GetBusinesses?location=${searchLocation}&type=${searchType}`,
        type: 'GET',
        dataType: 'json',
        success: function (data, textStatus, jQqhr) {

            // Clear results table
            $("#apiResults").empty();

            // Load table skeleton
            $("#apiResults").html(
                `<tr>
                    <th style="width: 200px; text-align: center;">Result</th>
                    <th style="width: 200px; text-align: center;">Location</th>
                    <th style="width: 75px; text-align: center;">Rating</th>
                    <th style="width: 75px; text-align: center;">Price</th>
                    <th></th>
                </tr>`
            );

            // Load API Results
            $.each(data.businesses, function (key, value) {
                $("#apiResults").append(
                    `<tr style="border-top: 1px solid black; margin-top: 10px">
                        <td>${value.name}</td>
                        <td>${value.location.address1}</td>
                        <td style="text-align: center;">${value.rating}</td>
                        <td style="text-align: center;">${value.price}</td>
                        <td style="text-align: center;">
                            <button type="button" class="btn btn-secondary btn-sm"id="ShareToGroup" onclick="shareToGroup('${value.url}')">Share To Group</button>
                            <button type="button" class="btn btn-secondary btn-sm" data-toggle="modal" data-target="#SingleBusinessDetails" onclick="yelpSingleBusiness('${value.id}', event)">View Details</button>
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

function yelpSingleBusiness(businessId, event) {

    //Yelp api call
    $.ajax({
        url: `https://localhost:44334/Home/GetBusiness?businessId=${businessId}`,
        type: 'GET',
        dataType: 'json',
        success: function (data, textStatus, jQqhr) {
            console.log(data);
            $("#SingleBusinessDetails .modal-dialog .modal-content .modal-header .modal-title").html(
                `${data.name}`
            );
            $("#SingleBusinessDetails .modal-dialog .modal-content .modal-body").html(
                `<div class="row">
                    <div class="col-sm-5">
                        <image src="${data.image_url}" alt="${data.name}" style="width: 100%"></image>
                    </div>
                    <div class="col-sm-7">
                        <table>
                            <tr valign="top">
                                <td style="width: 100px;">Address:</td>
                                <td>
                                    ${data.location.display_address[0]}<br>
                                        ${data.location.display_address[1]}
                                    </td>
                                </tr>
                                <tr>
                                    <td>Phone:</td>
                                    <td>${data.display_phone}</td>
                                </tr>
                                <tr>
                                    <td>Price:</td>
                                    <td>${data.price}</td>
                                </tr>
                                <tr>
                                    <td># Ratings:</td>
                                    <td>${data.review_count}</td>
                                </tr>
                                <tr>
                                    <td>Avg. Rating:</td>
                                    <td>${data.rating}</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>`
            )
            $("#SingleBusinessDetails .modal-dialog .modal-content .modal-footer").html(
                `<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary" id="ShareToGroup" onclick="shareToGroup('${data.url}')">Share To Group</button>`
            )
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(errorThrown);
        },
    });
    event.preventDefault();
}

function shareToGroup(url) {
    if (currentGroupId == null) {
        alert("Please select a Group to send the message to!")
    }
    else {
        $.ajax({
            type: "POST",
            url: "/api/message",
            data: JSON.stringify({
                AddedBy: $("#UserName").val(),
                GroupId: parseInt(currentGroupId),//$("#currentGroup").val(),
                message: `<a href="${url}">Check out this restaurant I found on Yelp!</a>`,
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
    }
}

// Create new group button DevCC Version
$("#CreateGroupButton").click(function () {
    let UserNames = $("#CurrentUserName")
        .map(function () {
            return $(this).val();
        }).get();
    let gn = $("#GroupName").val();
    let data = {
        GroupName: $("#GroupName").val(),
        UserNames: UserNames
    };

    $.ajax({
        type: "POST",
        url: "/api/group",
        data: JSON.stringify(data),
        success: (data) => {
            $('#CreateGroup').modal('hide');
            $('#MyGroups').append(
                `<tr>
                    <td>
                        ${gn}
                    </td>
                    
                </tr>`
            );
        },
        dataType: 'json',
        contentType: 'application/json'
    });

});

