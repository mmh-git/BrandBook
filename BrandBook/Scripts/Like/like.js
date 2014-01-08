$(document).ready(function () {
    $.connection.url = "signalr/hubs";
    var like = $.connection.likeHub;
    like.client.SaveLikeCallBack = function (result) {

        var msg1 = "";
        var msg2 = "";

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
        if (result.Data.likes.length == 0) {
            $('#s' + result.Data.like.LikedContentID + '  .likeStatus').addClass("hide").children(".likemsg").html("").next().html("");
        }
        if (result.Data.like.LikedByUserID == getCookie('UserDetaisID') && result.Data.like.LikeID>1) {
            $('#s' + result.Data.like.LikedContentID + '  .aLike').html("Unlike").data("likeid", result.Data.like.LikeID);
        }
        else {
            $('#s' + result.Data.like.LikedContentID + '  .aLike').html("Like").data("likeid", 0);
        }

    }
    $.connection.hub.start();
    $.connection.hub.start().done(function () {
        $('.aLike').on("click", function (event) {
            var context = $(this);
            event.preventDefault();
            var likeUrl = baseUrl + "Like/SaveLike";
            $.ajax({
                url: likeUrl,
                type: 'POST',
                async: false,
                data: {
                    'LikeID': context.data("likeid"), 'LikedByUserID': getCookie('UserID'), 'LikedContentID': context.data("statusid")
                    , LikedContentType: context.data("liketype")
                }
            });
        })
    })
})


