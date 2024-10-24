﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.Authentication.Responses
{
  public class UserLoginResponse
  {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
