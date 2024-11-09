using CapaPresentacion.Utilidades;
using CapaEntidad;
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

namespace CapaPresentacion
{
    public partial class frmCaja : Form
    {
        public frmCaja()
        {
            InitializeComponent();
        }

        private void textNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void frmCaja_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new OpcionCombo { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo { Valor = 0, Texto = "Inactivo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;


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


            //Mostrar Cajas
            List<Caja> lista = new CN_Caja().Listar();

            foreach (Caja item in lista)
            {
                dataUser.Rows.Add(new object[] {"",item.Id, item.Num_caja,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "Inactivo"
                });
            }
        }

        private void iconGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Caja obj = new Caja()
            {
                Id = Convert.ToInt32(textId.Text),
                Num_caja = textNum.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
                
            };

            if (obj.Id == -1)
            {
                int idgenerado = new CN_Caja().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    dataUser.Rows.Add(new object[] {"",idgenerado, textNum.Text, 
                    ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()

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
                bool resultado = new CN_Caja().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataUser.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Id"].Value = textId.Text;
                    row.Cells["Num_caja"].Value = textNum.Text;
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

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
            textNum.Text = "";
            cboEstado.SelectedIndex = 0;
        }

        private void dataUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataUser.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    textId.Text = dataUser.Rows[indice].Cells["Id"].Value.ToString();
                    textNum.Text = dataUser.Rows[indice].Cells["Num_caja"].Value.ToString();


                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataUser.Rows[indice].Cells["IdEstado"].Value))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
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

        private void iconEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textId.Text) != -1)
            {
                if (MessageBox.Show("¿Desea eliminar la caja?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Caja obj = new Caja()
                    {
                        Id= Convert.ToInt32(textId.Text)
                    };

                    bool respuesta = new CN_Caja().Eliminar(obj, out mensaje);

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
    }
}
