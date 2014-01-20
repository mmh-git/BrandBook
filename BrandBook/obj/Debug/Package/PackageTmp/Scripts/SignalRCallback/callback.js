$(document).ready(function () {
    var status = $.connection.statusHub;
    var like = $.connection.likeHub;
    var comment = $.connection.commentHub;
    status.client.GetNewStatus = function (result) {
        $("#divStatus").children('.no-update').detach();
        $("#divStatus").prepend(result.Data.html);
        $("#statusInput").children(".PostStatus").addClass('Shrink').removeClass('Expand');
    }

    like.client.SaveLikeCallBack = function (result) {

        var msg1 = "";
        var msg2 = "";
        if (result.Data.likes.length == 0) {
            $('#StatusMain #s' + result.Data.like.LikedContentID + '  .likeStatus').addClass("hide").children(".likemsg").html("").next().html("");
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

            $('#StatusMain #s' + result.Data.like.LikedContentID + '  .likeStatus').removeClass("hide").children(".likemsg").html(msg1).next().html(msg2);
        }
        if (result.Data.like.LikedByUserID == getCookie('UserDetaisID') && result.Data.like.LikeID > 1) {
            $('#StatusMain #s' + result.Data.like.LikedContentID + '  .aLike').html("Unlike");
        }
        else {
            $('#StatusMain #s' + result.Data.like.LikedContentID + '  .aLike').html("Like");
        }

    }
    comment.client.GetComment = function (result)
    {
        //$('#StatusMain #s' + result.Data.commentModel.StatusID + '  .userComment').prepend(result.Data.html).parent().removeClass('hide');
        $('.userComment').filter(function () {
            return $(this).data('statusid') == result.Data.commentModel.StatusID;
        }).before(result.Data.html).parent().removeClass('hide');
    }
    $.connection.hub.start();
    $.connection.hub.start().done(function () {
        $("#StatusMain").on('click',"#btnPostStatus", postStatus);
        $('#StatusMain').on('keydown', "#txtStatusInput", postStatus);
        $('#StatusMain').on("click", ".aLike", function (event) {
            var context = $(this);
            event.preventDefault();
            var likeUrl = baseUrl + "Like/SaveLike";
            $.ajax({
                url: likeUrl,
                type: 'POST',
                async: false,
                data: { 'LikedContentID': context.data("statusid"), 'LikedContentType': context.data("liketype") }
            });
        });
        $('#StatusMain').on('keydown', '.StatusComment textarea', function (event) {
            if (event.keyCode == 13) {
                var context = $(this);
                var parent = context.parent();
                var statusUrl = baseUrl + "Comment/SaveComment";
                //var params = $('#statusInput textarea :input').serialize();
                $.ajax({
                    url: statusUrl,
                    type: 'POST',
                    async: false,
                    data: { 'StatusID': parent.data('statusid'), 'CommentedByUserID': parent.data('commentbyuserid'), 'CommentContent': context.val(), 'CommentType': 'T', 'Action': 'I' },
                    success: function () {
                        context.val("");
                    }
                });
            }
        })
    });
   
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

function postStatus(event) {
    
    if (event.type == 'click' || (event.type == 'keydown' && event.key == 'Enter')) {
        
        var statusUrl = baseUrl + "Status/SaveStatus";
        //var params = $('#statusInput textarea :input').serialize();
        $.ajax({
            url: statusUrl,
            type: 'POST',
            async: false,
            data: {
                'StatusContent': $("#txtStatusInput").val(), 'StatusType': $('#StatusType').val(), 'UserID': getCookie('UserID'),
                'fileName': $('#fileName').val(), 'fileDesc': $('#txtImgDesc').val()
            },
            success: function () {
                resetStatusUploadControl();
            }
        });
    }
};

function resetStatusUploadControl() {
    $('.imgStatusWrapper, .imgStatus, .progressBarWrapper').removeClass('hide').addClass('hide');
    $('.statusImg').children('img').attr('src', '');
    $('.PostStatus').removeClass('Shrink').addClass('Shrink');

    $('#txtImgDesc').val('');
    $("#txtStatusInput").val("");

}