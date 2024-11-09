using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmNotasCredito : Form
    {
        private static Empleado empleadoActual;
        private bool devuelto = false;
        DateTime fechaNota;
        public frmNotasCredito(Empleado objempleado, DateTime fecha)
        {
            fechaNota = fecha;
            empleadoActual = objempleado;
            InitializeComponent();
        }

        private void frmNotasCredito_Load(object sender, EventArgs e)
        {
            List<MetodoPago> listametodo = new CN_MetodoPago().Listar();
            foreach (MetodoPago item in listametodo)
            {
                cboMetodo.Items.Add(new OpcionCombo() { Valor = item.IdMetodo, Texto = item.NombreMetodo });
            }
            cboMetodo.DisplayMember = "Texto";
            cboMetodo.ValueMember = "Valor";
            cboMetodo.SelectedIndex = 0;

            List<Caja> listacaja = new CN_Caja().Listar();
            foreach (Caja item in listacaja)
            {
                cboCaja.Items.Add(new OpcionCombo() { Valor = item.Id, Texto = item.Num_caja });
            }
            cboCaja.DisplayMember = "Texto";
            cboCaja.ValueMember = "Valor";
            cboCaja.SelectedIndex = 0;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            decimal UME = 0;
            decimal UMKE = 0;
            bool exist = false;
            if (textBox8.Text =="")
            {
                MessageBox.Show("Debe seleccionar un producto", "mensaje", MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                return;
            }
            foreach(DataGridViewRow fila in dataUser.Rows) {
                if (fila.Cells["Codigo_producto"].Value.ToString() == textBox8.Text)
                {
                    exist = true;
                    break;
                }
            }
            if (UMedida.Text.ToString() == "1")
            {
                decimal.TryParse(cantidad1.Text, out UME);

                if (UME % 1 != 0)
                {
                    MessageBox.Show("Formato de cantidad incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            if (UMedida.Text.ToString() == "0")
            {
                if (!decimal.TryParse(cantidad1.Text, out UMKE))
                {
                    MessageBox.Show("Formato de cantidad incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (decimal.Parse(textBox6.Text) < cantidad1.Value || cantidad1.Value <= 0)
            {
                MessageBox.Show("No se puede devolver esta cantidad", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if (!exist)
            {
               
                dataUser.Rows.Add(new object[] {
                    textBox8.Text, textBox2.Text, textBox3.Text, cantidad1.Value.ToString(),

                    decimal.Parse(textBox3.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()), //SubTotal
                    decimal.Parse(textBox10.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()), //Descuento
                    (decimal.Parse(textBox3.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString())) 
                    - (decimal.Parse(textBox10.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString())), //Total
                    UMedida.Text, "", chkMerma.Checked

                    //((OpcionCombo)cboMetodo.SelectedItem).Valor.ToString(),
                    //((OpcionCombo)cboMetodo.SelectedItem).Texto.ToString()
                });
                //textBox5.Text = Convert.ToDecimal(textBox3.Text)
                calculartotal();
                // - (decimal.Parse(textBox6.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()))

            }
        }

        private void calculartotal()
        {
            decimal totalpago = 0;
            decimal totaldesc = 0;
            decimal totalcomp = 0;
            if (dataUser.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataUser.Rows)
                {
                    totalcomp += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
                    totaldesc += Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                    totalpago += Convert.ToDecimal(row.Cells["Total"].Value.ToString());
                }
                    
            }
            textBox13.Text = totalcomp.ToString("0.00");
            textBox12.Text = totaldesc.ToString("0.00");
            textBox14.Text = totalpago.ToString("0.00");
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            devuelto = false;
            dataFolios.Rows.Clear();
            dataUser.Rows.Clear();
            textBox8.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";

            if (textBox5.Text == "")
            {
                MessageBox.Show("El campo no puede estar vacio", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<NotasCredito> lista = new CN_Notas_Credito().BusquedaFolio(textBox5.Text.ToString());

            if (lista.Count == 0)
            {
                MessageBox.Show("Nota no existe", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (NotasCredito item in lista)
            {
                if (lista != null)
                {
                    textBox7.Text = item.IdVenta.ToString();
                    textBox4.Text = item.Fecha.ToString();
                    textBox1.Text = item.oCaja.Num_caja.ToString();
                }

                List<NotasCredito> listaP = new CN_Notas_Credito().BusquedaCantidadExisteNota(item.IdVenta.ToString(), item.oProducto.Codigo_producto.ToString());
                string devueltas = "0.00";
                foreach (NotasCredito item2 in listaP)
                {
                    devueltas = item2.Cantidad.ToString();
                }

                dataFolios.Rows.Add(new object[] {
                    "",
                    item.oProducto.Codigo_producto,
                    item.NombreProducto,
                    item.Cantidad,
                    devueltas
                });
            }
        }

        private void dataFolios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataFolios.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                textBox8.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    if(Convert.ToDecimal(dataFolios.Rows[indice].Cells["CantidadP"].Value) <= Convert.ToDecimal(dataFolios.Rows[indice].Cells["CantidadDevuelto"].Value))
                    {
                        MessageBox.Show("Ya se ha devuelto toda la cantidad de este producto", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    List<NotasCredito> lista = new CN_Notas_Credito().BusquedaProducto(textBox7.Text.ToString(), dataFolios.Rows[indice].Cells["IdPro"].Value.ToString());

                    foreach (NotasCredito item in lista)
                    {
                        if (!item.Devolucion)
                        {
                            MessageBox.Show("El departamento no acepta devolucion", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        textBox8.Text = item.oProducto.Codigo_producto.ToString();
                        textBox2.Text = item.NombreProducto.ToString();
                        textBox3.Text = item.PrecioUnitario.ToString();
                        textBox6.Text = (item.Cantidad - Convert.ToDecimal(dataFolios.Rows[indice].Cells["CantidadDevuelto"].Value)).ToString();
                        textBox9.Text = item.Subtotal.ToString();
                        textBox10.Text = item.Descuento.ToString();
                        textBox11.Text = item.PrecioVenta.ToString();
                        UMedida.Text = item.unidad_medida == true ? "1" : "0";


                        //dataUser.Rows.Add(new object[] {
                        //    item.oProducto.Codigo_producto,
                        //    item.NombreProducto,
                        //    item.PrecioUnitario,
                        //    item.Cantidad,
                        //    item.Subtotal,
                        //    item.Descuento,
                        //    item.PrecioVenta,
                        //    item.Subtotal
                        //});
                    }

                }
            }
        }

        private void dataUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataUser.Columns[e.ColumnIndex].Name == "btnseleccionar1")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    dataUser.Rows.RemoveAt(indice);
                    calculartotal();
                }
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (devuelto)
            {
                MessageBox.Show("Ya se ha devuelto el ticket", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataUser.Rows.Count > 0)
            {
                NotasCredito obj = new NotasCredito()
                {
                    //Fecha = row.Cells["SubTotal"].Value.ToString(),
                    oEmpleado = new Empleado() { IdEmpleado = Convert.ToInt32(empleadoActual.IdEmpleado) },
                    oCaja = new Caja() { Id = Convert.ToInt32(((OpcionCombo)cboCaja.SelectedItem).Valor), Num_caja = ((OpcionCombo)cboCaja.SelectedItem).Texto.ToString() },
                    oVenta = new Venta() { IdVenta = Convert.ToInt32(textBox7.Text.ToString()) },
                    MontoCambio = Convert.ToDecimal(textBox14.Text.ToString()),
                    FechaRegistro = fechaNota,
                };

                string mensaje = string.Empty;
                int idgenerado = new CN_Notas_Credito().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    foreach (DataGridViewRow row in dataUser.Rows)
                    {
                        NotasCredito objn = new NotasCredito()
                        {
                            oProducto = new Producto() { Codigo_producto = Convert.ToInt32(row.Cells["Codigo_producto"].Value.ToString()) },
                            NombreProducto = row.Cells["Nombre"].Value.ToString(),
                            Cantidad = Convert.ToDecimal(row.Cells["Cantidad"].Value.ToString()),
                            Subtotal = Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString()),
                            PrecioVenta = Convert.ToDecimal(row.Cells["Total"].Value.ToString()),
                            PrecioUnitario = Convert.ToDecimal(row.Cells["Precio"].Value.ToString()),
                            Descuento = Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()),
                            unidad_medida = Convert.ToInt32(row.Cells["UnidadMedida"].Value) == 1 ? true : false,
                            //Num_Devolucion = Convert.ToInt32(idgenerado),
                            oVenta=new Venta() { IdVenta= idgenerado},
                            Merma = row.Cells["Merma"].Value.ToString() == "True"? true: false
                        };

                        int iddetalle = new CN_Notas_Credito().RegistrarDetalle(objn, out mensaje);
                        MessageBox.Show("Folio de devolucion= " + idgenerado, "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        devuelto = true;
                    }
                }
                else
                {
                    MessageBox.Show(mensaje);
                }

            }
            else
            {
                MessageBox.Show("Aun no se agregan productos para devolver", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (dataUser.Rows.Count > 0)
            {
                if (!devuelto)
                {
                    MessageBox.Show("Aun no se devuelven los productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                string Texto_HTML = Properties.Resources.NotaCredito.ToString();



                string filas = string.Empty;
                foreach (DataGridViewRow row in dataUser.Rows)
                {
                    filas += "<tr>";
                    filas += "<td>" + row.Cells["Codigo_producto"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Nombre"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["Precio"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["Descuento"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["Total"].Value.ToString() + "</td>";
                    filas += "</tr>";
                }
                Texto_HTML = Texto_HTML.Replace("@filas", filas);
                Texto_HTML = Texto_HTML.Replace("@montototal", textBox14.Text);



                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("Nota_Credito0.pdf", DateTime.Now.ToString("dddd MMMM yyy"));
                savefile.Filter = "Pdf files|*pdf";



                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();



                        using (StringReader sr = new StringReader(Texto_HTML))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        }



                        pdfDoc.Close();
                        stream.Close();
                        MessageBox.Show("Nota generada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aun no se agregan productos para devolver", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
    
}
