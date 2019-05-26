var User_ID;

$(function () {
    $('#modal_general').on('hide.bs.modal', function (e) {
        window.location.href = hostDomain + "/Admin/";
    })
});

function ChangeUserStatus(user_id, new_status) {
    $('#div_Loading').show();
    
    var forgeryId = $("#forgeryToken").val();

    $.ajax({
        type: "POST",
        url: hostDomain + '/Admin/ChangeUserStatus',
        data: { UserID: user_id, IsActive: new_status },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            $('#div_Loading').hide();

            if (response.Success === true) {
                if (new_status)
                    $('#modal_general_message').text("User activated successfully!");
                else
                    $('#modal_general_message').text("User deactivated successfully!");
            }
            else {
                $('#modal_general_message').text(response.ErrorMsg);
            }

            $('#modal_general').modal();
        },
        error: function (xhr, status, p3, p4) {
            $('#div_Loading').hide();
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}

function DeactivateUser(user_id) {
    $('#div_Loading').show();
    
    var forgeryId = $("#forgeryToken").val();

    // First, check for active campaigns
    $.ajax({
        type: "POST",
        url: hostDomain + '/Admin/GetActiveCampaigns',
        data: { UserID: user_id },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            $('#div_Loading').hide();

            if (response.Success === true) {
                if (response.CampaignCount > 0) {
                    User_ID = user_id;
                    $('#modal_decision_message').text("The user has active campaigns. Do you wish to continue?");
                    $('#modal_decision').modal();
                }
                else {
                    ChangeUserStatus(user_id, false);
                }
            }
            else {
                $('#modal_general_message').text(response.ErrorMsg);
                $('#modal_general').modal();
            }
        },
        error: function (xhr, status, p3, p4) {
            $('#div_Loading').hide();
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}

function ConfirmDeactivateUser() {
    ChangeUserStatus(User_ID, false);
}

function SaveFooter() {
    var new_footer = $('#tb_Footer').val();
    $('#div_Loading').show();
    $('#btn_SaveFooter').hide();
    
    var forgeryId = $("#forgeryToken").val();

    $.ajax({
        type: "POST",
        url: hostDomain + '/Admin/SaveFooter',
        data: { NewFooter: new_footer },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            $('#div_Loading').hide();
            $('#btn_SaveFooter').show();

            if (response.Success === true) {
                $('#modal_general_message').text("Footer saved successfully!");
            }
            else {
                $('#modal_general_message').text(response.ErrorMsg);
            }

            $('#modal_general').modal();
        },
        error: function (xhr, status, p3, p4) {
            $('#div_Loading').hide();
            $('#btn_SaveFooter').show();

            $('#modal_general_message').text("An unexpected error occured!");
            $('#modal_general').modal();

            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}