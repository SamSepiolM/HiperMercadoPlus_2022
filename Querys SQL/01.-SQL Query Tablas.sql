
CREATE DATABASE PIA_MAD_GPO01
USE PIA_MAD_GPO01

CREATE TABLE Puesto(
	IdPuesto				INT PRIMARY KEY IDENTITY,
	Nombre_Puesto			VARCHAR (15),
	FechaRegistro			DATETIME DEFAULT GETDATE()
)

CREATE TABLE Autorizacion(
	IdAutor					INT PRIMARY KEY IDENTITY,
	IdPuesto				INT NOT NULL,
	NombreMenu				VARCHAR(100),
	CONSTRAINT fk_Puesto FOREIGN KEY (IdPuesto) REFERENCES Puesto(IdPuesto)
)

CREATE TABLE Empleado(
	IdEmpleado				INT PRIMARY KEY IDENTITY(0000,1), 
	Contrasenia				VARCHAR(20) NOT NULL, 
	Nombre					VARCHAR(20) NOT NULL, 
	ApePaterno				VARCHAR(10) NOT NULL, 
	ApeMaterno				VARCHAR(10) NOT NULL, 
	CURP					VARCHAR(20) NOT NULL, 
	NumNomina				VARCHAR(10), 
	Correo					VARCHAR(30), 
	Telefono				VARCHAR(12),
	Domicilio				VARCHAR(40),
	IdPuesto				INT NOT NULL,
	Estado					BIT,
	FechaRegistro			DATETIME DEFAULT GETDATE(),
	CONSTRAINT fk_PuestoEmpleado FOREIGN KEY (IdPuesto) REFERENCES Puesto(IdPuesto)
) 

CREATE TABLE Departamento(
	Clave					INT PRIMARY KEY IDENTITY,
	Nombre					VARCHAR(20) NOT NULL,
	Descuento				INT NOT NULL,
	DescActivo				BIT,
	Devolucion				BIT
)

CREATE TABLE Producto(
	Codigo_producto			INT PRIMARY KEY IDENTITY,
	Nombre					VARCHAR(15), 
	Descripcion				VARCHAR(90),
	Unidad_medida			BIT DEFAULT 1, 
	Costo					DECIMAL(10,2) NOT NULL DEFAULT 0, 
	Precio_unitario			DECIMAL(10,2) NOT NULL DEFAULT 0, 
	FechaRegistro			DATETIME DEFAULT GETDATE(),  
	Existencia				DECIMAL(10,2) NOT NULL, 
	Stock					BIT NOT NULL,
	Punto_reorden			DECIMAL(10,2) DEFAULT 1, 
	Depto					INT NOT NULL,
	Unidad_vendida 			DECIMAL(10,2) DEFAULT 0,
	Merma 					DECIMAL(10,2) DEFAULT 0,
	FechaModificacion		DATETIME DEFAULT GETDATE(),
	DescuentoProducto		DECIMAL(10,2) DEFAULT 0,
	IdAdminMod				INT,
	FechaInicio				DATETIME DEFAULT GETDATE(),
	FechaFin				DATETIME DEFAULT GETDATE()

	CONSTRAINT fk_AdminMod FOREIGN KEY (IdAdminMod) REFERENCES Empleado(idEmpleado),
	CONSTRAINT fk_Departamento FOREIGN KEY (Depto) REFERENCES Departamento(Clave)
)

CREATE TABLE Caja(
	Id						INT PRIMARY KEY IDENTITY,  
	Num_caja				VARCHAR (10),
	Estado					BIT
) 

CREATE TABLE Venta(
	IdVenta					INT PRIMARY KEY IDENTITY,  
	IdCajero				INT NOT NULL,  
	TipoDocumento			VARCHAR(20),
	NumeroDocumento			VARCHAR(20),
	MontoPago				DECIMAL(10,2) DEFAULT 0.00,
	MontoCambio				DECIMAL(10,2) DEFAULT 0.00,
	MontoTotal				DECIMAL(10,2) DEFAULT 0.00,
	DescuentoT				DECIMAL(10,2) DEFAULT 0.00,
	IdCaja					INT NOT NULL,
	Fecha					DATETIME DEFAULT GETDATE(), 
	CONSTRAINT fk_CajeroVenta FOREIGN KEY (IdCajero) REFERENCES Empleado(idEmpleado),
	CONSTRAINT fk_CajaVenta FOREIGN KEY (IdCaja) REFERENCES Caja(Id)
) 

