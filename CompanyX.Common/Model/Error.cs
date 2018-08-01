using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyX.Common.Model
{
    public enum FailureType
    {
        None = 0,
        Unknown = 1, //500 
        Validation = 2, //400
        Authorization = 3, //403
        BusinessError = 4, //400
        Authentication = 5, //401
        ResourceNotFound = 6, //404     
        ContentNotAvailable = 7,//403
        ServiceUnavailable = 8,//503
        MethodNotAllowed = 9, //405
        ConcurrentCalls = 10 //409
    }


    public class Error
    {
        #region Public Properties

         public string Code { get; set; }

        public string Message { get; set; }

       public string ExtraInformation { get; set; }

        public string FailureTypeString
        {
            get
            {
                return FailureType.ToString();
            }
            set
            {
                FailureType = (FailureType)Enum.Parse(typeof(FailureType), value);
            }
        }

        public FailureType FailureType { get; set; }

        public object Response { get; set; }

        #endregion

        #region Constructor

        public Error()
        { }

        public Error(string code, string message, FailureType failureType)
            : this(code, message, failureType, null)
        { }

         public Error(string code, string message, FailureType failureType, string extraInformation)
        {
            this.Code = code;
            this.Message = message;
            this.FailureType = failureType;
            this.ExtraInformation = extraInformation;
        }

        #endregion
    }
}
