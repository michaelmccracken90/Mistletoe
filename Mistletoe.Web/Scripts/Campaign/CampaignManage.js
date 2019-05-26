var Campaign_ID = 0;

$(function () {
    var arrCampaignIDs = $("#hdn_CampaignIDs").val().split(',');
    
    for (var i = 0; i < arrCampaignIDs.length; i++) {
        var tempDivID = ".div_Frequency_" + arrCampaignIDs[i];
        var tempFreqID = "#hdn_Frequency_" + arrCampaignIDs[i];

        var freqValue = $(tempFreqID).val();

        var cron = $(tempDivID).jqCron(
            {
                default_value: freqValue,
                lang: 'en'
            }
        ).jqCronGetInstance();

        cron.disable();
    }

    $('#myModal').on('hide.bs.modal', function (e) {
        window.location.href = hostDomain + "/Campaign/";
    })
});

function ActivateCampaign(campaign_id) {
    var forgeryId = $("#forgeryToken").val();

    $.ajax({
        type: "POST",
        url: hostDomain + '/Campaign/ChangeCampaignStatus',
        data: { CampaignID: campaign_id, IsActive: true, ScheduleCampaign: true },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            if (response.Success === true) {
                $('#div_ModalMessage').text("Campaign activated successfully!");
            }
            else {
                $('#div_ModalMessage').text(response.ErrorMsg);
            }

            $('#myModal').modal();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}

function DeactivateCampaign(campaign_id) {
    var forgeryId = $("#forgeryToken").val();

    $.ajax({
        type: "POST",
        url: hostDomain + '/Campaign/ChangeCampaignStatus',
        data: { CampaignID: campaign_id, IsActive: false, ScheduleCampaign: true },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            if (response.Success === true) {
                $('#div_ModalMessage').text("Campaign deactivated successfully!");
            }
            else {
                $('#div_ModalMessage').text(response.ErrorMsg);
            }

            $('#myModal').modal();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}

function DeleteCampaign(campaign_id) {
    var forgeryId = $("#forgeryToken").val();

    $.ajax({
        type: "POST",
        url: hostDomain + '/Campaign/DeleteCampaign',
        data: { CampaignID: campaign_id },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            if (response.Success === true) {
                $('#div_ModalMessage').text("Campaign deleted successfully!");
            }
            else {
                $('#div_ModalMessage').text(response.ErrorMsg);
            }

            $('#myModal').modal();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}

function PreviewEmail(campaign_id) {
    Campaign_ID = campaign_id;
    var forgeryId = $("#forgeryToken").val();

    $.ajax({
        type: "POST",
        url: hostDomain + '/Campaign/PreviewEmail',
        data: { CampaignID: campaign_id },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            if (response.Success === true) {
                var templateObj = response.Template;

                $('#span_Preview_Sender').html(templateObj.Sender);
                $('#span_Preview_Receivers').html(templateObj.Receivers);
                $('#span_Preview_Subject').html(templateObj.Subject);
                $('#span_Preview_Body').html(templateObj.Body);

                $('#preview_Modal').modal();
            }
            else {
                $('#div_ModalMessage').text(response.ErrorMsg);
                $('#myModal').modal();
            }
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}

function SendEmail() {
    $('#div_Loading').show();
    var forgeryId = $("#forgeryToken").val();

    $.ajax({
        type: "POST",
        url: hostDomain + '/Campaign/SendTestEmail',
        data: { CampaignID: Campaign_ID },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            if (response.Success === true) {
                $('#div_ModalMessage').text("Email sent successfully!");
            }
            else {
                $('#div_ModalMessage').text(response.ErrorMsg);
            }

            $('#div_Loading').hide();
            $('#myModal').modal();
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