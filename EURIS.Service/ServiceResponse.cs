using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Service
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static ServiceResponse Success(string message)
        {
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static ServiceResponse Error(string message)
        {
            return new ServiceResponse
            {
                IsSuccess = false,
                Message = message
            };
        }


    }
}
