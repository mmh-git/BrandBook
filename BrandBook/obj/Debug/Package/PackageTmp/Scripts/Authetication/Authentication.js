var setIntervalId;
$(document).ready(function () {
   
    var screenHeiht = $(document).height();
    //$(document).height();
    var mainHeight = screenHeiht - 58;
    $("#btnLogin").on("click", function () {
        var regUrl = baseUrl + "Account/Login";
       
        var serialiedData = $("#divLogin :input").serialize();
        $.validator.unobtrusive.parse("#frmLogin");
        if ($("#frmLogin").valid())
        {
            $.ajax({
                url: regUrl,
                type: 'POST',
                data: serialiedData,
                async: false,
                success: function (data) {
                    if (data == "done") {
                        if (!$('.loginErrorMsg').hasClass('hide')) {
                            $('.loginErrorMsg').addClass('hide')
                        }
                        setIntervalId = setInterval(function () {
                            $.ajax({
                                url: baseUrl + "Account/IsLogedIn", type: 'GET', async: true, success: function (result) {
                                    if (!result.loggedIn && loggedIn) {
                                        clearInterval(setIntervalId);
                                        $(".LogOutModalWrapper").css('display', 'block');
                                        $(".logOutMessageModal").slideDown({ duration: 1000 })
                                        $(".btn-submit").on('click', function () {
                                            $(".logOutMessageModal").slideUp('slow', function () {
                                                $(".LogOutModalWrapper").css('display', 'none');
                                                $("#divMain").slideUp("slow", function () {
                                                    $("#loginContent").fadeIn({ duration: 500 });
                                                    $("#divLogOut").fadeOut({ duration: 500 }).empty();
                                                    loggedIn = false;
                                                });

                                            })
                                        });

                                    }
                                    else {
                                        loggedIn = true;
                                    }
                                }
                            });
                        }, 10000);
                        loadUser();
                        loggedIn = true;
                    }
                    else {
                        $('.loginErrorMsg').empty().text("* Username or password is incorrect.");
                        if ($('.loginErrorMsg').hasClass('hide')) {
                            $('.loginErrorMsg').removeClass('hide')
                        }
                    }
                },
                error: function () {
                    $('.loginErrorMsg').empty().text("* Network problem. Please Try again");
                    if ($('.loginErrorMsg').hasClass('hide')) {
                        $('.loginErrorMsg').removeClass('hide')
                    }
                }
            });
        }
    })
});