﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyX.Common
{
    [Serializable]
    public class SignInResponseModel
    {
        public string id { get; set; }
        public string auth_token { get; set; }
        public int expires_in { get; set; }
    }
}
