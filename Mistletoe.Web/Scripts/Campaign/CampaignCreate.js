var auto_redirect = true;

$(function () {
    $('.div_Frequency').jqCron(
        {
            no_reset_button: false,
            lang: 'en',
            bind_to: $('.span_Frequency'),
            bind_method: {
                set: function ($element, value) {
                    $element.html(value);
                }
            }
        }
    );

    $("#tb_Start_Date").datepicker({
        minDate: 0,
        maxDate: "+10Y",
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd.mm.yy"
    });

    $("#tb_End_Date").datepicker({
        minDate: 0,
        maxDate: "+10Y",
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd.mm.yy"
    });


    //$("#tb_Start_Date").datepicker({
    //    showOn: "button",
    //    buttonImage: "../../images/calendar.gif",
    //    buttonImageOnly: true,
    //    buttonText: "Select date"
    //});


    $('#txtUploadFile').on('change', function (e) {
        var campaign_id = "-1";

        var files = e.target.files;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                $("#div_UploadingStatus").html("Uploading file...");
                $('#div_Loading').show();
                $("#btn_Save").attr("disabled", true);

                var forgeryId = $("#forgeryToken").val();

                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }
                
                $.ajax({
                    type: "POST",
                    url: hostDomain + '/Campaign/UploadFile?CampaignID=' + campaign_id,
                    contentType: false,
                    processData: false,
                    data: data,
                    headers: { 'VerificationToken': forgeryId },
                    success: function (response) {
                        $('#div_Loading').hide();
                        $("#btn_Save").removeAttr("disabled");

                        if (response.Success === true) {
                            $("#div_UploadingStatus").html("File Uploaded.");
                            $('#div_UploadingStatus').delay(5000).fadeOut('slow');

                            $("#hdn_UploadFilePath").val(response.FilePath);
                        }
                        else {
                            alert(response.ErrorMsg);
                        }
                    },
                    error: function (xhr, status, p3, p4) {
                        $('#div_Loading').hide();
                        $("#btn_Save").removeAttr("disabled");

                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] === "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                    }
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!");
            }
        }
    });

    $('#myModal').on('hide.bs.modal', function (e) {
        if (auto_redirect === true)
            window.location.href = hostDomain + "/Campaign/";

        auto_redirect = true;
    })
});

function CreateCampaign() {
    var campaign_id = "-1";
    var campaign_name = $("#tb_Name").val();
    var start_date = $("#tb_Start_Date").val();
    var end_date = $("#tb_End_Date").val();
    var freq = $('.span_Frequency').text();
    var uploadFilePath = $("#hdn_UploadFilePath").val();
    var forgeryId = $("#forgeryToken").val();

    $('#div_Loading').show();
    $("#btn_Save").attr("disabled", true);

    $.ajax({
        type: "POST",
        url: hostDomain + '/Campaign/SaveCampaign',
        data: { CampaignID: campaign_id, CampaignName: campaign_name, StartDate: start_date, EndDate: end_date, Frequency: freq, DataFilePath: uploadFilePath },
        headers: { 'VerificationToken': forgeryId },
        success: function (response) {
            $('#div_Loading').hide();
            $("#btn_Save").removeAttr("disabled");
            
            if (response.Success === true) {
                $('#div_ModalMessage').text("Campaign created successfully!");
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
            if (xhr.responseText && xhr.responseText[0] === "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}

function Cancel() {
    window.location.href = hostDomain + "/Campaign/";
}