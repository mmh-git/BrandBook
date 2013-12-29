var loggedIn = false;

$(document).ready(function () {
    $('#tabs').tab();
    $.ajax({
        url: baseUrl + "Account/IsLogedIn", type: 'GET', async: false, success: function (result) {
            if (result.loggedIn) {
                //window.ShowLoading(true);
                //var statUrl = baseUrl + "User/Index";
                ////$("#divMain").css("height", mainHeight);
                ////$("#content").css("height", mainHeight - 4);
                //$.ajax({
                //    url: statUrl, type: 'GET', async: false, success: function (result) {
                //        $("#StatusMain").html(result);
                //        window.ShowLoading(false);
                //    }
                //});
                //$("#divMain").css('display', 'block');

                loadUser();

                //$("<a id='aLogout' href='#'>Log Out</>").appendTo("#divLogOut");
                //$("#aLogout").on("click", function () {

                //    $.ajax({
                //        url: baseUrl + "Account/LogOff",
                //        type: 'POST',
                //        success: function (data) {
                //            if (data == "success") {
                //                $("#divMain").slideUp("slow", function () {
                //                    $("#loginContent").fadeIn({ duration: 500 });
                //                    $("#divLogOut").fadeOut({ duration: 500 }).empty();
                //                    clearInterval(setIntervalId);
                //                });

                //            }
                //        }
                //    });

                //});

                //$("#divLogOut").css('display', 'block');
            }
        }
    });
});





function loadUser() {
    var statUrl = baseUrl + "User/Index";
    //$("#divMain").css("height", mainHeight);
    //$("#content").css("height", mainHeight - 4);
    window.ShowLoading(true);
    $("#loginContent").fadeOut(function () {
        $.ajax({
            url: statUrl, type: 'GET', async: true, success: function (result) {
                $("#StatusMain").html(result);
                window.ShowLoading(false);
            }
        });
        $("#divMain").slideDown({ duration: 500 });
    });

    $("<a id='aLogout' href='#'>Log Out</>").appendTo("#divLogOut");
    $("#aLogout").on("click", function () {
        $.ajax({
            url: baseUrl + "Account/LogOff",
            type: 'POST',
            success: function (data) {
                if (data == "success") {
                    $("#divMain").slideUp("slow", function () {
                        $("#loginContent").fadeIn({ duration: 500 });
                        $("#divLogOut").fadeOut({ duration: 500 }).empty();
                        clearInterval(setIntervalId);
                    });

                }
            }
        })
    });

    $("#divLogOut").fadeIn({ duration: 500 });

    //$("#loginContent").animate({ height: 0, duration: 600 });

    //loginContent
}

window.ShowLoading = function (block)
{
    if (block) {
        $.blockUI({

            message: "<img src='" + baseUrl + "Content/themes/base/images/wait.gif' />",
            css: {
                top: ($(window).height() * 50 / 100) + 'px',
                left: ($(window).width() - 32) / 2 + 'px',
                width: '40px',
                height: '40px'
            }
        });
    }
    else
    {
        $.unblockUI();
    }
}