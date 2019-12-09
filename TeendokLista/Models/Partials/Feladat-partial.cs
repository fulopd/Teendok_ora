using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeendokLista.Models
{
    public partial class feladat
    {
        public feladat(int id, string cim, DateTime letrehozasDatum, string szoveg, bool allapot)
        {
            Id = id;
            Cim = cim;
            LetrehozasDatum = letrehozasDatum;
            Szoveg = szoveg;
            Elvegezve = allapot;            
        }

        public feladat()
        {

        }

    }
}
