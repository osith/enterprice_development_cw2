using System;
using static CW_2.Models.UserModal;

namespace CW_2.Models
{
    public class DataHandler
    {
        public class Request<T>
        {
            public object Model { get; set; }
        }

        public class Responce
        {
            public string Message { get; set; }
            public bool IsSuccess { get; set; }
        }

        public class UserResponce : Responce
        {
            public RegisterUserDTO User { get; set; }
        }

    }
}
