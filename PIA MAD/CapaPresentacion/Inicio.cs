using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {

        private static Empleado empleadoActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;
        DateTime fechaActual;

        public Inicio(Empleado objempleado, DateTime date)
        {
            empleadoActual = objempleado;
            fechaActual = date;
            InitializeComponent();
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AbrirFormulario(IconMenuItem menu, Form formulario){
            if (MenuActivo != null) {
                MenuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.Firebrick;

            contenedor.Controls.Add(formulario);
            formulario.Show();

        }

        private void iconMenuItem6_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Autorizacion> ListaAutorizacion = new CN_Autorizacion().Listar(empleadoActual.IdEmpleado);

            foreach (IconMenuItem iconmenu in menu.Items) {
                bool encontrado = ListaAutorizacion.Any(m => m.NombreMenu == iconmenu.Name);

                if (encontrado == false) {
                    iconmenu.Visible = false;

                }
            }

            lblnombre.Text = empleadoActual.Nombre;
            lblapellido.Text = empleadoActual.ApePaterno;
            lblapellidoM.Text = empleadoActual.ApeMaterno;
            //lblfecha.Text = fechaActual.ToString();
            lblfecha.Text = fechaActual.ToShortDateString();
            //lblhora.Text = fechaActual.ToShortTimeString();
        }

        private void submenuproducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProducto(empleadoActual, fechaActual));
        }

        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            //AbrirFormulario((IconMenuItem)sender, new frmReportes());
        }

        private void iconMenuItem8_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmDepartamento());
        }

        private void iconMenuItem3_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmVentas(empleadoActual, fechaActual));
        }

        private void iconMenuItem9_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmDetalleVenta());
        }

        private void MenuCajas_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmCaja());
        }

        private void MenuNota_Click(object sender, EventArgs e)
        {
            //AbrirFormulario((IconMenuItem)sender, new frmNotaCredito());
        }

        private void MenuVentas_Click(object sender, EventArgs e)
        {

        }

        private void ventaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmReportes());
        }

        private void cajeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmReportesCajero());
        }

        private void MenuInfo_Click(object sender, EventArgs e)
        {

        }

        private void notaCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmNotasCredito(empleadoActual, fechaActual));
        }

        private void detalleNotaCreditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmNotaCredito());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fechaActual= fechaActual.AddMilliseconds(109.49999999999995);
            //lblhora.Text = DateTime.Now.ToString("hh:mm:ss");
            //lblfecha.Text = DateTime.Now.ToLongDateString();
            lblhora.Text = fechaActual.ToLongTimeString();
            
        }
    }
}
