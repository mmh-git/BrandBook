﻿$(function () {
    var like = $.connection.likeHub;
    like.client.SaveLikeCallBack = function (result) {
        
        var msg1 = "";
        var msg2 = "";

    if(result.Data.likes.length>1)
    {
            msg1 = result.length + ' pepole';
        msg2 = 'like this';
    }
    if (result.Data.likes.length == 1) {
            var like = result[0];
            if (like.LikedByUserID == getCookie('UserID')) {
                msg1 = "You";
                msg2 = "like this";
            }
            else {
                msg1 = like.LikedByUserFullName;
                msg2 = "likes this"
            }
    }
    $('#' + result[0].LikedContentID + ' > .likemsg').removeClass("hide").html(msg1).next().html(msg2);
       if (result.Data.likes.length == 0)
       {
           $('#' + result.Data.likes[0].LikedContentID + ' > .likemsg').addClass("hide").html("").next().html("");
       }
       if (result.Data.like.LikeID > 1)
       {
           $('#' + result.Data.likes[0].LikedContentID + ' > .aLike').html("Unlike").data("likeid", result.Data.like.LikeID);
       }
       if (result.Data.like.LikeID ==0) {
           $('#' + result.Data.likes[0].LikedContentID + ' > .aLike').html("Like").data("likeid", 0);
       }
        
    }

    $.connection.hub.start().done(function () {
        $('.aLike').live("click", function (event) {
            var context=$(this);
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