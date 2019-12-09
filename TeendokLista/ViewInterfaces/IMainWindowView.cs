using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeendokLista.Models;

namespace TeendokLista.ViewInterfaces
{
    interface IMainWindowView
    {
        feladat feladat { get; set; }
        List<feladat> feladatLista { set; }
    }
}
