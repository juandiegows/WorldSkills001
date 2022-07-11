using Amonic.Models;
using Amonic.ViewClass;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amonic.View
{
    public partial class CambiarClaveJD : Form
    {
        public CambiarClaveJD()
        {
            InitializeComponent();
        }

        private void btnCanceled_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPasswordOld.Text != Token.Users.Password)
            {
                MessageBox.Show("Contraseña antigua no coinciden");
                return;
            }
            if(txtConfirmacion.Text != txtPassword.Text)
            {
                MessageBox.Show("No coincide la contraseña ");
                return;
            }
            using(Session1Entities model = new Session1Entities())
            {
                Users users = model.Users.FirstOrDefault(x => x.ID == Token.Users.ID);
                users.Password = txtPassword.Text;
                model.Entry(users).State = EntityState.Modified;
                if (model.SaveChanges() > 0)
                {
                    MessageBox.Show("Se ha cambiado correctamete :)");
                }
                else
                {
                    MessageBox.Show("no se ha cambiado correctamete :)");
                }
                this.Close();
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtPasswordOld.UseSystemPasswordChar)
            {
                txtPasswordOld.UseSystemPasswordChar = false;
                linkLabel1.Text = "Ocultar";
            }
            else
            {
                txtPasswordOld.UseSystemPasswordChar = true;
                linkLabel1.Text = "Mostrar";
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPasswordOld.UseSystemPasswordChar = false;
                linkLabel3.Text = "Ocultar";
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                linkLabel3.Text = "Mostrar";
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtConfirmacion.UseSystemPasswordChar)
            {
                txtConfirmacion.UseSystemPasswordChar = false;
                linkLabel3.Text = "Ocultar";
            }
            else
            {
                txtConfirmacion.UseSystemPasswordChar = true;
                linkLabel3.Text = "Mostrar";
            }
        }
    }
}
