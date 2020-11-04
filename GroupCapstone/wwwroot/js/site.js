// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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

    $("#currentGroup").val(group_id); // update the current group_id to html file...
    currentGroupId = group_id;

    // get all messages for the group and populate it...
    $.get("/api/message/" + group_id, function (data) {
        let message = "";

        data.forEach(function (data) {
            let position = (data.addedBy == $("#UserName").val()) ? " float-right" : "";

            message += `<div class="row chat_message` + position + `">
                             <b>` + data.addedBy + `: </b>` + data.message +
                `</div>`;
        });

        $(".chat_body").html(message);
    });

});