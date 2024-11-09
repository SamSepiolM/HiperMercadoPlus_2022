
namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.MenuUsuario = new FontAwesome.Sharp.IconMenuItem();
            this.MenuAdministrador = new FontAwesome.Sharp.IconMenuItem();
            this.submenuproducto = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem8 = new FontAwesome.Sharp.IconMenuItem();
            this.MenuCajas = new FontAwesome.Sharp.IconMenuItem();
            this.MenuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem3 = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem9 = new FontAwesome.Sharp.IconMenuItem();
            this.MenuReportes = new FontAwesome.Sharp.IconMenuItem();
            this.ventaToolStripMenuItem = new FontAwesome.Sharp.IconMenuItem();
            this.cajeroToolStripMenuItem = new FontAwesome.Sharp.IconMenuItem();
            this.MenuNota = new FontAwesome.Sharp.IconMenuItem();
            this.notaCreditoToolStripMenuItem = new FontAwesome.Sharp.IconMenuItem();
            this.detalleNotaCreditoToolStripMenuItem = new FontAwesome.Sharp.IconMenuItem();
            this.MenuInfo = new FontAwesome.Sharp.IconMenuItem();
            this.menutitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblnombre = new System.Windows.Forms.Label();
            this.lblapellido = new System.Windows.Forms.Label();
            this.lblapellidoM = new System.Windows.Forms.Label();
            this.lblhora = new System.Windows.Forms.Label();
            this.lblfecha = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuUsuario,
            this.MenuAdministrador,
            this.MenuCajas,
            this.MenuVentas,
            this.MenuReportes,
            this.MenuNota,
            this.MenuInfo});
            this.menu.Location = new System.Drawing.Point(0, 70);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(934, 73);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // MenuUsuario
            // 
            this.MenuUsuario.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.MenuUsuario.IconColor = System.Drawing.Color.Black;
            this.MenuUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuUsuario.IconSize = 50;
            this.MenuUsuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuUsuario.Name = "MenuUsuario";
            this.MenuUsuario.Size = new System.Drawing.Size(64, 69);
            this.MenuUsuario.Text = "Usuarios";
            this.MenuUsuario.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.MenuUsuario.Click += new System.EventHandler(this.iconMenuItem6_Click);
            // 
            // MenuAdministrador
            // 
            this.MenuAdministrador.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuproducto,
            this.iconMenuItem8});
            this.MenuAdministrador.IconChar = FontAwesome.Sharp.IconChar.User;
            this.MenuAdministrador.IconColor = System.Drawing.Color.Black;
            this.MenuAdministrador.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuAdministrador.IconSize = 50;
            this.MenuAdministrador.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuAdministrador.Name = "MenuAdministrador";
            this.MenuAdministrador.Size = new System.Drawing.Size(62, 69);
            this.MenuAdministrador.Text = "Admin";
            this.MenuAdministrador.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // submenuproducto
            // 
            this.submenuproducto.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuproducto.IconColor = System.Drawing.Color.Black;
            this.submenuproducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuproducto.Name = "submenuproducto";
            this.submenuproducto.Size = new System.Drawing.Size(150, 22);
            this.submenuproducto.Text = "Productos";
            this.submenuproducto.Click += new System.EventHandler(this.submenuproducto_Click);
            // 
            // iconMenuItem8
            // 
            this.iconMenuItem8.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItem8.IconColor = System.Drawing.Color.Black;
            this.iconMenuItem8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem8.Name = "iconMenuItem8";
            this.iconMenuItem8.Size = new System.Drawing.Size(150, 22);
            this.iconMenuItem8.Text = "Departamento";
            this.iconMenuItem8.Click += new System.EventHandler(this.iconMenuItem8_Click);
            // 
            // MenuCajas
            // 
            this.MenuCajas.IconChar = FontAwesome.Sharp.IconChar.BoxesPacking;
            this.MenuCajas.IconColor = System.Drawing.Color.Black;
            this.MenuCajas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuCajas.IconSize = 50;
            this.MenuCajas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuCajas.Name = "MenuCajas";
            this.MenuCajas.Size = new System.Drawing.Size(62, 69);
            this.MenuCajas.Text = "Cajas";
            this.MenuCajas.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.MenuCajas.Click += new System.EventHandler(this.MenuCajas_Click);
            // 
            // MenuVentas
            // 
            this.MenuVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuItem3,
            this.iconMenuItem9});
            this.MenuVentas.IconChar = FontAwesome.Sharp.IconChar.MoneyCheck;
            this.MenuVentas.IconColor = System.Drawing.Color.Black;
            this.MenuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuVentas.IconSize = 50;
            this.MenuVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuVentas.Name = "MenuVentas";
            this.MenuVentas.Size = new System.Drawing.Size(62, 69);
            this.MenuVentas.Text = "Ventas";
            this.MenuVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.MenuVentas.Click += new System.EventHandler(this.MenuVentas_Click);
            // 
            // iconMenuItem3
            // 
            this.iconMenuItem3.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItem3.IconColor = System.Drawing.Color.Black;
            this.iconMenuItem3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem3.Name = "iconMenuItem3";
            this.iconMenuItem3.Size = new System.Drawing.Size(143, 22);
            this.iconMenuItem3.Text = "Venta";
            this.iconMenuItem3.Click += new System.EventHandler(this.iconMenuItem3_Click);
            // 
            // iconMenuItem9
            // 
            this.iconMenuItem9.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItem9.IconColor = System.Drawing.Color.Black;
            this.iconMenuItem9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem9.Name = "iconMenuItem9";
            this.iconMenuItem9.Size = new System.Drawing.Size(143, 22);
            this.iconMenuItem9.Text = "Detalle Venta";
            this.iconMenuItem9.Click += new System.EventHandler(this.iconMenuItem9_Click);
            // 
            // MenuReportes
            // 
            this.MenuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ventaToolStripMenuItem,
            this.cajeroToolStripMenuItem});
            this.MenuReportes.IconChar = FontAwesome.Sharp.IconChar.Pager;
            this.MenuReportes.IconColor = System.Drawing.Color.Black;
            this.MenuReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuReportes.IconSize = 50;
            this.MenuReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuReportes.Name = "MenuReportes";
            this.MenuReportes.Size = new System.Drawing.Size(65, 69);
            this.MenuReportes.Text = "Reportes";
            this.MenuReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.MenuReportes.Click += new System.EventHandler(this.iconMenuItem1_Click);
            // 
            // ventaToolStripMenuItem
            // 
            this.ventaToolStripMenuItem.IconChar = FontAwesome.Sharp.IconChar.None;
            this.ventaToolStripMenuItem.IconColor = System.Drawing.Color.Black;
            this.ventaToolStripMenuItem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ventaToolStripMenuItem.Name = "ventaToolStripMenuItem";
            this.ventaToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.ventaToolStripMenuItem.Text = "Ventas";
            this.ventaToolStripMenuItem.Click += new System.EventHandler(this.ventaToolStripMenuItem_Click);
            // 
            // cajeroToolStripMenuItem
            // 
            this.cajeroToolStripMenuItem.IconChar = FontAwesome.Sharp.IconChar.None;
            this.cajeroToolStripMenuItem.IconColor = System.Drawing.Color.Black;
            this.cajeroToolStripMenuItem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.cajeroToolStripMenuItem.Name = "cajeroToolStripMenuItem";
            this.cajeroToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.cajeroToolStripMenuItem.Text = "Cajero";
            this.cajeroToolStripMenuItem.Click += new System.EventHandler(this.cajeroToolStripMenuItem_Click);
            // 
            // MenuNota
            // 
            this.MenuNota.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notaCreditoToolStripMenuItem,
            this.detalleNotaCreditoToolStripMenuItem});
            this.MenuNota.IconChar = FontAwesome.Sharp.IconChar.Wallet;
            this.MenuNota.IconColor = System.Drawing.Color.Black;
            this.MenuNota.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuNota.IconSize = 50;
            this.MenuNota.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuNota.Name = "MenuNota";
            this.MenuNota.Size = new System.Drawing.Size(85, 69);
            this.MenuNota.Text = "Nota credito";
            this.MenuNota.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.MenuNota.Click += new System.EventHandler(this.MenuNota_Click);
            // 
            // notaCreditoToolStripMenuItem
            // 
            this.notaCreditoToolStripMenuItem.IconChar = FontAwesome.Sharp.IconChar.None;
            this.notaCreditoToolStripMenuItem.IconColor = System.Drawing.Color.Black;
            this.notaCreditoToolStripMenuItem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.notaCreditoToolStripMenuItem.Name = "notaCreditoToolStripMenuItem";
            this.notaCreditoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.notaCreditoToolStripMenuItem.Text = "Nota Credito";
            this.notaCreditoToolStripMenuItem.Click += new System.EventHandler(this.notaCreditoToolStripMenuItem_Click);
            // 
            // detalleNotaCreditoToolStripMenuItem
            // 
            this.detalleNotaCreditoToolStripMenuItem.IconChar = FontAwesome.Sharp.IconChar.None;
            this.detalleNotaCreditoToolStripMenuItem.IconColor = System.Drawing.Color.Black;
            this.detalleNotaCreditoToolStripMenuItem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.detalleNotaCreditoToolStripMenuItem.Name = "detalleNotaCreditoToolStripMenuItem";
            this.detalleNotaCreditoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.detalleNotaCreditoToolStripMenuItem.Text = "Detalle Nota Credito";
            this.detalleNotaCreditoToolStripMenuItem.Click += new System.EventHandler(this.detalleNotaCreditoToolStripMenuItem_Click);
            // 
            // MenuInfo
            // 
            this.MenuInfo.IconChar = FontAwesome.Sharp.IconChar.Info;
            this.MenuInfo.IconColor = System.Drawing.Color.Black;
            this.MenuInfo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.MenuInfo.IconSize = 50;
            this.MenuInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuInfo.Name = "MenuInfo";
            this.MenuInfo.Size = new System.Drawing.Size(71, 69);
            this.MenuInfo.Text = "Acerca de";
            this.MenuInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.MenuInfo.Visible = false;
            this.MenuInfo.Click += new System.EventHandler(this.MenuInfo_Click);
            // 
            // menutitulo
            // 
            this.menutitulo.AutoSize = false;
            this.menutitulo.BackColor = System.Drawing.Color.Firebrick;
            this.menutitulo.Location = new System.Drawing.Point(0, 0);
            this.menutitulo.Name = "menutitulo";
            this.menutitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menutitulo.Size = new System.Drawing.Size(934, 70);
            this.menutitulo.TabIndex = 1;
            this.menutitulo.Text = "menuStrip2";
            this.menutitulo.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip2_ItemClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Firebrick;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FloralWhite;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bienvenido a NTEAN";
            // 
            // contenedor
            // 
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 143);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(934, 519);
            this.contenedor.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Firebrick;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(655, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Usuario:";
            // 
            // lblnombre
            // 
            this.lblnombre.AutoSize = true;
            this.lblnombre.BackColor = System.Drawing.Color.Firebrick;
            this.lblnombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnombre.ForeColor = System.Drawing.Color.White;
            this.lblnombre.Location = new System.Drawing.Point(722, 21);
            this.lblnombre.Name = "lblnombre";
            this.lblnombre.Size = new System.Drawing.Size(69, 17);
            this.lblnombre.TabIndex = 5;
            this.lblnombre.Text = "idNombre";
            // 
            // lblapellido
            // 
            this.lblapellido.AutoSize = true;
            this.lblapellido.BackColor = System.Drawing.Color.Firebrick;
            this.lblapellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblapellido.ForeColor = System.Drawing.Color.White;
            this.lblapellido.Location = new System.Drawing.Point(722, 44);
            this.lblapellido.Name = "lblapellido";
            this.lblapellido.Size = new System.Drawing.Size(69, 17);
            this.lblapellido.TabIndex = 6;
            this.lblapellido.Text = "idApellido";
            // 
            // lblapellidoM
            // 
            this.lblapellidoM.AutoSize = true;
            this.lblapellidoM.BackColor = System.Drawing.Color.Firebrick;
            this.lblapellidoM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblapellidoM.ForeColor = System.Drawing.Color.White;
            this.lblapellidoM.Location = new System.Drawing.Point(797, 44);
            this.lblapellidoM.Name = "lblapellidoM";
            this.lblapellidoM.Size = new System.Drawing.Size(80, 17);
            this.lblapellidoM.TabIndex = 7;
            this.lblapellidoM.Text = "idApellidoM";
            // 
            // lblhora
            // 
            this.lblhora.AutoSize = true;
            this.lblhora.BackColor = System.Drawing.Color.Firebrick;
            this.lblhora.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhora.ForeColor = System.Drawing.Color.White;
            this.lblhora.Location = new System.Drawing.Point(333, 9);
            this.lblhora.Name = "lblhora";
            this.lblhora.Size = new System.Drawing.Size(38, 17);
            this.lblhora.TabIndex = 8;
            this.lblhora.Text = "label";
            // 
            // lblfecha
            // 
            this.lblfecha.AutoSize = true;
            this.lblfecha.BackColor = System.Drawing.Color.Firebrick;
            this.lblfecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfecha.ForeColor = System.Drawing.Color.White;
            this.lblfecha.Location = new System.Drawing.Point(333, 33);
            this.lblfecha.Name = "lblfecha";
            this.lblfecha.Size = new System.Drawing.Size(38, 17);
            this.lblfecha.TabIndex = 9;
            this.lblfecha.Text = "label";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 662);
            this.Controls.Add(this.lblfecha);
            this.Controls.Add(this.lblhora);
            this.Controls.Add(this.lblapellidoM);
            this.Controls.Add(this.lblapellido);
            this.Controls.Add(this.lblnombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.menutitulo);
            this.MainMenuStrip = this.menu;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.MenuStrip menutitulo;
        private FontAwesome.Sharp.IconMenuItem MenuUsuario;
        private FontAwesome.Sharp.IconMenuItem MenuAdministrador;
        private FontAwesome.Sharp.IconMenuItem MenuCajas;
        private FontAwesome.Sharp.IconMenuItem MenuVentas;
        private FontAwesome.Sharp.IconMenuItem MenuReportes;
        private FontAwesome.Sharp.IconMenuItem MenuInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblnombre;
        private FontAwesome.Sharp.IconMenuItem submenuproducto;
        private System.Windows.Forms.Label lblapellido;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem8;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem3;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem9;
        private FontAwesome.Sharp.IconMenuItem MenuNota;
        private System.Windows.Forms.Label lblapellidoM;
        private FontAwesome.Sharp.IconMenuItem ventaToolStripMenuItem;
        private FontAwesome.Sharp.IconMenuItem cajeroToolStripMenuItem;
        private FontAwesome.Sharp.IconMenuItem notaCreditoToolStripMenuItem;
        private FontAwesome.Sharp.IconMenuItem detalleNotaCreditoToolStripMenuItem;
        private System.Windows.Forms.Label lblhora;
        private System.Windows.Forms.Label lblfecha;
        private System.Windows.Forms.Timer timer1;
    }
}

