using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Models.Constants
{
    public enum ApiStatusCode
    {
        //100 series for failures
        RecordAlreadyExist = 100,
        InvalidUserName = 101,
        InvalidPassword = 102,

        // 200 series for success
        RecordSavedSuccessfully = 200,
        SuccessfullLogin = 201
    }
}
