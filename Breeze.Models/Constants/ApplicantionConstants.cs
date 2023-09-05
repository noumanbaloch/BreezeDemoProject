using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Models.Constants
{
    public struct ApiStatusCodes
    {
        // 100 series for failures
        public const short RECORD_ALREADY_EXIST = 100;
        public const short INVALID_USERNAME_OR_PASSWORD = 101;
        public const short SOMETHING_WENT_WRONG = 102;

        // 200 series for success.
        public const short RECORD_SAVED_SUCCESSFULLY = 200;
        public const short SUCCESSFULLY_LOGIN = 201;
    }

    public struct ApiResponseMessages
    {
        public const string RECORD_ALREADY_EXIST = "Record already exists.";
        public const string INVALID_USERNAME_OR_PASSWORD = "Invalid username or password";
        public const string RECORD_SAVED_SUCCESSFULLY = "Record saved successfully.";
        public const string SUCCESSFULLY_LOGIN = "User login successfully.";
        public const string SOMETHING_WENT_WRONG = "Something went wrong.";
    }
}
