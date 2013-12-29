using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandBook.CustomValidation;
using System.Web.Security;
namespace BrandBook.CustomValidation
{
    public class IsUserAvailable : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (Membership.FindUsersByName(value.ToString()).Count != 0)
            {
                return true;
            }
            return false;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {

                ErrorMessage = "User already exists",
                ValidationType = "isuseravailable"
            };
        }
    }

    
}