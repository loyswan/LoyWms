﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Application.Authentication.Interfaces;

public interface IAuthenticatedUserService
{
    string UserId { get; }
}