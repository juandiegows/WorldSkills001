using Amonic.ViewClass;

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
    public partial class UserJD : Form
    {
        public UserJD()
        {
            InitializeComponent();
            lblInicio.Text = $"Hola {Token.Users.FirstName} {Token.Users.LastName} Bievenido al sistema de automatizacion de Amonic airline";
            lblIntento.Text = $"Numero de intentos fallidos {Token.Users.IntentoFallidos.Count}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new CambiarClaveJD().ShowDialog();
        }
    }
}
