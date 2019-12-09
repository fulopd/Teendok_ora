using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeendokLista.Models;
using TeendokLista.ViewInterfaces;

namespace TeendokLista.Presenters
{
    class MainWindowPresenter
    {
        private IMainWindowView view;
        private TeendokContext db;

        public MainWindowPresenter(IMainWindowView param)
        {
            view = param;
            db = new TeendokContext();
            // Adatok betöltése
            LoadData();
            if (db.feladat.First() != null)
            {
                GetFeladat(db.feladat.First().Id);
            }
        }

        /// <summary>
        /// Listanézet feltöltése adatbázisból
        /// </summary>
        public void LoadData()
        {
            // A View lista feltöltése az adatbázisból
            view.feladatLista = db.feladat.ToList();
        }
        
        /// <summary>
        /// Kikeres egy adott feladatot.
        /// </summary>
        /// <param name="id">Feladat sorszáma</param>
        public void GetFeladat(int id)
        {
            view.feladat = db.feladat.Find(id);
        }

        public void CheckFeladat(int id, bool allapot)
        {
            var feladat = db.feladat.Find(id);
            feladat.Elvegezve = allapot;

            // Módosítom a bejegyzést
            db.Entry(feladat).State = EntityState.Modified;

            try
            {
                // Elemtem az összes változást az adatbázisban
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
