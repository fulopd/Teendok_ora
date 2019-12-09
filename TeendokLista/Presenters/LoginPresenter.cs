using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeendokLista.Models;
using TeendokLista.ViewInterfaces;

namespace TeendokLista.Presenters
{
    class LoginPresenter
    {
        private ILoginView view;
        private TeendokContext db = new TeendokContext();
        public bool LoginSucces;

        public LoginPresenter(ILoginView param)
        {
            view = param;
        }
        
        private bool ConnectionExist()
        {
            return db.Database.Exists();
        }

        public void Authenticate()
        {
            if (!ConnectionExist())
            {
                view.ErrorMessage = "Adatbázishoz való kapcsolódás sikertelen!";
            }
            else
            {
                var user = db.felhasznalo
                   .SingleOrDefault(x =>
                   x.felhasznalonev == view.Username &&
                   x.jelszo == view.Password);

                // A felhasználó létezik
                if (user != null)
                {
                    LoginSucces = true;
                }
                else
                {
                    view.ErrorMessage = "Hibás felhasználónév vagy jelszó!";
                }
            }

           
        }
    }
}
