var loggedIn = false;

$(document).ready(function () {   
        $('#Content').on('click', '.likemsg', function (event) {
            event.preventDefault();
            var context = $(this);
            $('#LikedUserModal').modal('show');
            showLoader("LikedUserModalContent");
            $.ajax({
                url: baseUrl + "Like/GetLikedUserCollection",
                type: 'POST',
                async: false,
                data: {
                    'LikedContentId': context.data('likedcontentid')
                },
                success: function (data) {
                    $('.LikedUserModalBody').html(data);
                },
                complete: function () {
                    hideLoader("LikedUserModalContent");
                }
            });
        });
   
        $('#Content').on('click', '.popupImg', function () {
            $('.popupimagecontainer').attr('src', $(this).attr('src'));
            $('#imageModal').modal('show');
        });

    $(".aProfile").on('click', function (event) {
        event.preventDefault();
        loadProfile(0);
        
    });
    $('#Profile').on('click', '.btn-SaveUserProject', function () {
        var serialiedData = $("#frmEditProjectBrnad").serialize();
        $.validator.unobtrusive.parse("#frmEditProjectBrnad");
        if ($('#frmEditProjectBrnad').valid()) {
            var editUrl = baseUrl + "UserProfile/inserProject";
            showLoader('#editUserProjectModalContent');
            $.ajax({
                url: editUrl,
                type: 'POST',
                data: serialiedData,
                async: false,
                success: function (data) {
                    if (Number(data)>0) {
                        $('.message').html('');
                        var statUrl = baseUrl + "UserProfile/getUserProfile";

                        $.ajax({
                            url: statUrl, async: false, data: { 'userDetailsId': 0 }, success: function (data) {
                                $('.userProfileLeftPanel').html($(data).children('.userProfileLeftPanel').children());
                            }
                        });

                        $('#editUserProjectModal').modal('hide');

                    }
                    else {
                        $('.message').html('Unsuccessful operation');
                    }
                },
                error: function () {

                    $('.message').html('Error Happens');
                },
                complete: function () {
                    hideLoader('#editUserProjectModalContent');
                }
            });
        }
    });
    $('#Profile').on('click', '.btn-SaveUserBrand', function () {
        var serialiedData = $("#frmEditUserBrnad").serialize();
        $.validator.unobtrusive.parse("#frmEditUserBrnad");
        if ($('#frmEditUserBrnad').valid()) {
            var editUrl = baseUrl + "UserProfile/inserBrand";
            showLoader('#editUserBrandModalContent');
            $.ajax({
                url: editUrl,
                type: 'POST',
                data: serialiedData,
                async: false,
                success: function (data) {
                    if (Number(data) > 0) {
                        $('.message').html('');
                        var statUrl = baseUrl + "UserProfile/getUserProfile";

                        $.ajax({
                            url: statUrl, async: false, data: { 'userDetailsId': 0 }, success: function (data) {
                                $('.userProfileLeftPanel').html($(data).children('.userProfileLeftPanel').children());
                            }
                        });
                        
                        $('#editUserBrandModal').modal('hide');

                    }
                    else {
                        $('.message').html('Unsuccessful operation');
                    }
                },
                error: function () {

                    $('.message').html('Error Happens');
                },
                complete: function () {
                    hideLoader('#editUserBrandModalContent');
                }
            });
        }
    });
    $('#Profile').on('click', '.btn-SaveUserInfo', function () {
        var serialiedData = $("#frmEditUserInfo").serialize();
        $.validator.unobtrusive.parse("#frmEditUserInfo");
        if ($('#frmEditUserInfo').valid()) {
            var editUrl = baseUrl + "UserProfile/EditUserProfile";
            showLoader('#editUserInfoModalContent');
            $.ajax({
                url: editUrl,
                type: 'POST',
                data: serialiedData,
                async:false,
                success: function (data) {
                    if (data=="1") {
                        $('.message').html('');
                        var statUrl = baseUrl + "UserProfile/getUserProfile";
                        
                        $.ajax({
                            url: statUrl, async: false, data: { 'userDetailsId': 0 }, success: function (data) {
                                $('.userProfileLeftPanel').html($(data).children('.userProfileLeftPanel').children());
                            }
                        });
                        hideLoader('#editUserInfoModalContent');
                        $('#editUserInfoModal').modal('hide');
                        
                    }
                    else
                    {
                        $('.message').html('Unsuccessful operation');
                    }
                },
                error: function () {

                    $('.message').html('Error Happens');
                },
                complete: function ()
                {
                    
                }
            });
        }
    });

    $("#divMain").on('click', '.aFullName', function (event) {
        event.preventDefault();
        

        //$('.tab-pane').removeClass('active');
        //$('#Profile').addClass('active');
        $('.aProfile').tab('show');
        loadProfile($(this).data('userid'))
    });



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
    var documentMinHeight = ($(document).height() - 92) + 'px';
    //$("#divMain").css("height", mainHeight);
    //$("#content").css("height", mainHeight - 4);
    ShowLoading(true);
    $("#loginContent").fadeOut(function () {
        $.ajax({
            url: statUrl, type: 'GET', async: true, success: function (result) {
                $("#StatusMain").html(result);
                //$('.statusList-middle , profile-left').css('min-height', documentMinHeight);
                $('.statusList-middle , profile-left').addClass('contentMinHeight');
                $('.contentMinHeight').css({ 'min-height': documentMinHeight });

                ShowLoading(false);
                $("#divMain").slideDown({ duration: 500 });
            }
        });
        
    });

    $("<a id='aLogout' href='#'>Log Out</>").appendTo("#divLogOut");
    $("#aLogout").on("click", function (event) {
        event.preventDefault();
        $.ajax({
            url: baseUrl + "Account/LogOff",
            type: 'POST',
            async:false,
            success: function (data) {
                if (data == "success") {
                    $("#divMain").slideUp("slow");
                    $("#loginContent").fadeIn({ duration: 500 });
                    $("#divLogOut").fadeOut({ duration: 500 }).empty();
                    clearInterval(setIntervalId);

                }
            }
        })
    });

    $("#divLogOut").fadeIn({ duration: 500 });

    //$("#loginContent").animate({ height: 0, duration: 600 });

    //loginContent
}

function ShowLoading(block)
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


function showLoader(element)
{
    
    $(element).append("<div class='loaderWrapper'>"+
"<div class='loadingOverlay'>"+
            "</div>"+
            "<div class='waitLoader'>"+
             "<img src='" + baseUrl + "Content/themes/base/images/loader-circle-ball.gif'/>" +
                "Processing...."+
            "</div>"+
            "</div>");
}

function hideLoader(element)
{
    $(element).children('.loaderWrapper').remove();
}

function loadProfile(userDetailsID)
{
    var statUrl = baseUrl + "UserProfile/getUserProfile";
    $(".UserProfileMainWrapper").html('');
    $(".progressbar").width(0).html('');
    $(".progressbarContainer").removeClass('hide');
    $.ajax({
        url: statUrl, type: 'GET', dataType: 'html',
        contentType: "application/html; charset=utf-8",
        data: { 'userDetailsId': userDetailsID },
        cache: false,
        xhr: function () {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    //Do something with upload progress here
                }
            }, false);
            xhr.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = Math.round((evt.loaded / evt.total) * 100);
                    var progressBarWidth = percentComplete * $(".progressbarContainer").width() / 100;
                    $(".progressbar").width(progressBarWidth).html(percentComplete + "% ");
                }
            }, false);

            return xhr;
        },
        beforeSend: function (XMLHttpRequest) {

        },
        success: function (result) {
            $(".progressbarContainer").addClass('hide')
            $(".UserProfileMainWrapper").html(result);
        }
    });
}