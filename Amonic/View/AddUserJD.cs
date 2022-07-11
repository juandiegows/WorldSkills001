using Amonic.Helpers;
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
    public partial class AddUserJD : Form
    {
        public AddUserJD()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            FillComboboxRol();
            FillComboboxOficina();
            txtCorreo.IsCorreo();
            txtNombre.Requerido();
            txtApellido.Requerido();
            txtClave.Requerido();
        }
        public void FillComboboxOficina()
        {
            using (Session1Entities model = new Session1Entities())
            {
                var lista = model.Offices.ToList();
                cmbOficina.DataSource = lista;
                cmbOficina.DisplayMember = "Title";
                cmbOficina.ValueMember = "ID";
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnCanceled_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "¿esta seguro que desea guardar?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (this.ValidateChildren())
            {
                using (Session1Entities model = new Session1Entities())
                {
                    model.Users.Add(new Users {
                    Active = 1,
                    Birthdate = DTNacimiento.Value,
                    Email = txtCorreo.Text,
                    LastName = txtApellido.Text,
                    OfficeID = (int) cmbOficina.SelectedValue,
                    Password = txtClave.Text,
                    RoleID = (int) cmbRol.SelectedValue,
                    FirstName = txtNombre.Text
                    });
                    if (model.SaveChanges() > 0)
                    {
                        MessageBox.Show("Se ha agregado el usuario correctamente :)");

                    }
                    else
                    {
                        MessageBox.Show("No Se ha agregado el usuario correctamente :)");
                    }
                    this.Close();
                }
            }
        }
    }
}
