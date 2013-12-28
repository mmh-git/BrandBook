$(document).ready(function () {
    $.validator.addMethod('CheckDate', function (value, element, params) {
        if (!/Invalid|NaN/.test(new Date(value)))
            return true;
        else
            return false;
    }, '');
    //$.validator.unobtrusive.adapters.add('IsDate', {}, function (options) {
    //    options.rulse["IsDate"] = true;
    //    options.messages["IsDate"] = options.messages;
    //})

    $("#btnRegister").on('click', function () {
        var regUrl = baseUrl + "Account/SignUp";
        var serialiedData = $("#divSignUp :input").serialize();
        $.validator.unobtrusive.parse("form");
        var validDate=isDate($('#DateOfBirth').val());
        if ($('form').valid() && validDate) {
            $.ajax({
                url: regUrl,
                type: 'POST',
                data: serialiedData,
                success: function (data) {
                    if (data == 'Success') {
                        loadUser();
                    }
                },
                error: function () {

                    alert("error happen");
                }
            });
        }
    })

})

function isDate(value)
{
    if(isNaN(Date.parse(value)))
    {
        if (!$(".birthday-wrapper").hasClass("input-validation-error"))
        {
            $(".birthday-wrapper").addClass("input-validation-error");
        }
        return false
    }
    else
    {
        if ($(".birthday-wrapper").hasClass("input-validation-error"))
        {
            $(".birthday-wrapper").removeClass("input-validation-error");
        }
        return true;
    }
}