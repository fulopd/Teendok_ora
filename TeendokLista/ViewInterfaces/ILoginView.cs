﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeendokLista.ViewInterfaces
{
    interface ILoginView
    {
        string ErrorMessage { set; }
        string Username { get;  }
        string Password { get; }
    }
}
