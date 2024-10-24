﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.ServiceModels.Authentication.Request
{
  public class UserLoginRequest
  {
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }
}
