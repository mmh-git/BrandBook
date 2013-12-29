using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandBook.CustomValidation;
namespace BrandBook.CustomValidation
{
    public class IsDateAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value != null || value.isDate())
            {
                return true;
            }
            return false;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = "Invalid Date Format",
                ValidationType = "isdate"
            };
        }
    }


    public static class MyExtension
    {
        public static bool isDate(this object value)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    
}