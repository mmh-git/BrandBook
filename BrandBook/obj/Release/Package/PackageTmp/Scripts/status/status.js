$(document).ready(function () {

    var status = $.connection.statusHub;
    status.client.GetNewStatus = new function (result)
    {
        $("#divStatus").children('.no-update').detach();
        $("#divStatus").prepend(result);
        $("#statusInput").children(".PostStatus").addClass('Shrink').removeClass('Expand');
    }
    $.connection.hub.start().done(function () {
        $("#btnPostStatus").on('click', function () {
            var statusUrl = baseUrl + "Status/SaveStatus";
            //var params = $('#statusInput textarea :input').serialize();
            $.ajax({
                url: statusUrl,
                type: 'POST',
                data: { 'StatusContent': $("#txtStatusInput").val(), 'StatusType': $('#StatusType').val() },
                success: function (result) {
                   
                }
            });
        })
    });

    
});