using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Infrastructure.Constants
{
    /// <summary>
    /// Validation Constants
    /// </summary>
    public static class ValidationConstants
    {
        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 10;
        public const int ContentMaxLength = 1500;
        public const int ContentMinLength = 30;


        public const string RequiredErrorMessage = "The {0} field is required";
        public const string StringLengthErrorMessage = "The {0} field must be between {2} and {1} character long";
    }
}