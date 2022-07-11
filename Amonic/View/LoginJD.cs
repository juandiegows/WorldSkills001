using Amonic.Helpers;
using Amonic.Models;
using Amonic.View;
using Amonic.ViewClass;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amonic
{
    public partial class LoginJD : Form
    {
        int intentos = 0;
        public LoginJD()
        {
            InitializeComponent();
            Init();
            intentos = 0;
            time = 10;
        }

        private async void Init()
        {
            txtUser.IsCorreo();

            txtPassword.Requerido();
         
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private  void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                using (Session1Entities model = new Session1Entities())
                {
                    Users users = model.Users.ToList().Where(x => x.Email.ToLower() == txtUser.Text.ToLower() && x.Password == txtPassword.Text).FirstOrDefault();
                    if (users == null)
                    {
                        users = model.Users.ToList().Where(x => x.Email.ToLower() == txtUser.Text.ToLower()).FirstOrDefault();
                        if (users == null)
                        {
                             ShowMessage("this user do not exists",5000);
                        }
                        else
                        {
                            model.IntentoFallidos.Add(new IntentoFallidos
                            {
                                Date = DateTime.Now,
                                UserID = users.ID


                            });
                            model.SaveChanges();
                            intentos++;
                            if (intentos == 3)
                            {
                                intentos = 0;
                                txtPassword.Enabled = false;
                                txtUser.Enabled = false;
                                btnCanceled.Enabled = false;
                                btnLogin.Enabled = false;
                                timer1.Enabled = true;
                                time = 10;
                            }
                            ShowMessage($"Password incorrect try again, {intentos} intent", 3000);
                        }

                    }
                    else if (users.Active!=1)
                    {
                        string userName =users.CredecialesDegnegada.Count==0?"sistema": $"{users.CredecialesDegnegada.LastOrDefault().Users1.FirstName} {users.CredecialesDegnegada.LastOrDefault().Users1.LastName}";
                        ShowMessage($"Usuario suspendido por {userName}", 3000);
                    }
                    else
                    {
                        Token.Users = users;
                        if (users.RoleID == 1)
                        {
                            this.Hide();
                            new AdminJD().ShowDialog();
                            this.Show();
                           txtUser.Text= txtPassword.Text= "";
                        }
                        else
                        {
                           
                            this.Hide();
                            new UserJD().ShowDialog();
                            this.Show();
                            txtUser.Text = txtPassword.Text = "";
                        }
                    }
                }
            }

        }

        private async void ShowMessage(String text, int time)
        {
            lblmessage.Text = text;
            lblmessage.Text = await Task.Run(() =>
            {
                Thread.Sleep(time);
                return "";

            });
        }
        int time = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblmessage.Text = $"Credenciales incorrecta, debe esperar {time} segundos";

            time--;
            if (time == 0)
            {
                timer1.Enabled = false;
                txtPassword.Enabled = true;
                txtUser.Enabled = true;
                btnCanceled.Enabled = true;
                btnLogin.Enabled = true;
                lblmessage.Text = "";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                linkLabel1.Text = "Ocultar";
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                linkLabel1.Text = "Mostrar";
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
