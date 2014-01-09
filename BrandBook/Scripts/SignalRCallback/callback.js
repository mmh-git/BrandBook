$(window).ready(function () {
    var status = $.connection.statusHub;
    var like = $.connection.likeHub;
    status.client.GetNewStatus = function (result) {
        $("#divStatus").children('.no-update').detach();
        $("#divStatus").prepend(result.Data.html);
        $("#statusInput").children(".PostStatus").addClass('Shrink').removeClass('Expand');
    }

    like.client.SaveLikeCallBack = function (result) {

        var msg1 = "";
        var msg2 = "";
        if (result.Data.likes.length == 0) {
            $('#s' + result.Data.like.LikedContentID + '  .likeStatus').addClass("hide").children(".likemsg").html("").next().html("");
        }
        else {
            if (result.Data.likes.length > 1) {
                msg1 = result.Data.likes.length + ' pepole';
                msg2 = 'like this';
            }
            if (result.Data.likes.length == 1) {
                var like = result.Data.likes[0];
                if (like.LikedByUserID == getCookie('UserDetaisID')) {
                    msg1 = "You";
                    msg2 = "like this";
                }
                else {
                    msg1 = like.LikedByUserFullName;
                    msg2 = "likes this"
                }
            }

            $('#s' + result.Data.like.LikedContentID + '  .likeStatus').removeClass("hide").children(".likemsg").html(msg1).next().html(msg2);
        }
        if (result.Data.like.LikedByUserID == getCookie('UserDetaisID') && result.Data.like.LikeID > 1) {
            $('#s' + result.Data.like.LikedContentID + '  .aLike').html("Unlike");
        }
        else {
            $('#s' + result.Data.like.LikedContentID + '  .aLike').html("Like");
        }

    }

    $.connection.hub.start();
    $.connection.hub.start().done(function () {
        $("#btnPostStatus").on('click', function () {
            var statusUrl = baseUrl + "Status/SaveStatus";
            //var params = $('#statusInput textarea :input').serialize();
            $.ajax({
                url: statusUrl,
                type: 'POST',
                async: false,
                data: { 'StatusContent': $("#txtStatusInput").val(), 'StatusType': $('#StatusType').val(), 'UserID': getCookie('UserID') },
                success: function () {
                    $("#txtStatusInput").val("");
                }
            });

            //status.server.saveStatus({ 'StatusContent': $("#txtStatusInput").val(), 'StatusType': $('#StatusType').val(), 'UserID': getCookie('UserID') });
        })
    });

    $('.aLike').on("click", function (event) {
        var context = $(this);
        event.preventDefault();
        var likeUrl = baseUrl + "Like/SaveLike";
        $.ajax({
            url: likeUrl,
            type: 'POST',
            async: false,
            data: { 'LikedContentID': context.data("statusid"), 'LikedContentType': context.data("liketype") }
        });
    })

});

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

