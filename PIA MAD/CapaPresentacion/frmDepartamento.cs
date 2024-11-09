using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDepartamento : Form
    {
        public frmDepartamento()
        {
            InitializeComponent();
        }

        private void frmDepartamento_Load(object sender, EventArgs e)
        {
            cboEstDcto.Items.Add(new OpcionCombo { Valor = 1, Texto = "Activo" });
            cboEstDcto.Items.Add(new OpcionCombo { Valor = 0, Texto = "Inactivo" });
            cboEstDcto.DisplayMember = "Texto";
            cboEstDcto.ValueMember = "Valor";
            cboEstDcto.SelectedIndex = 0;


            cboDevolucion.Items.Add(new OpcionCombo { Valor = 1, Texto = "Activo" });
            cboDevolucion.Items.Add(new OpcionCombo { Valor = 0, Texto = "Inactivo" });
            cboDevolucion.DisplayMember = "Texto";
            cboDevolucion.ValueMember = "Valor";
            cboDevolucion.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dataUser.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    cboBuscar.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBuscar.DisplayMember = "Texto";
            cboBuscar.ValueMember = "Valor";
            cboBuscar.SelectedIndex = 0;


            //Mostrar Departamentos
            List<Departamento> lista = new CN_Departamento().Listar();

            foreach (Departamento item in lista)
            {
                dataUser.Rows.Add(new object[] {"",item.Clave, item.Nombre, item.Descuento,
                    item.DescActivo == true ? 1 : 0,
                    item.DescActivo == true ? "Activo" : "Inactivo",
                    item.Devolucion == true ? 1 : 0,
                    item.Devolucion == true ? "Activo" : "Inactivo"
                });
            }
        }

        private void textDto_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }

        }

        private void iconGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Departamento obj = new Departamento()
            {
                Clave = Convert.ToInt32(textId.Text),
                Nombre = textNombre.Text,
                Descuento = Convert.ToInt32(textDto.Text),
                DescActivo = Convert.ToInt32(((OpcionCombo)cboEstDcto.SelectedItem).Valor) == 1 ? true : false,
                Devolucion = Convert.ToInt32(((OpcionCombo)cboDevolucion.SelectedItem).Valor) == 1 ? true : false

            };

            if (obj.Clave == -1)
            {
                int idgenerado = new CN_Departamento().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    dataUser.Rows.Add(new object[] {"",idgenerado, textNombre.Text, textDto.Text, 
                    ((OpcionCombo)cboEstDcto.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboEstDcto.SelectedItem).Texto.ToString(),
                    ((OpcionCombo)cboDevolucion.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboDevolucion.SelectedItem).Texto.ToString()

                });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Departamento().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataUser.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Clave"].Value = textId.Text;
                    row.Cells["Nombre"].Value = textNombre.Text;
                    row.Cells["Descuento"].Value = textDto.Text;
                    row.Cells["IdDescuento"].Value = ((OpcionCombo)cboEstDcto.SelectedItem).Valor.ToString();
                    row.Cells["EdoDesc"].Value = ((OpcionCombo)cboEstDcto.SelectedItem).Texto.ToString();
                    row.Cells["IdDevolucion"].Value = ((OpcionCombo)cboDevolucion.SelectedItem).Valor.ToString();
                    row.Cells["Devolucion"].Value = ((OpcionCombo)cboDevolucion.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void Limpiar()
        {
            txtindice.Text = "-1";
            textId.Text = "-1";
            textNombre.Text =  "";
            textDto.Value = 0;
            cboEstDcto.SelectedIndex = 0;
            cboDevolucion.SelectedIndex = 0;
        }

        private void dataUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataUser.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    textId.Text = dataUser.Rows[indice].Cells["Clave"].Value.ToString();
                    textNombre.Text = dataUser.Rows[indice].Cells["Nombre"].Value.ToString();
                    textDto.Text = dataUser.Rows[indice].Cells["Descuento"].Value.ToString();


                    foreach (OpcionCombo oc in cboEstDcto.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataUser.Rows[indice].Cells["IdDescuento"].Value))
                        {
                            int indice_combo = cboEstDcto.Items.IndexOf(oc);
                            cboEstDcto.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cboDevolucion.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataUser.Rows[indice].Cells["IdDevolucion"].Value))
                        {
                            int indice_combo = cboDevolucion.Items.IndexOf(oc);
                            cboDevolucion.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void iconEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textId.Text) != -1)
            {
                if (MessageBox.Show("¿Desea eliminar el departamento?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Departamento obj = new Departamento()
                    {
                        Clave = Convert.ToInt32(textId.Text)
                    };

                    bool respuesta = new CN_Departamento().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dataUser.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columaFiltro = ((OpcionCombo)cboBuscar.SelectedItem).Valor.ToString();
            if (dataUser.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataUser.Rows)
                {
                    if (row.Cells[columaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBuscar.Text.Trim().ToUpper()))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            foreach (DataGridViewRow row in dataUser.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void cboBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