CREATE TABLE DetalleVenta(
	IdDetalleVenta			INT PRIMARY KEY IDENTITY,  
	IdVenta					INT NOT NULL,
	IdProducto				INT NOT NULL, 
	NombreProducto			VARCHAR(15) DEFAULT '', 
	PrecioVenta				DECIMAL(10,2) DEFAULT 0.00,
	PrecioUnitario			DECIMAL(10,2) DEFAULT 0.00,
	Cantidad				DECIMAL(10,2) DEFAULT 0.00,
	Subtotal				DECIMAL(10,2) DEFAULT 0.00,
	Descuento				DECIMAL(10,2) DEFAULT 0.00, 
	Unidad_medida			BIT,  
	Utilidad				DECIMAL(10,2) DEFAULT 0.00,
	Devuelto				BIT DEFAULT 0,
	
	CONSTRAINT fk_ProductoVenta FOREIGN KEY (IdProducto) REFERENCES Producto(Codigo_producto),
	CONSTRAINT fk_Venta FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta)
) 

CREATE TABLE MetodoPago(
	IdMetodo				INT PRIMARY KEY IDENTITY, 
	NombreMetodo			VARCHAR(10) NOT NULL,
)

CREATE TABLE DetalleMetodoPago(
	IdDetalle				INT PRIMARY KEY IDENTITY,
	IdMetodo				INT NOT NULL,
	IdVentaM				INT NOT NULL,
	MontoPago				DECIMAL(10,2) DEFAULT 0,
	CONSTRAINT fk_MetodoD FOREIGN KEY (IdMetodo) REFERENCES MetodoPago(IdMetodo),
	CONSTRAINT fk_VentaM FOREIGN KEY (IdVentaM) REFERENCES Venta(IdVenta)
)


CREATE TABLE NotaCredito(
	Num_Devolucion 			INT PRIMARY KEY IDENTITY,
	IdAdmin					INT NOT NULL,
	IdCaja					INT NOT NULL,
	IdTicketV				INT NOT NULL,
	MontoCambio				DECIMAL(10,2) NOT NULL,
	Fecha					DATETIME DEFAULT GETDATE(),
	CONSTRAINT fk_Administrador FOREIGN KEY (IdAdmin) REFERENCES Empleado(idEmpleado),
	CONSTRAINT FK_IdCaja FOREIGN KEY (IdCaja) REFERENCES Caja(Id),
	CONSTRAINT FK_IdTicketV FOREIGN KEY (IdTicketV) REFERENCES Venta(IdVenta)
)


CREATE TABLE NotaCreditoDetalle(
	Num_Recibo 				INT PRIMARY KEY IDENTITY,
	Id_Num_Dev				INT NOT NULL,
	IdProDev				INT NOT NULL, 
	Prod_regresado			VARCHAR(15) DEFAULT '', 
	Cantidad				DECIMAL(10,2) NOT NULL, 
	Subtotal				DECIMAL(10,2) NOT NULL, 
	Total					DECIMAL(10,2) NOT NULL, 
	DescuentoP				DECIMAL(10,2) DEFAULT 0.00,
	PrecioUni				DECIMAL(10,2) DEFAULT 0.00,
	Unidad_medida			BIT, 
	Merma					BIT,
	
	CONSTRAINT FK_ProdDevuelto FOREIGN KEY (IdProDev) REFERENCES Producto(Codigo_producto),
	CONSTRAINT FK_Id_Num_Dev FOREIGN KEY (Id_Num_Dev) REFERENCES NotaCredito(Num_Devolucion)
) 

CREATE TABLE Merma(
	Id_Merma				INT PRIMARY KEY IDENTITY, 
	prodNombre				VARCHAR (15),
	prodMerma				INT NOT NULL,
	IdNotCred				INT NOT NULL,
	CONSTRAINT fk_MermaNota FOREIGN KEY (IdNotCred) REFERENCES NotaCredito(Num_Devolucion)
)

/*
CREATE TABLE  Registro(
	IdRegistro				INT PRIMARY KEY IDENTITY, 
	Vendedor				INT NOT NULL, 
	IdCaja					INT NOT NULL, 
	Canasta					INT NOT NULL,
	CONSTRAINT fk_CajaAtencion FOREIGN KEY (IdCaja) REFERENCES Caja(Id),
	CONSTRAINT fk_CajeroRegistro FOREIGN KEY (Vendedor) REFERENCES Empleado(idEmpleado),
	CONSTRAINT fk_ProductoRegistro FOREIGN KEY (Canasta) REFERENCES Producto(Codigo_producto)
) 
DROP TABLE Registro
*/