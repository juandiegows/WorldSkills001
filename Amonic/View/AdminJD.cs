using Amonic.Models;
using Amonic.ViewClass;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amonic.View
{
    public partial class AdminJD : Form
    {
        public AdminJD()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            FillComboboxOficina();
            fillUser();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        public void FillComboboxOficina()
        {
            using(Session1Entities model = new Session1Entities())
            {
                var lista = model.Offices.ToList();
                lista.Insert(0, new Offices
                {
                    ID = 0,
                    Title = "Todas"
                });
                cmbOffices.DataSource = lista;
                cmbOffices.DisplayMember = "Title";
                cmbOffices.ValueMember = "ID";
            }
        }
        private void fillUser()
        {
            using (Session1Entities model = new Session1Entities())
            {
                int IDOffice = (int)cmbOffices.SelectedValue;
                var lista = model.Users.ToList().Where(x=>x.OfficeID== IDOffice || IDOffice ==0).Select(x => new UserView(x)).ToList();

                userViewBindingSource.DataSource = lista;
                TBUser.Refresh();
                TBUser.Columns[0].Visible = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminJD_Load(object sender, EventArgs e)
        {

        }

        private void cmbOffices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fillUser();
            }
            catch (Exception)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int.TryParse(TBUser.CurrentRow.Cells[0].Value.ToString(), out int ID);

            using (Session1Entities model = new Session1Entities())
            {

                Users users = model.Users.FirstOrDefault(x => x.ID == ID);

                new RolJD(users).ShowDialog();
                fillUser();
            }
        }

        private void TBUser_SelectionChanged(object sender, EventArgs e)
        {
            if (TBUser.CurrentRow.Cells[7].Value.ToString() == "1")
            {
                btnRestablecer.Enabled = false;
                btnSuspender.Enabled = true;
            }
            else
            {
                btnRestablecer.Enabled = true;
                btnSuspender.Enabled = false;
            }
        }

        private void btnSuspender_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿esta seguro que desea suspender?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            int.TryParse(TBUser.CurrentRow.Cells[0].Value.ToString(), out int ID);

            using (Session1Entities model = new Session1Entities())
            {

                Users users = model.Users.FirstOrDefault(x => x.ID == ID);

                users.Active = 0;
                model.Entry(users).State = System.Data.Entity.EntityState.Modified;
                if (model.SaveChanges() > 0)
                {
                    
                    model.CredecialesDegnegada.Add(new CredecialesDegnegada
                    {
                        UserID = users.ID,
                        DateTime = DateTime.Now,
                        UserIDDegnegador = Token.Users.ID
                    });
                    model.SaveChanges();
                    MessageBox.Show("Se ha suspendido correctamente :)");
                }else
                {
                    MessageBox.Show("no se ha podido suspendido correctamente :(");
                }
                fillUser();
            }
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "¿esta seguro que desea restablecer?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            int.TryParse(TBUser.CurrentRow.Cells[0].Value.ToString(), out int ID);

            using (Session1Entities model = new Session1Entities())
            {

                Users users = model.Users.FirstOrDefault(x => x.ID == ID);

                users.Active = 1;
                model.Entry(users).State = System.Data.Entity.EntityState.Modified;
                if (model.SaveChanges() > 0)
                {
                    MessageBox.Show("Se ha restablecido correctamente :)");
                }
                else
                {
                    MessageBox.Show("no se ha podido restablecer correctamente :(");
                }
                fillUser();
            }
        }

        private void TBUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.Rows[e.RowIndex].Cells[7].Value.ToString() == "1")
            {
                e.CellStyle.ForeColor = Color.Green;
            }
            else
            {
                e.CellStyle.ForeColor = Color.Red;
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new AddUserJD().ShowDialog();
            fillUser();
        }
    }
}
