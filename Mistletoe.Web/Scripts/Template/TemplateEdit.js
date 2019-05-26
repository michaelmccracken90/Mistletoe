var auto_redirect = true;

$(function () {
    $('#div_BodyHTML').summernote({
        height: 400
    });

    var bodyHTML = $("#hdn_Body").val();
    $('#div_BodyHTML').summernote('code', bodyHTML);

    $('#myModal').on('hide.bs.modal', function (e) {
        if (auto_redirect)
            window.location.href = hostDomain + "/Campaign/";

        auto_redirect = true;
    })
});

function SaveTemplate() {
    var campaign_id = $("#hdn_Campaign_ID").val();
    var template_id = $("#hdn_Template_ID").val();
    var template_name = $("#tb_Name").val();
    var sender = $("#tb_Sender").val();
    var receivers = $("#tb_Receivers").val();
    var subj = $("#tb_Subject").val();
    var body = escape($('#div_BodyHTML').summernote('code'));
    var forgeryId = $("#forgeryToken").val();

    $('#div_Loading').show();
    $("#btn_Save").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: hostDomain + '/Template/SaveTemplate',
        data: { CampaignID: campaign_id, TemplateID: template_id, TemplateName: template_name, Sender: sender, Receivers: receivers, Subject: subj, Body: body },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            $('#div_Loading').hide();
            $("#btn_Save").removeAttr("disabled");

            if (response.Success === true) {
                $('#div_ModalMessage').text("Template updated successfully!");
            }
            else {
                $('#div_ModalMessage').html(response.ErrorMsg);
                auto_redirect = false;
            }

            $('#myModal').modal();
        },
        error: function (xhr, status, p3, p4) {
            $('#div_Loading').hide();
            $("#btn_Save").removeAttr("disabled");

            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}

function Cancel() {
    window.location.href = hostDomain + "/Campaign/";
}