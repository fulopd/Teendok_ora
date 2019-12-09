using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeendokLista.Models;
using TeendokLista.Presenters;
using TeendokLista.ViewInterfaces;

namespace TeendokLista.Views
{
    public partial class MainWindow : Form, IMainWindowView
    {
        private MainWindowPresenter presenter;
        private bool loading = true;
        public MainWindow()
        {
            InitializeComponent();
            presenter = new MainWindowPresenter(this);
        }

        public feladat feladat {

            get
            {
                int id = int.Parse(labelSorszam.Text);
                DateTime datum = DateTime.Parse(labelDatum.Text);                
                return new feladat(id, textBoxCim.Text, datum, richTextBoxReszletek.Text, checkBoxElvegezve.Checked);
            }
            set
            {
                labelSorszam.Text = value.Id.ToString();
                // Naptári nap jelenik meg = ToShortDateString()
                labelDatum.Text = value.LetrehozasDatum.ToShortDateString();
                textBoxCim.Text = value.Cim;
                richTextBoxReszletek.Text = value.Szoveg;
                checkBoxElvegezve.Checked = value.Elvegezve;
            }
        }
        public List<feladat> feladatLista {
            set
            {
                checkedListBox1.DataSource = value;
                // Megjelénési tulajdonság
                checkedListBox1.DisplayMember = "Cim";
                // Érték tulajdonság
                checkedListBox1.ValueMember = "Id";

                // Listában lévő elemek kipipálása
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].Elvegezve)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                    else
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            loading = false;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!loading)
            {
                // Ha be lett pipálva akkor IGAZ
                bool allapot = e.NewValue == CheckState.Checked ? true : false;
                // Kasztolás nélkül sima OBJECT az értéke 
                // Convert.ToFeladat = (feladat)érték
                var feladat = (feladat)checkedListBox1.Items[e.Index];
                presenter.CheckFeladat(feladat.Id, allapot);
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                // Ellenőrzöm, hogy a kijelölt elemnek van e értéke
                if (checkedListBox1.SelectedItem != null)
                {
                    var id = int.Parse(
                        checkedListBox1.SelectedValue.ToString());
                    presenter.GetFeladat(id);
                }
            }
        }

        private void szerkesztésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.SaveFeladat();

        }

        private void törlésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var id = int.Parse(checkedListBox1.SelectedValue.ToString());
            
            presenter.DeleteFeladat(id);
        }

        private void újFeladatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presenter.CreateFeladat();
            checkedListBox1.ClearSelected();
        }


    }
}
