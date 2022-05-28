using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookStoreManagement
{
    public class IsbnRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int isbn = 0;
            try
            {
                if (((string)value).Length > 0) isbn = int.Parse((string)value); 
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
