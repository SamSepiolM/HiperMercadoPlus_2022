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
    public partial class frmNotaCredito : Form
    {
        public frmNotaCredito()
        {
            InitializeComponent();
        }

        private void dataUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmNotaCredito_Load(object sender, EventArgs e)
        {
           
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            dataUser.Rows.Clear();
            dataFolios.Rows.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox10.Text = "";

            if (textBox5.Text == "")
            {
                MessageBox.Show("El campo no puede estar vacio", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            List<NotaCredito> lista = new CN_Nota_Credito().BusquedaFolio(textBox5.Text.ToString());

            if (lista.Count == 0)
            {
                MessageBox.Show("Nota no existe", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (NotaCredito item in lista)
            {
                if (lista != null)
                {
                    textBox1.Text = item.Num_Devolucion.ToString();
                    textBox2.Text = item.Fecha.ToString();
                    textBox3.Text = item.oCaja.Num_caja.ToString();
                    textBox4.Text = item.oVenta.IdVenta.ToString();
                    textBox10.Text = item.Total.ToString();
                }

                dataUser.Rows.Add(new object[] {
                    item.oProducto.Codigo_producto,
                    item.Prod_regresado,
                    item.PrecioUni,
                    item.Cantidad,
                    item.Subtotal,
                    item.DescuentoP,
                    item.Total,
                    item.Subtotal

                });
            }

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            dataUser.Rows.Clear();
            dataFolios.Rows.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            if (textBox7.Text == "")
            {
                MessageBox.Show("El campo no puede estar vacio", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<NotaCredito> lista = new CN_Nota_Credito().BusquedaAvanzada(textBox7.Text.ToString(), textBox7.Text.ToString());

            if (lista.Count == 0)
            {
                MessageBox.Show("No existen notas que coincidan", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (NotaCredito item in lista)
            {
                DateTime aux;
                DateTime.TryParse(item.Fecha, out aux);
                if (aux.Date == dtpFecha.Value.Date)
                {
                    dataFolios.Rows.Add(new object[] {

                    "",

                    item.Num_Devolucion,
                    item.oCaja.Num_caja,
                    item.Fecha,

                    //item.Fecha

                    });
                }
                    
            }
        }

        private void dataFolios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataFolios.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                dataUser.Rows.Clear();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox10.Text = "";
                int indice = e.RowIndex;

                if (indice >= 0)
                {

                    List<NotaCredito> lista = new CN_Nota_Credito().BusquedaFolio(dataFolios.Rows[indice].Cells["Folio"].Value.ToString());

                    foreach (NotaCredito item in lista)
                    {
                        if (lista != null)
                        {
                            textBox1.Text = item.Num_Devolucion.ToString();
                            textBox2.Text = item.Fecha.ToString();
                            textBox3.Text = item.oCaja.Num_caja.ToString();
                            textBox4.Text = item.oVenta.IdVenta.ToString();
                            textBox10.Text = item.Total.ToString();
                        }

                        dataUser.Rows.Add(new object[] {
                            item.oProducto.Codigo_producto,
                            item.Prod_regresado,
                            item.PrecioUni,
                            item.Cantidad,
                            item.Subtotal,
                            item.DescuentoP,
                            item.Total,
                            item.Subtotal

                        });
                    }

                }
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            if (dataUser.Rows.Count > 0)
            {



                string Texto_HTML = Properties.Resources.DetalleNota.ToString();



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
                    filas += "<td> $" + row.Cells["Utilidad"].Value.ToString() + "</td>";
                    filas += "</tr>";



                }
                Texto_HTML = Texto_HTML.Replace("@filas", filas);



                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("Detalle_Nota_0.pdf", DateTime.Now.ToString("dddd MMMM yyy"));
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
                        MessageBox.Show("Documento generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }



            }
            else
            {
                MessageBox.Show("No se ha seleccionado la venta", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

    }
}
