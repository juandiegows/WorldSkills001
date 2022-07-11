using Amonic.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amonic.View
{
    public partial class RolJD : Form
    {
        Users Users = null;
        public RolJD(Users users)
        {
            InitializeComponent();
            Users = users;
            FillComboboxRol();
            try
            {
                cmbRol.SelectedValue = users.RoleID;
            }
            catch (Exception)
            {

            }
        }

        public void FillComboboxRol()
        {
            using (Session1Entities model = new Session1Entities())
            {
                var lista = model.Roles.ToList();
           
                cmbRol.DataSource = lista;
                cmbRol.DisplayMember = "Title";
                cmbRol.ValueMember = "ID";
            }
        }

        private void btnCanceled_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "¿esta seguro que desea guardar?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            using (Session1Entities model = new Session1Entities())
            {
                Users users = model.Users.FirstOrDefault(x => x.ID == Users.ID);
                users.RoleID = (int)cmbRol.SelectedValue;
                model.Entry(users).State = System.Data.Entity.EntityState.Modified;
                if (model.SaveChanges() > 0)
                {
                    MessageBox.Show("Se ha cambiado correctamente :)");
                }
                else
                {
                    MessageBox.Show("No se  podido ha cambiado correctamente :(");
                }
                this.Close();
            }
        }
    }
}
