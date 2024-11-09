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
    public partial class frmProducto : Form
    {
        private static Empleado empleadoActual;
        private DateTime fechaProducto;
        public frmProducto(Empleado objempleado, DateTime fecha)
        {
            empleadoActual = objempleado;
            fechaProducto = fecha;
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            txtUM.Items.Add(new OpcionCombo { Valor = 1, Texto = "Unidad" });
            txtUM.Items.Add(new OpcionCombo { Valor = 0, Texto = "Kilo" });
            txtUM.DisplayMember = "Texto";
            txtUM.ValueMember = "Valor";
            txtUM.SelectedIndex = 0;

            cboStock.Items.Add(new OpcionCombo { Valor = 1, Texto = "Activo" });
            cboStock.Items.Add(new OpcionCombo { Valor = 0, Texto = "Inactivo" });
            cboStock.DisplayMember = "Texto";
            cboStock.ValueMember = "Valor";
            cboStock.SelectedIndex = 0;


            List<Departamento> listacategoria = new CN_Departamento().Listar();

            foreach (Departamento item in listacategoria)
            {
                cboDepto.Items.Add(new OpcionCombo() { Valor = item.Clave, Texto = item.Nombre });
            }
            cboDepto.DisplayMember = "Texto";
            cboDepto.ValueMember = "Valor";
            cboDepto.SelectedIndex = 0;

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

            //Mostrar Productos
            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                dataUser.Rows.Add(new object[] {"",
                    item.Codigo_producto, 
                    item.Nombre, 
                    item.Descripcion,
                    item.Unidad_medida == true ? 1 : 0,
                    item.Unidad_medida == true ? "Unidad" : "Kilo",
                    item.Costo,
                    item.Precio_unitario,
                    item.Existencia,
                    item.Vendido,
                    item.Merma,
                    item.Stock == true ? 1 : 0,
                    item.Stock == true ? "Activo" : "Inactivo",
                    item.Punto_reorden,
                    item.oDepartamento.Clave,
                    item.oDepartamento.Nombre,
                    item.Descuento,
                    item.FechaInicio.ToString(), item.FechaFin.ToString(), item.FechaModificacion.ToString(), item.NombreAdminMod
                });
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            decimal costo = 0;
            decimal precio_unitario = 0;
            decimal existencia = 0;
            decimal punto_reorden = 0;
            string unidad_medida;
            decimal UMKE = 0;
            decimal UMKPR = 0;
            decimal UME = 0;
            decimal UMPR = 0;


            if (!decimal.TryParse(txtCosto.Text, out costo))
            {
                MessageBox.Show("Formato de Costo incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCosto.Select();
                return;
            }

            if (!decimal.TryParse(txtPU.Text, out precio_unitario))
            {
                MessageBox.Show("Formato de Precio Unitario incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPU.Select();
                return;
            }

            decimal.TryParse(txtCosto.Text, out costo);
            decimal.TryParse(txtPU.Text, out precio_unitario);

            if (precio_unitario <= costo)
            {
                MessageBox.Show("El precio no puede ser menor al costo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCosto.Select();
                txtPU.Select();
                return;
            }

            decimal.TryParse(txtExist.Text, out existencia);
            decimal.TryParse(txtPR.Text, out punto_reorden);
            if (existencia <= punto_reorden)
            {
                MessageBox.Show("El punto de reorden no puede ser mayor a la existencia", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtExist.Select();
                txtPR.Select();
                return;
            }

            unidad_medida = ((OpcionCombo)txtUM.SelectedItem).Valor.ToString();

            if (unidad_medida == "1")
            {
                decimal.TryParse(txtExist.Text, out UME);
                decimal.TryParse(txtPR.Text, out UMPR);

                if (UME % 1 != 0)
                {
                    MessageBox.Show("Formato de existencia incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (UMPR % 1 != 0)
                {
                    MessageBox.Show("Formato de punto de reorden incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (unidad_medida == "0")
            {
                if (!decimal.TryParse(txtExist.Text, out UMKE))
                {
                    MessageBox.Show("Formato de existencia incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!decimal.TryParse(txtPR.Text, out UMKPR))
                {
                    MessageBox.Show("Formato de punto de reorden incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }



            string mensaje = string.Empty;

            Producto obj = new Producto()
            {
                Codigo_producto = Convert.ToInt32(textId.Text),
                Nombre = textNombre.Text,
                Descripcion = txtdescrip.Text,
                Unidad_medida = Convert.ToInt32(((OpcionCombo)txtUM.SelectedItem).Valor) == 1 ? true : false,
                Costo = Convert.ToDecimal(txtCosto.Text),
                Precio_unitario = Convert.ToDecimal(txtPU.Text),
                Existencia = Convert.ToInt32(txtExist.Value),
                Punto_reorden = Convert.ToInt32(txtPR.Value),
                Stock = Convert.ToInt32(((OpcionCombo)cboStock.SelectedItem).Valor) == 1 ? true : false,
                oDepartamento = new Departamento() { Clave = Convert.ToInt32(((OpcionCombo)cboDepto.SelectedItem).Valor) },
                IdAdmin = empleadoActual.IdEmpleado,
                Descuento = Convert.ToDecimal(textDto.Value),
                FechaRegistro = fechaProducto,
                FechaModificacion = fechaProducto,
                FechaInicio=dateTimePicker1.Value,
                FechaFin=dateTimePicker2.Value,
                
            };

            if (obj.Codigo_producto == -1)
            {
                int idgenerado = new CN_Producto().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    dataUser.Rows.Add(new object[] {"",
                        idgenerado,
                        textNombre.Text,
                        txtdescrip.Text,
                        ((OpcionCombo)txtUM.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)txtUM.SelectedItem).Texto.ToString(),
                        txtCosto.Text,
                        txtPU.Text,
                        txtExist.Value, "0.00", "0.00",
                        ((OpcionCombo)cboStock.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboStock.SelectedItem).Texto.ToString(),
                        txtPR.Value.ToString("0.00"),
                        ((OpcionCombo)cboDepto.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboDepto.SelectedItem).Texto.ToString(),
                        textDto.Value.ToString("0.00"),dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), fechaProducto.ToString(), empleadoActual.Nombre + " " + empleadoActual.ApePaterno + " " + empleadoActual.ApeMaterno

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
                bool resultado = new CN_Producto().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dataUser.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Codigo_producto"].Value = textId.Text;
                    row.Cells["Nombre"].Value = textNombre.Text;
                    row.Cells["Descripcion"].Value = txtdescrip.Text;
                    row.Cells["IdUM"].Value = ((OpcionCombo)txtUM.SelectedItem).Valor.ToString();
                    row.Cells["Unidad_medida"].Value = ((OpcionCombo)txtUM.SelectedItem).Texto.ToString();
                    row.Cells["Existencia"].Value = txtExist.Value;
                    row.Cells["Costo"].Value = txtCosto.Text;
                    row.Cells["Precio_unitario"].Value = txtPU.Text;
                    row.Cells["Punto_reorden"].Value = txtPR.Value;

                    row.Cells["IdStock"].Value = ((OpcionCombo)cboStock.SelectedItem).Valor.ToString();
                    row.Cells["Stock"].Value = ((OpcionCombo)cboStock.SelectedItem).Texto.ToString();
                    row.Cells["Clave"].Value = ((OpcionCombo)cboDepto.SelectedItem).Valor.ToString();
                    row.Cells["Nombre_Depto"].Value = ((OpcionCombo)cboDepto.SelectedItem).Texto.ToString();
                    row.Cells["DescuentoProducto"].Value = textDto.Value;
                    row.Cells["FechaInicio"].Value = dateTimePicker1.Value.ToString();
                    row.Cells["FechaFin"].Value = dateTimePicker2.Value.ToString();
                    row.Cells["FechaModificacion"].Value = fechaProducto.ToString();
                    row.Cells["IDmodificado"].Value = empleadoActual.Nombre + " " + empleadoActual.ApePaterno + " " + empleadoActual.ApeMaterno;

                    //dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString(), fechaProducto.ToString(), empleadoActual

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
            textNombre.Text = "";
            txtdescrip.Text = "";
            txtUM.SelectedIndex = 0;
            txtExist.Value = 1;
            txtCosto.Text = "";
            txtPU.Text = "";
            txtPR.Value = 1;
            cboStock.SelectedIndex = 0;
            cboDepto.SelectedIndex = 0;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dataUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataUser.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    textId.Text = dataUser.Rows[indice].Cells["Codigo_producto"].Value.ToString();
                   
                    textNombre.Text = dataUser.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtdescrip.Text = dataUser.Rows[indice].Cells["Descripcion"].Value.ToString();
                    
                    txtExist.Text = dataUser.Rows[indice].Cells["Existencia"].Value.ToString();
                    txtCosto.Text = dataUser.Rows[indice].Cells["Costo"].Value.ToString();
                    txtPU.Text = dataUser.Rows[indice].Cells["Precio_unitario"].Value.ToString();
                    txtPR.Text = dataUser.Rows[indice].Cells["Punto_reorden"].Value.ToString();
                    textDto.Text = dataUser.Rows[indice].Cells["DescuentoProducto"].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataUser.Rows[indice].Cells["FechaInicio"].Value.ToString());
                    dateTimePicker2.Value = DateTime.Parse(dataUser.Rows[indice].Cells["FechaFin"].Value.ToString());

                    foreach (OpcionCombo oc in cboDepto.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataUser.Rows[indice].Cells["Clave"].Value))
                        {
                            int indice_combo = cboDepto.Items.IndexOf(oc);
                            cboDepto.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in txtUM.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataUser.Rows[indice].Cells["IdUM"].Value))
                        {
                            int indice_combo = txtUM.Items.IndexOf(oc);
                            txtUM.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cboStock.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dataUser.Rows[indice].Cells["IdStock"].Value))
                        {
                            int indice_combo = cboStock.Items.IndexOf(oc);
                            cboStock.SelectedIndex = indice_combo;
                            break;
                        }
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

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textId.Text) != -1)
            {
                if (MessageBox.Show("¿Desea eliminar el producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Producto obj = new Producto()
                    {
                        Codigo_producto = Convert.ToInt32(textId.Text)
                    };

                    bool respuesta = new CN_Producto().Eliminar(obj, out mensaje);

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

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)) {
                e.Handled = false;
            }
            else {
                if (txtCosto.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".") {
                    e.Handled = true;
                }
                else {
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == "."){
                        e.Handled = false;
                    }
                    else {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtPU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPU.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
