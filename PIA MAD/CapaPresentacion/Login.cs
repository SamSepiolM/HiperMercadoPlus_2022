using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            List<Empleado> TEST = new CN_Empleado().Listar();

            Empleado oempleado = new CN_Empleado().Login().Where(u => u.CURP == textusuario.Text && u.Contrasenia == textcontrasenia.Text).FirstOrDefault();
            DateTime fecha = FechaPick.Value;
            if (oempleado != null)
            {
                Inicio form = new Inicio(oempleado, fecha);

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;
            }
            else {
                MessageBox.Show("Contraseña o CURP incorrecta", "Aleta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            textusuario.Text = "";
            textcontrasenia.Text = "";
            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FechaPick.Value = FechaPick.Value.AddMilliseconds(109.499999999999995);
        }
    }
}
