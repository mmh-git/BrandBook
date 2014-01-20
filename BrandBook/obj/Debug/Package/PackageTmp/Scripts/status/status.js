$(window).ready(function () {
    var status =$.connection.statusHub;
    status.client.GetNewStatus = function (result)
    {
        $("#divStatus").children('.no-update').detach();
        $("#divStatus").prepend(result.Data.html);
        $("#statusInput").children(".PostStatus").addClass('Shrink').removeClass('Expand');
    }
    $.connection.hub.start();
    $.connection.hub.start().done(function () {
        $("#btnPostStatus").on('click', function () {
            var statusUrl = baseUrl + "Status/SaveStatus";
            //var params = $('#statusInput textarea :input').serialize();
            $.ajax({
                url: statusUrl,
                type: 'POST',
                async:false,
                data: {
                    'StatusContent': $("#txtStatusInput").val(), 'StatusType': $('#StatusType').val(), 'UserID': getCookie('UserID'),
                    'fileName': $('#fileName').val(), 'fileDesc': $('#txtImgDesc').val()
                },
                success: function () {
                    resetStatusUploadControl();
                }
            });

            //status.server.saveStatus({ 'StatusContent': $("#txtStatusInput").val(), 'StatusType': $('#StatusType').val(), 'UserID': getCookie('UserID') });
        })
    });

    
});

function getCookie(cname)
{
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i=0; i<ca.length; i++)
    {
        var c = ca[i].trim();
        if (c.indexOf(name)==0) return c.substring(name.length,c.length);
    }
    return "";
}

function resetStatusUploadControl()
{
    $('.imgStatusWrapper, .imgStatus, .progressBarWrapper').removeClass('hide').addClass('hide');
    $('.statusImg').children('img').attr('src', '');
    $('.PostStatus').removeClass('Shrink').addClass('Shrink');
    $('#txtStatusWrapper').removeClass('hide');
    
    $('#txtImgDesc').val('');
    $("#txtStatusInput").val("");

}