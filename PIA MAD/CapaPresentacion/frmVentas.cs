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
    public partial class frmVentas : Form
    {
        private static Empleado empleadoActual;
        private decimal montoPagado = 0;
        private bool pagando = false;
        private bool pagado = false;
        DateTime fechaVenta;
        public frmVentas(Empleado objempleado, DateTime fechaActual)
        {
            empleadoActual = objempleado;
            fechaVenta = fechaActual;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            //dataUser.Columns[3].DefaultCellStyle.Format = "#,##0.00";
            cboDock.Items.Add(new OpcionCombo { Valor = "Ticket", Texto = "Ticket" });
            cboDock.Items.Add(new OpcionCombo { Valor = "Factura", Texto = "Factura" });
            cboDock.DisplayMember = "Texto";
            cboDock.ValueMember = "Valor";
            cboDock.SelectedIndex = 0;

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cboDock_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            dataProducto.Rows.Clear();
            //Producto oProducto = new CN_Producto().Listar().Where(p => p.Nombre == textBox1.Text && p.Stock == true).FirstOrDefault();
            Producto oProducto = null;

            if (textBox1.Text != "")
            {
                List<Producto> listaPro = new CN_Producto().BuscarNombre();

                if (listaPro.Count == 0)
                {
                    MessageBox.Show("Producto no existe", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                foreach (Producto item in listaPro)
                {
                    dataProducto.Rows.Add(new object[] {
                    item.Codigo_producto,
                    item.Nombre
                    });
                }

                

                if (dataProducto.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataProducto.Rows)
                    {
                        if (row.Cells["NombreProducto"].Value.ToString().Trim().ToUpper().Contains(textBox1.Text.Trim().ToUpper()))
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
            else
            {
                if (textBox4.Text != "")
                {
                    oProducto = new CN_Producto().Listar().Where(p => p.Codigo_producto == int.Parse(textBox4.Text) && p.Stock == true).FirstOrDefault();
                }

                if (oProducto != null)
                {
                    Departamento oDepartamento = new CN_Departamento().Listar().Where(d => d.Nombre == oProducto.oDepartamento.Nombre.ToString()).FirstOrDefault();
                    textBox1.BackColor = Color.Honeydew;
                    textBox4.BackColor = Color.Honeydew;
                    textBox4.Text = oProducto.Codigo_producto.ToString();
                    textBox2.Text = oProducto.Nombre;
                    textBox3.Text = oProducto.Precio_unitario.ToString();
                    
                    textBox7.Text = oProducto.oDepartamento.Nombre.ToString();
                    textBox8.Text = textBox4.Text;
                    textBoxExistencias.Text = oProducto.Existencia.ToString();
                    UMedida.Text = oProducto.Unidad_medida == true ? "1" : "0";
                    costo.Text = oProducto.Costo.ToString();
                    if (oDepartamento.DescActivo == true && (oProducto.FechaInicio.Date <= fechaVenta.Date && oProducto.FechaFin.Date >=fechaVenta.Date))
                    {
                        textBox6.Text = oProducto.Descuento.ToString();
                    }
                    else
                    {
                        textBox6.Text = "0.00";
                    }
                    

                    oDepartamento.Clave = oProducto.oDepartamento.Clave;
                    textBox1.Text = "";
                    textBox4.Text = "";
                }
                else
                {
                    textBox1.BackColor = Color.MistyRose;
                    textBox4.BackColor = Color.MistyRose;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    MessageBox.Show("Producto no existe", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            decimal UME = 0;
            decimal UMKE = 0;
            bool exist = false;
            DataGridViewRow row = null;
            if (pagando)
            {
                MessageBox.Show("No se pueden agregar mas productos, ya se encuentra pagando el ticket", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (textBox8.Text =="")
            {
                MessageBox.Show("Debe seleccionar un producto", "mensaje", MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                return;
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
            
            if (cantidad1.Value <= 0)
            {
                MessageBox.Show("No podemos vender esta cantidad", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dataUser.Rows)
            {
                if (fila.Cells["Codigo_producto"].Value.ToString() == textBox8.Text)
                {
                    exist = true;
                    row = fila;
                    //MessageBox.Show("Producto ya existe en el ticket", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //break;
                }
            }

            if (!exist)
            {
                if (decimal.Parse(textBoxExistencias.Text) < cantidad1.Value)
                {
                    MessageBox.Show("No hay suficientes existencias", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                DetalleVenta obj = new DetalleVenta()
                {
                    Cantidad = Convert.ToDecimal(cantidad1.Value.ToString()),
                    PrecioUnitario = Convert.ToDecimal(costo.Text.ToString()),
                    PrecioVenta = Convert.ToDecimal(textBox3.Text.ToString()),
                    Descuento = Convert.ToDecimal(textBox6.Text.ToString())
                };
                string mensaje = string.Empty;
                decimal subtotal, descuento, total, utilidad;

                dataUser.Rows.Add(new object[] {
                    textBox8.Text, textBox2.Text, textBox3.Text, cantidad1.Value.ToString(),
                    //(cantidad1.Value *  decimal.Parse(textBox3.ToString())).ToString()
                    //decimal.Parse(textBox3.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()), //SubTotal
                    //decimal.Parse(textBox6.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()), //Descuento
                    //(decimal.Parse(textBox3.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()))
                    //- (decimal.Parse(textBox6.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString())), //Total
                    // (decimal.Parse(textBox3.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()))
                    //- (decimal.Parse(textBox6.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()))
                    //- (decimal.Parse(costo.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString())) //Utilidad
                    subtotal = new CN_Venta().ObtenerSubtotal(obj, out mensaje), descuento = new CN_Venta().ObtenerDescuento(obj, out mensaje),
                    total = new CN_Venta().ObtenerTotalVenta(obj, out mensaje), utilidad = new CN_Venta().ObtenerUtilidadVenta(obj, out mensaje)

                    , UMedida.Text,
                    ((OpcionCombo)cboMetodo.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboMetodo.SelectedItem).Texto.ToString(),
                }); 
            }
            else
            {
                if (decimal.Parse(textBoxExistencias.Text) < cantidad1.Value + Convert.ToDecimal(row.Cells["Cantidad"].Value))
                {
                    MessageBox.Show("No hay suficientes existencias", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (row.Index >= 0)
                {
                    DetalleVenta obj = new DetalleVenta()
                    {
                        Cantidad = Convert.ToDecimal(cantidad1.Value.ToString()),
                        PrecioUnitario = Convert.ToDecimal(costo.Text.ToString()),
                        PrecioVenta = Convert.ToDecimal(textBox3.Text.ToString()),
                        Descuento = Convert.ToDecimal(textBox6.Text.ToString())
                    };
                    string mensaje = string.Empty;
                    decimal subtotal, descuento, total, utilidad;

                    dataUser.Rows.Insert(row.Index, new object[]
                    {
                        textBox8.Text, textBox2.Text, textBox3.Text, (Convert.ToDecimal(row.Cells["Cantidad"].Value) +Convert.ToDecimal(cantidad1.Value)),
                    //(cantidad1.Value *  decimal.Parse(textBox3.ToString())).ToString()
                    //decimal.Parse(textBox3.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()) + Convert.ToDecimal(row.Cells["SubTotal"].Value), //SubTotal
                    //decimal.Parse(textBox6.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()) + Convert.ToDecimal(row.Cells["Descuento"].Value), //Descuento
                    //(decimal.Parse(textBox3.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()))
                    //- (decimal.Parse(textBox6.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString())) + Convert.ToDecimal(row.Cells["Total"].Value), //Total
                    // (decimal.Parse(textBox3.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()))
                    //- (decimal.Parse(textBox6.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()))
                    //- (decimal.Parse(costo.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString())) + Convert.ToDecimal(row.Cells["Utilidad"].Value),  //Utilidad
                    subtotal = new CN_Venta().ObtenerSubtotal(obj, out mensaje) + Convert.ToDecimal(row.Cells["SubTotal"].Value), 
                    descuento = new CN_Venta().ObtenerDescuento(obj, out mensaje) + Convert.ToDecimal(row.Cells["Descuento"].Value),
                    total = new CN_Venta().ObtenerTotalVenta(obj, out mensaje) + Convert.ToDecimal(row.Cells["Total"].Value), 
                    utilidad = new CN_Venta().ObtenerUtilidadVenta(obj, out mensaje) + Convert.ToDecimal(row.Cells["Utilidad"].Value)

                    ,UMedida.Text,
                    ((OpcionCombo)cboMetodo.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboMetodo.SelectedItem).Texto.ToString(),
                    });
                    dataUser.Rows.Remove(row);
                }
            }
                //textBox5.Text = Convert.ToDecimal(textBox3.Text)
                calculartotal();
                // - (decimal.Parse(textBox6.Text.ToString()) * decimal.Parse(cantidad1.Value.ToString()))

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox8.Text = "";
                textBox11.Text = "0.00";
                textBoxExistencias.Text = "";

                cantidad1.Value = 1;
                dataProducto.Rows.Clear();



        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void calculartotal()
        {
            decimal totalpago = 0;
            decimal totaldesc = 0;
            decimal totalcomp = 0;
            decimal cambio = 0;
            decimal restante = 0;
            if (dataUser.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataUser.Rows)
                {
                    totalcomp += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
                    totaldesc += Convert.ToDecimal(row.Cells["Descuento"].Value.ToString());
                    totalpago += Convert.ToDecimal(row.Cells["Total"].Value.ToString());
                }

                if (textBox11.Text != "" && textBox11.Text != "0.00")
                {
                    cambio = (totalpago - decimal.Parse(textBox11.Text.ToString()) -montoPagado) * -1;
                    
                }


                //if (!((textBox11.Text == "" || textBox11.Text == "0.00") && (textBox13.Text == "" || textBox13.Text == "0.00")))
                //{
                //    if (decimal.Parse(textBox13.Text) <= decimal.Parse(textBox11.Text))
                //    {
                //        textBox13.Text = "0.00";
                //    }
                //    else
                //    {
                //        textBox13.Text = (totalpago - montoPagado).ToString();
                //    }
                //}
                //else
                //{
                //    textBox13.Text = (totalpago - montoPagado).ToString();
                //}
            }
            restante = totalpago - montoPagado;
            if (restante < 0)
            {
                cambio = restante * -1;
                restante = 0;
            }

            textBox9.Text = totalcomp.ToString("0.00");
            textBox10.Text = totaldesc.ToString("0.00");
            textBox5.Text = totalpago.ToString("0.00");
            textBox12.Text = cambio.ToString("0.00");
            textBox13.Text = restante.ToString();
        }

        private void dataUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataUser.Columns[e.ColumnIndex].Name=="btnseleccionar")
            {
                int indice = e.RowIndex;
                if (indice>=0)
                {
                    dataUser.Rows.RemoveAt(indice);
                    calculartotal();
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (pagado)
            {
                MessageBox.Show("Ya se ha pagado el ticket", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataUser.Rows.Count > 0)
            {
                pagando = true;
                if (textBox11.Text == "" || textBox11.Text == "0.00")
                {
                    MessageBox.Show("Falta llenar monto de pago", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (decimal.Parse(textBox13.Text) > decimal.Parse(textBox11.Text))
                {
                    montoPagado += decimal.Parse(textBox11.Text);
                    textBox13.Text = (decimal.Parse(textBox13.Text) - decimal.Parse(textBox11.Text)).ToString();

                    dataMetodos.Rows.Add(new object[] {
                        ((OpcionCombo)cboMetodo.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboMetodo.SelectedItem).Texto.ToString(),
                        decimal.Parse(textBox11.Text)
                    });

                    textBox11.Text = "0.00";
                    textBox14.Text = montoPagado.ToString();
                    return;
                }
                if (decimal.Parse(textBox13.Text) <= decimal.Parse(textBox11.Text))
                {
                    montoPagado += decimal.Parse(textBox11.Text);
                    textBox13.Text = (decimal.Parse(textBox13.Text) - decimal.Parse(textBox11.Text)).ToString();
                    textBox13.Text = "0.00";

                    dataMetodos.Rows.Add(new object[] {
                        ((OpcionCombo)cboMetodo.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboMetodo.SelectedItem).Texto.ToString(),
                        decimal.Parse(textBox11.Text)
                    });

                    textBox14.Text = montoPagado.ToString();
                    //textBox11.Text = "0.00";
                }


                Venta obj = new Venta()
                {
                    //Fecha = row.Cells["SubTotal"].Value.ToString(),
                    oEmpleado = new Empleado() { IdEmpleado = Convert.ToInt32(empleadoActual.IdEmpleado) },
                    oCaja = new Caja() { Id = Convert.ToInt32(((OpcionCombo)cboCaja.SelectedItem).Valor), Num_caja = ((OpcionCombo)cboCaja.SelectedItem).Texto.ToString() },
                    TipoDocumento = ((OpcionCombo)cboDock.SelectedItem).Texto.ToString(),
                    NumeroDocumento = ((OpcionCombo)cboDock.SelectedItem).Valor.ToString(),
                    MontoPago = Convert.ToDecimal(textBox14.Text.ToString()),
                    MontoCambio = Convert.ToDecimal(textBox12.Text.ToString()),
                    MontoTotal = Convert.ToDecimal(textBox5.Text.ToString()),
                    DescuentoTotal= Convert.ToDecimal(textBox10.Text.ToString()),
                    FechaRegistro=fechaVenta,
                };
                string mensaje = string.Empty;
                new CN_Venta().iniciarTransaccion(out mensaje);
                int idgenerado = new CN_Venta().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    foreach (DataGridViewRow row in dataMetodos.Rows)
                    {
                        MetodoPago meto = new MetodoPago()
                        {
                            IdMetodo = Convert.ToInt32(row.Cells["dataGridViewTextBoxColumn8"].Value),
                            MontoPago = Convert.ToDecimal(row.Cells["Monto"].Value),
                            IdVentaM = idgenerado
                        };

                        string mensaje1 = string.Empty;
                        int idgenerado1 = new CN_MetodoPago().Registrar(meto, out mensaje1);
                    }

                    foreach (DataGridViewRow row in dataUser.Rows)
                    {
                        DetalleVenta objn = new DetalleVenta()
                        {
                            oProducto = new Producto() { Codigo_producto = Convert.ToInt32(row.Cells["Codigo_producto"].Value.ToString()) },
                            NombreProducto = row.Cells["Nombre"].Value.ToString(),
                            Cantidad = Convert.ToDecimal(row.Cells["Cantidad"].Value.ToString()),
                            Subtotal = Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString()),
                            PrecioVenta = Convert.ToDecimal(row.Cells["Total"].Value.ToString()),
                            PrecioUnitario = Convert.ToDecimal(row.Cells["Precio"].Value.ToString()),
                            Descuento = Convert.ToDecimal(row.Cells["Descuento"].Value.ToString()),
                            unidad_medida = Convert.ToInt32(row.Cells["UnidadM"].Value) == 1 ? true : false,
                            //Num_Devolucion = Convert.ToInt32(idgenerado),
                            oVenta = new Venta() { IdVenta = idgenerado },
                            Utilidad = Convert.ToDecimal(row.Cells["Utilidad"].Value.ToString()),
                            //Utilidad = 0,
                            //oMetodoPago =new MetodoPago { IdMetodo= Convert.ToInt32(row.Cells["idMetodoPago"].Value.ToString()) }
                        };

                        int iddetalle = new CN_Venta().RegistrarDetalle(objn, out mensaje);

                    }
                    MessageBox.Show("Folio de venta= " + idgenerado, "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    new CN_Venta().confirmarTransaccion(out mensaje);
                    pagado = true;
                }
                else
                {
                    MessageBox.Show(mensaje);
                    new CN_Venta().cancelarTransaccion(out mensaje);
                }

            }
            else
            {
                MessageBox.Show("Aun no se agregan productos", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //textBox11.Text = "0.00";
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (textBox11.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                //textBox11.Text = "0.00";
                textBox11.Text = "";
                return;
            }
            if (dataUser.Rows.Count > 0 && decimal.Parse(textBox11.Text) >= 0)
            {
                decimal aux = (decimal.Parse(textBox5.Text) - decimal.Parse(textBox11.Text) -montoPagado) * -1;
                if (aux > 0)
                {
                    textBox12.Text = aux.ToString();
                }
                else
                {
                    textBox12.Text = "0.00";
                }
            }
            else
            {
                textBox11.Text = "";
                MessageBox.Show("Aun no se agregan productos", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnTicket_Click(object sender, EventArgs e)
        {
            if (dataUser.Rows.Count > 0)
            {
                if (!pagado)
                {
                    MessageBox.Show("Aun no se paga el ticket", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (cboDock.Text == "")
                {
                    MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                string dock;
                string Texto_HTML = Properties.Resources.Ticket.ToString();
                dock = cboDock.Text;



                Texto_HTML = Texto_HTML.Replace("@tipodocumento", dock);



                string filas = string.Empty;
                foreach (DataGridViewRow row in dataUser.Rows)
                {
                    filas += "<tr>";
                    filas += "<td>" + row.Cells["Nombre"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["Precio"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["Descuento"].Value.ToString() + "</td>";
                    filas += "</tr>";
                }
                Texto_HTML = Texto_HTML.Replace("@filas", filas);



                string Metpag;
                Metpag = cboMetodo.Text;
                Texto_HTML = Texto_HTML.Replace("@metodoPago", Metpag);
                Texto_HTML = Texto_HTML.Replace("@monto", textBox11.Text);



                Texto_HTML = Texto_HTML.Replace("@montototal", textBox5.Text);



                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("Ticket_0.pdf", DateTime.Now.ToString("dddd MMMM yyy"));
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
                        MessageBox.Show("Ticket generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aun no se agregan productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            dataUser.Rows.Clear();
            dataMetodos.Rows.Clear();
            dataProducto.Rows.Clear();
            montoPagado = 0;
            pagando = false;
            pagado = false;
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBoxExistencias.Text = "";
        }

        private void dataProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataProducto.Columns[e.ColumnIndex].Name == "btnseleccionar2")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    
                    Producto oProducto = new CN_Producto().Listar().Where(p => p.Nombre == dataProducto.Rows[indice].Cells["NombreProducto"].Value.ToString() && p.Stock == true).FirstOrDefault();

                    if (oProducto != null)
                    {
                        Departamento oDepartamento = new CN_Departamento().Listar().Where(d => d.Nombre == oProducto.oDepartamento.Nombre.ToString()).FirstOrDefault();
                        textBox1.BackColor = Color.Honeydew;
                        textBox4.BackColor = Color.Honeydew;
                        textBox4.Text = oProducto.Codigo_producto.ToString();
                        textBox2.Text = oProducto.Nombre;
                        textBox3.Text = oProducto.Precio_unitario.ToString();
                        
                        textBox7.Text = oProducto.oDepartamento.Nombre.ToString();
                        textBox8.Text = textBox4.Text;
                        textBoxExistencias.Text = oProducto.Existencia.ToString();
                        UMedida.Text = oProducto.Unidad_medida == true ? "1" : "0";
                        costo.Text = oProducto.Costo.ToString();

                        if (oDepartamento.DescActivo == true && (oProducto.FechaInicio.Date <= fechaVenta.Date && oProducto.FechaFin.Date >= fechaVenta.Date))
                        {
                            textBox6.Text = oProducto.Descuento.ToString();
                        }
                        else
                        {
                            textBox6.Text = "0.00";
                        }

                        oDepartamento.Clave = oProducto.oDepartamento.Clave;
                        textBox1.Text = "";
                        textBox4.Text = "";
                    }
                    else
                    {
                        textBox1.BackColor = Color.MistyRose;
                        textBox4.BackColor = Color.MistyRose;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        MessageBox.Show("Producto no existe", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
