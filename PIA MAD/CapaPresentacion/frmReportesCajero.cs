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
    public partial class frmReportesCajero : Form
    {
        int indice = 0;
        public frmReportesCajero()
        {
            InitializeComponent();
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmReportes_Load(object sender, EventArgs e)
        {

        }

        private void frmReportesCajero_Load(object sender, EventArgs e)
        {
            //Mostrar Ventas
            List<Departamento> listaDepartamento = new CN_Departamento().Listar();
            cboDep.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Todos los Departamentos" });
            foreach (Departamento item in listaDepartamento)
            {
                cboDep.Items.Add(new OpcionCombo() { Valor = item.Clave, Texto = item.Nombre });
            }
            cboDep.DisplayMember = "Texto";
            cboDep.ValueMember = "Valor";
            cboDep.SelectedIndex = 0;

            List<Empleado> listaCaja = new CN_Empleado().ListarCajeros();
            cboCaja.Items.Add(new OpcionCombo() { Valor = 3000, Texto = "Todos los Cajeros" });
            foreach (Empleado item in listaCaja)
            {
                cboCaja.Items.Add(new OpcionCombo() { Valor = item.IdEmpleado, Texto = item.Nombre + " " + item.ApePaterno + " " + item.ApeMaterno });
            }
            cboCaja.DisplayMember = "Texto";
            cboCaja.ValueMember = "Valor";
            cboCaja.SelectedIndex = 0;


            //Mostrar Ventas por Cajero
            List<ReporteCajero> lista = new CN_Reporte_Cajero().Listar();
            DateTime aux;
            foreach (ReporteCajero item in lista)
            {
                DateTime.TryParse(item.oDetalleVenta.Fecha, out aux);
                dataUser.Rows.Add(new object[] {
                    aux.Day + "/" + aux.Month + "/" + aux.Year,
                    item.oCajero.Nombre + " " + item.oCajero.ApePaterno + " " + item.oCajero.ApeMaterno,
                    item.oDepartamento.Nombre,
                    //item.oProducto.Codigo_producto,
                    item.oDetalleVenta.Cantidad,
                    //item.oDetalleVenta.Subtotal,
                    item.oDetalleVenta.PrecioVenta,
                    item.oDetalleVenta.Utilidad,
                    
                    //item.oDepartamento.Clave,
                    
                });
            }
            calcularTotal();
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (dataUser.Rows.Count > 0)
            {



                string Texto_HTML = Properties.Resources.ReporteCajero.ToString();



                string filas = string.Empty;
                foreach (DataGridViewRow row in dataUser.Rows)
                {
                    filas += "<tr>";
                    filas += "<td>" + row.Cells["Fecha_Venta"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Nombre"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Departamento"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Unidades"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["SumaVenta"].Value.ToString() + "</td>";
                    filas += "<td> $" + row.Cells["Utilidad"].Value.ToString() + "</td>";
                    filas += "</tr>";



                }
                Texto_HTML = Texto_HTML.Replace("@filas", filas);



                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteCajero_0.pdf", DateTime.Now.ToString("dddd MMMM yyy"));
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
                        MessageBox.Show("Reporte de cajero generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }



            }
            else
            {
                MessageBox.Show("No existen reportes por cajero", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dataUser.Rows.RemoveAt(indice);
            if (dataUser.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataUser.Rows)
                {
                    int selectDepa = Convert.ToInt32(((OpcionCombo)cboDep.SelectedItem).Valor.ToString());
                    int selectCaja = Convert.ToInt32(((OpcionCombo)cboCaja.SelectedItem).Valor.ToString());
                    DateTime fecha;

                    if (row.Cells["Departamento"].Value.ToString().Trim().ToUpper().Contains(((OpcionCombo)cboDep.SelectedItem).Texto.Trim().ToUpper()) || selectDepa == 0)
                    {
                        if (row.Cells["Nombre"].Value.ToString().Trim().ToUpper().Contains(((OpcionCombo)cboCaja.SelectedItem).Texto.Trim().ToUpper()) || selectCaja == 3000)
                        {
                            if (checkBox1.Checked)
                            {
                                row.Visible = true;
                            }
                            else
                            {
                                DateTime.TryParse(row.Cells["Fecha_Venta"].Value.ToString(), out fecha);
                                if (fecha.Date >= dtpFecha1.Value.Date && fecha.Date <= dtpFecha2.Value.Date)
                                {
                                    row.Visible = true;
                                }
                                else
                                {
                                    row.Visible = false;
                                }
                            }

                        }
                        else
                        {
                            row.Visible = false;
                        }

                        //row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }


                }
            }
            calcularTotal();
        }

        private void calcularTotal()
        {
            indice = 0;
            decimal Unidades = 0, SumaVenta = 0, Utilidad = 0;
            if (dataUser.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataUser.Rows)
                {
                    if (row.Visible)
                    {
                        Unidades += Convert.ToDecimal(row.Cells["Unidades"].Value.ToString());
                        SumaVenta += Convert.ToDecimal(row.Cells["SumaVenta"].Value.ToString());
                        Utilidad += Convert.ToDecimal(row.Cells["Utilidad"].Value.ToString());
                    }
                    indice++;
                }

                dataUser.Rows.Add(new object[] {
                    "", "","",
                    Unidades, SumaVenta, Utilidad

                });
            }
        }
    }
}
