var loggedIn = false;

$(document).ready(function () {
    $('#tabs').tab();
    $.ajax({
        url: baseUrl + "Account/IsLogedIn", type: 'GET', async: false, success: function (result) {
            if (result.loggedIn) {
                var statUrl = baseUrl + "User/Index";
                //$("#divMain").css("height", mainHeight);
                //$("#content").css("height", mainHeight - 4);
                    $.ajax({
                        url: statUrl, type: 'GET', async: false, success: function (result) {
                            $("#StatusMain").html(result);
                        }
                    });
                    $("#divMain").css('display','block');

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
                    });
                    
                    });

                    $("#divLogOut").css('display','block');
                }
        }
    });
});





function loadUser() {
    var statUrl = baseUrl + "User/Index";
    //$("#divMain").css("height", mainHeight);
    //$("#content").css("height", mainHeight - 4);
    $("#loginContent").fadeOut(function () {
        $.ajax({
            url: statUrl, type: 'GET', async: false, success: function (result) {
                $("#StatusMain").html(result);
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