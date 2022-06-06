using System;
using System.ComponentModel.DataAnnotations;

namespace iTrainee.Models.common
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            int age = DateTime.Now.Year - dateTime.Year;
            bool isValid = age > 18;
            return isValid;
        }
    }
}