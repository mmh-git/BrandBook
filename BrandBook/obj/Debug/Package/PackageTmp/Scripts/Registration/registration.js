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
        $.validator.unobtrusive.parse("#frmSignUp");
        var validDate=isDate($('#DateOfBirth').val());
        if ($('#frmSignUp').valid() && validDate) {
            showSignupLoader();
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
                },
                complete: function ()
                {
                    hideSignupLoader();
                }
            });
        }
    })

})


function showSuccessSignupMessage()
{

    $('.message-header').text('Congratulations!');
    $('.message-body>label').text('You have been signedup successfully. Enjoy Brand book');
    $('.signup-message-wrapper').slideDown({ duration: 3000 });
    this.proceed = function ()
    {
        document.getElementById('frmSignUp').reset();
        $('.signup-message-wrapper').slideUp({ duration: 3000 });
    }
}

function showSignupLoader()
{
    $('.signup-loading-overlay, .signup-loader').toggleClass('hide-loader show-loader');
}
function hideSignupLoader()
{
    $('.signup-loading-overlay, .signup-loader').toggleClass('show-loader hide-loader');
}
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