var hostDomain = document.location.origin + "/Mistletoe.Web";

$(function () {
    var urlArr = document.location.pathname.split('/');

    var headerItems = $("#ul_header li");
    if (headerItems != undefined) {
        headerItems.each(function (idx, li) {
            $(li).removeClass("active")
        });

        headerItems.each(function (idx, li) {
            var item = $(li);
            var item_id = item.attr('id');
            
            if (item_id == 'li_' + urlArr[2])
                item.addClass("active");
        });
    }
});