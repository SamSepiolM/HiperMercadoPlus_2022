using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaPresentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo { Valor = 0, Texto = "Inactivo" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;


            List<Puesto> listapuesto = new CN_Puesto().Listar();

            foreach (Puesto item in listapuesto) {
                cbopuesto.Items.Add(new OpcionCombo() { Valor = item.IdPuesto, Texto = item.Nombre_Puesto}); 
            }
            cbopuesto.DisplayMember = "Texto";
            cbopuesto.ValueMember = "Valor";
            cbopuesto.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dataUser.Columns) {
                if (columna.Visible == true && columna.Name != "btnseleccionar") {
                    cboBuscar.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText});
                }
            }
            cboBuscar.DisplayMember = "Texto";
            cboBuscar.ValueMember = "Valor";
            cboBuscar.SelectedIndex = 0;

            //Mostrar usuarios
            List<Empleado> listaempleado = new CN_Empleado().Listar();

            foreach (Empleado item in listaempleado)
            {
                dataUser.Rows.Add(new object[] {"",item.IdEmpleado, item.Nombre, item.ApePaterno, item.ApeMaterno, item.Telefono, item.Correo, item.CURP, item.Contrasenia, item.NumNomina, item.Domicilio,
                    item.oPuesto.IdPuesto,
                    item.oPuesto.Nombre_Puesto,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "Inactivo"
                });
            }
          
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
            {
            string mensaje = string.Empty;

            Empleado objempleado = new Empleado() {
                IdEmpleado = Convert.ToInt32(textId.Text),
                Nombre = txtNombre.Text,
                ApePaterno = txtApePat.Text,
                ApeMaterno = txtApeMat.Text,
                Telefono = txtTel.Text,
                Correo = txtCorreo.Text,
                CURP = txtCURP.Text,
                Contrasenia = txtContrasenia.Text,
                NumNomina = textNomina.Text,
                Domicilio = textDomicilio.Text,
                oPuesto = new Puesto() { IdPuesto = Convert.ToInt32(((OpcionCombo)cbopuesto.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false

            };

            if (objempleado.IdEmpleado == -1) {
                int idempleadogenerado = new CN_Empleado().Registrar(objempleado, out mensaje);

                if (idempleadogenerado != 0)
                {
                    dataUser.Rows.Add(new object[] {"",idempleadogenerado, txtNombre.Text, txtApePat.Text, txtApeMat.Text, txtTel.Text, txtCorreo.Text, txtCURP.Text, txtContrasenia.Text, textNomina.Text, textDomicilio.Text,
                    ((OpcionCombo)cbopuesto.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cbopuesto.SelectedItem).Texto.ToString(),
                    ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboestado.SelectedItem).Texto.ToString()

                });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else {
                bool resultado = new CN_Empleado().Editar(objempleado, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataUser.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Id"].Value = textId.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["ApePaterno"].Value = txtApePat.Text;
                    row.Cells["ApeMaterno"].Value = txtApeMat.Text;
                    row.Cells["Telefono"].Value = txtTel.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["CURP"].Value = txtCURP.Text;
                    row.Cells["Contrasenia"].Value = txtContrasenia.Text;
                    row.Cells["NumNomina"].Value = textNomina.Text;
                    row.Cells["Domicilio"].Value = textDomicilio.Text;
                    row.Cells["IdPuesto"].Value = ((OpcionCombo)cbopuesto.SelectedItem).Valor.ToString();
                    row.Cells["Puesto"].Value = ((OpcionCombo)cbopuesto.SelectedItem).Texto.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else {
                    MessageBox.Show(mensaje);
                }
            }
        }


        private void Limpiar() {
            txtindice.Text = "-1";
            textId.Text = "-1";
            txtNombre.Text = "";
            txtApePat.Text = "";
            txtApeMat.Text = "";
            txtTel.Text = "";
            txtCorreo.Text = "";
            txtCURP.Text = "";
            txtContrasenia.Text = "";
            txtConfirmContra.Text = "";
            textNomina.Text = "";
            textDomicilio.Text = "";
            cbopuesto.SelectedIndex =  0;
            cboestado.SelectedIndex = 0;
        }

        private void dataUser_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
      
        }

        private void dataUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataUser.Columns[e.ColumnIndex].Name == "btnseleccionar") {

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    textId.Text = dataUser.Rows[indice].Cells["Id"].Value.ToString();
                    txtNombre.Text = dataUser.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtApePat.Text = dataUser.Rows[indice].Cells["ApePaterno"].Value.ToString();
                    txtApeMat.Text = dataUser.Rows[indice].Cells["ApeMaterno"].Value.ToString();
                    txtTel.Text = dataUser.Rows[indice].Cells["Telefono"].Value.ToString();
                    txtCorreo.Text = dataUser.Rows[indice].Cells["Correo"].Value.ToString();
                    txtCURP.Text = dataUser.Rows[indice].Cells["CURP"].Value.ToString();
                    txtContrasenia.Text = dataUser.Rows[indice].Cells["Contrasenia"].Value.ToString();
                    txtConfirmContra.Text = dataUser.Rows[indice].Cells["Contrasenia"].Value.ToString();
                    textNomina.Text = dataUser.Rows[indice].Cells["NumNomina"].Value.ToString();
                    textDomicilio.Text = dataUser.Rows[indice].Cells["Domicilio"].Value.ToString();

                    foreach (OpcionCombo oc in cbopuesto.Items) {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataUser.Rows[indice].Cells["IdPuesto"].Value)) {
                            int indice_combo = cbopuesto.Items.IndexOf(oc);
                            cbopuesto.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cboestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataUser.Rows[indice].Cells["Estado"].Value))
                        {
                            int indice_combo = cboestado.Items.IndexOf(oc);
                            cboestado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textId.Text) != -1) {
                if (MessageBox.Show("¿Desea eliminar el empleado?", "Mensaje",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                    string mensaje = string.Empty;
                    Empleado objempleado = new Empleado()
                    {
                        IdEmpleado = Convert.ToInt32(textId.Text)
                    };

                    bool respuesta = new CN_Empleado().Eliminar(objempleado, out mensaje);

                    if (respuesta)
                    {
                        dataUser.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        Limpiar();
                    }
                    else {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columaFiltro = ((OpcionCombo)cboBuscar.SelectedItem).Valor.ToString();
            if (dataUser.Rows.Count > 0) {
                foreach (DataGridViewRow row in dataUser.Rows) {
                    if (row.Cells[columaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBuscar.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else {
                        row.Visible = false;
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            foreach (DataGridViewRow row in dataUser.Rows)
            {
                row.Visible = true;
            }
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void textNomina_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
