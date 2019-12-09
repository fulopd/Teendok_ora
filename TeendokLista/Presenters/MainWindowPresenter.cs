using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeendokLista.Models;
using TeendokLista.Services;
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

        public void SaveFeladat() {

            var feladat = view.feladat;
            //Jelenlegi felhasználó id -ja
            feladat.felhasznaloId = CurrentUser.Id;

            var letezik = db.feladat.Find(feladat.Id);
            //modositas
            if (letezik != null)
            {
                //a memoriaba a dupla erteket kiszedi
                db.Entry(letezik).State = EntityState.Detached;
                //modositom a dbContextben
                db.Entry(feladat).State = EntityState.Modified;
            }
            //Új létrehozas
            else
            {
                db.feladat.Add(feladat);
            }

            try
            {
                //Adatbázisba mentjük a változásokat
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            // megjelenő lista frissítése
            LoadData();
        }

        public void CreateFeladat() {

            int id = db.feladat
                .Select(x => x.Id)
                //ha üres a tábla akkor 0 -át ad vissza tehát 1 lesz az első Id
                .DefaultIfEmpty(0)
                .Max() + 1;
            view.feladat = new feladat(id, null, DateTime.Now, null, false);
        }

        public void DeleteFeladat(int id) {

            var feladat = db.feladat.Find(id);
            if (feladat != null)
            {
                //dbContextből kiszedte
                db.feladat.Remove(feladat);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            LoadData();
        }
    }
}
