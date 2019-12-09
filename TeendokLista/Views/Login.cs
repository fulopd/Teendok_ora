using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeendokLista.Presenters;
using TeendokLista.ViewInterfaces;

namespace TeendokLista.Views
{
    public partial class Login : Form, ILoginView
    {
        private LoginPresenter presenter;
        public Login()
        {
            InitializeComponent();
            presenter = new LoginPresenter(this);
        }

        public string ErrorMessage {
            set => errorProvider1.SetError(textBoxFelhasznalonev, value); }
        public string Username { get => textBoxFelhasznalonev.Text; }
        public string Password { get => textBoxJelszo.Text; }

        private void buttonBelepes_Click(object sender, EventArgs e)
        {
            presenter.Authenticate();
            if (presenter.LoginSucces)
            {
                var mw = new MainWindow();
                Hide();
                mw.ShowDialog();
                Close();
            }
        }

        private void textBoxJelszo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonBelepes.PerformClick();
            }
        }
    }
}
