USE PIA_MAD_GPO01;
--Puesto
EXEC sp_addextendedproperty
	@name = N'MS_Description',	@value = 'Codigo identificador del puesto (Entero, llave primaria)',
    @level0type = N'Schema',	@level0name = 'dbo',
    @level1type = N'Table',		@level1name = 'Puesto',
    @level2type = N'Column',	@level2name = 'IdPuesto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Nombre del puesto (Maximo 15 caracteres, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Puesto',
    @level2type = N'Column', @level2name = 'Nombre_Puesto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Fecha de registro del puesto',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Puesto',
    @level2type = N'Column', @level2name = 'FechaRegistro'
GO

--Autorizacion
EXEC sp_addextendedproperty
	@name = N'MS_Description',	@value = 'Codigo identificador de la autorizacion (Entero, llave primaria)',
    @level0type = N'Schema',	@level0name = 'dbo',
    @level1type = N'Table',		@level1name = 'Autorizacion',
    @level2type = N'Column',	@level2name = 'IdAutor'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Codigo identificador del puesto (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Autorizacion',
    @level2type = N'Column', @level2name = 'IdPuesto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Menus a los que los usuarios tienen acceso (segun su puesto)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Autorizacion',
    @level2type = N'Column', @level2name = 'NombreMenu'
GO

--EMPLEADO
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero de empleado (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'IdEmpleado'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Contrasenia del cajero (Maximo 20 caracteres, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'Contrasenia'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Nombre de empleado (Maximo 20 caracteres, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'Nombre'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Apellino Paterno de empleado (Maximo 10 caracteres, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'ApePaterno'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Apellido Materno de empleado (Maximo 10 caracteres, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'ApeMaterno'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Codigo unico de registro de poblacion del empleado (Maximo 20 caracteres, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'CURP'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero de registro financiero del sueldo de empleado (Numero Entero, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'NumNomina'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Email del empleado (Maximo 30 caracteres)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'Correo'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero de telefono del empleado (Maximo 12 caracteres)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'Telefono'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Domicilio del empleado (Maximo 40 caracteres)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'Domicilio'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra si el usuario esta activo o inactivo (Booleano, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'Estado'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Clave del puesto del empleado (Entero, no permite nulos, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'IdPuesto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Fecha en que el empleado ingreso a trabajar (FECHA DEFAULT:FECHA ACTUAL)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Empleado',
    @level2type = N'Column', @level2name = 'FechaRegistro'
GO

--DEPARTAMENTO
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Clave del departamento (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Departamento',
    @level2type = N'Column', @level2name = 'Clave'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Nombre del departamento (Maximo 20 caracteres, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Departamento',
    @level2type = N'Column', @level2name = 'Nombre'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Descuento de los productos del departamento (Entero, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Departamento',
    @level2type = N'Column', @level2name = 'Descuento'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra si el descuento de los productos del departamento esta activo (Booleano con activo como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Departamento',
    @level2type = N'Column', @level2name = 'DescActivo'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra si los productos del departamento tienen devolucion (Booleano con activo como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Departamento',
    @level2type = N'Column', @level2name = 'Devolucion'
GO

--PRODUCTO
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador del producto (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Codigo_producto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Nombre del producto (Maximo 15 caracteres)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Nombre'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Detalle del producto (Maximo 90 caracteres)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Descripcion'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Unidad en la que se contabiliza el producto (Bit no permite Nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Unidad_medida'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Precio cuanto se compro el producto al provedor (Entero, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Costo'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Precio el cual se vende el producto (Entero, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Precio_unitario'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Fecha en la cual se dio de alta el producto (Fecha Default:Fecha Actual)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'FechaRegistro'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero de productos en almacen (Entero, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Existencia'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Si hay productos disponibles para la venta o no (Bool, con activo como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Stock'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Cantidad de productos que debe haber en existencia para resurtir (Entero, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Punto_reorden'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero de departamento al cual pertenece el producto (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Depto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Cantidad de productos que fueron vendidos(Entero, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Unidad_vendida'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Cantidad de productos devueltos (Entero, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'Merma'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Guarda la fecha de modificacion(Datetime, fecha actual)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'fechaModificacion'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Guarda el descuento aplicado al producto (Decimal)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'DescuentoProducto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Guarda la clave del empleado que modifico el producto (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Producto',
    @level2type = N'Column', @level2name = 'IdAdminMod'
GO

--CAJA
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador de la caja  (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Caja',
    @level2type = N'Column', @level2name = 'Id'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero de la caja  (Varchar, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Caja',
    @level2type = N'Column', @level2name = 'Num_caja'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra el estado de la caja (Bool, con activo como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Caja',
    @level2type = N'Column', @level2name = 'Estado'
GO

--MetodoPago
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador del metodo de pago (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'MetodoPago',
    @level2type = N'Column', @level2name = 'IdMetodo'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Nombre del metodo de pago (Varchar, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'MetodoPago',
    @level2type = N'Column', @level2name = 'NombreMetodo'
GO

--DetalleMetodoPago
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador del detalle de metodo de pago (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleMetodoPago',
    @level2type = N'Column', @level2name = 'IdDetalle'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Relaciona la tabla con la tabla MetodoPago (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleMetodoPago',
    @level2type = N'Column', @level2name = 'IdMetodo'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Relaciona la tabla con la tabla Venta (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleMetodoPago',
    @level2type = N'Column', @level2name = 'IdVentaM'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Trae el total a pagar con este metodo de pago (Decimal, n permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleMetodoPago',
    @level2type = N'Column', @level2name = 'MontoPago'
GO

--Venta
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Identificador del numero de la venta (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'IdVenta'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Identificador del cajero que realizo la venta (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'IdCajero'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Determina si el documento es un ticket o una factura (Varchar, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'TipoDocumento'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Identificador del ticket/factura (Varchar, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'NumeroDocumento'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Monto a pagar por los productos de manera individual (Decimal, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'MontoPago'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Cambio que se le da al cliente despues de pagar (Decimal, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'MontoCambio'
Go

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Monto a pagar por los productos (Decimal, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'MontoTotal'
Go

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Es el descuento total de los productos (Decimal, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'DescuentoT'
Go

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Es el id de la caja (int, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'IdCaja'
Go

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Es la fecha en que se vendio (DATETIME)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Venta',
    @level2type = N'Column', @level2name = 'Fecha'
Go

--DetalleVenta
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Identificador del detalle de venta (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'IdDetalleVenta'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Identificador que relaciona esta tabla con la tabla venta (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'IdVenta'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Identificador que muestra los productos vendidos (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'IdProducto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra el nombre del producto (Decimal, con 0 como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'NombreProducto'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra el total del pago de los articulos (Decimal, con 0 como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'PrecioVenta'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra el precio de cada uno de los articulos (Decimal)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'PrecioUnitario'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra la cantidad de productos vendidos (Int, no puede ser nulo)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'Cantidad'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra el precio de los productos por modelos individuales (Decimal, con 0 como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'Subtotal'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Es la cantida del descueto que tiene el producto por su departamento (Decimal, con 0 como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'Descuento'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Si el producto se vende por unidad o por kilo (BIT)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'Unidad_medida'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Muestra la diferencia entre compra y venta de los productos (Decimal, con 0 como default)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'Utilidad'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Permite ver si el producto ha sido devuelto(Booleana no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'DetalleVenta',
    @level2type = N'Column', @level2name = 'Devuelto'
GO

--Nota de credito
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador del recibo (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCredito',
    @level2type = N'Column', @level2name = 'Num_Devolucion'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador del administrador(Entero, no permite nulos, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCredito',
    @level2type = N'Column', @level2name = 'IdAdmin'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador de la caja(Entero, no permite nulos, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCredito',
    @level2type = N'Column', @level2name = 'IdCaja'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador del ticket(Entero, no permite nulos, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCredito',
    @level2type = N'Column', @level2name = 'IdTicketV'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Total del cambio(Decimal, no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCredito',
    @level2type = N'Column', @level2name = 'MontoCambio'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Fecha de la devolucion(DATETIME)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCredito',
    @level2type = N'Column', @level2name = 'Fecha'
GO



--DetalleNotaCredito
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador del detalle de nota (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'Num_Recibo'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Relacion con la tabla Nota de credito (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'ID_Num_Dev'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador del producto devuelto (Entero, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'IdProDev'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Nombre del producto devuelto (Varchar de 15 espacios)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'Prod_regresado'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Cantidad de productos devueltos (Int no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'Cantidad'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Precio del producto regresado(Decimal no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'Subtotal'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Precio total de los productos regresados (Decimal no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'Total'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Descuento total de los productos regresados (Decimal no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'DescuentoP'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Precio unitario de los productos regresados (Decimal no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'PrecioUni'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Si se vende por unidad o por kilo (Bit)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'Unidad_medida'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Si se devolvio con merma o no (Bit)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'NotaCreditoDetalle',
    @level2type = N'Column', @level2name = 'Merma'
GO

--Merma
EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Numero identificador de la merma (Entero, llave primaria)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Merma',
    @level2type = N'Column', @level2name = 'Id_Merma'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Nombre del producto en merma (Varchar, con un maximo de 15 caracteres)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Merma',
    @level2type = N'Column', @level2name = 'prodNombre'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Cantidad del producto en merma (Int, que no permite nulos)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Merma',
    @level2type = N'Column', @level2name = 'prodMerma'
GO

EXEC sp_addextendedproperty
	@name = N'MS_Description', @value = 'Llave que relaciona la tabla de merma con nota de credito (Int, llave foranea)',
    @level0type = N'Schema', @level0name = 'dbo',
    @level1type = N'Table', @level1name = 'Merma',
    @level2type = N'Column', @level2name = 'IdNotCred'
GO

--Consultar Diccionario de Datos
SELECT IC.COLUMN_NAME, IC.Data_TYPE, EP.[Value] as [MS_Description], IKU.CONSTRAINT_NAME, ITC.CONSTRAINT_TYPE, IC.IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS IC
INNER JOIN sys.columns sc ON OBJECT_ID(QUOTENAME(IC.TABLE_SCHEMA) + '.' +
QUOTENAME(IC.TABLE_NAME)) = sc.[object_id] AND IC.COLUMN_NAME = sc.name
LEFT OUTER JOIN sys.extended_properties EP ON sc.[object_id] = EP.major_id AND
sc.[column_id] = EP.minor_id AND EP.name = 'MS_Description' AND EP.class = 1
LEFT OUTER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE IKU ON IKU.COLUMN_NAME = IC.COLUMN_NAME and IKU.TABLE_NAME = IC.TABLE_NAME and IKU.TABLE_CATALOG = IC.TABLE_CATALOG
LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ITC ON ITC.TABLE_NAME = IKU.TABLE_NAME and ITC.CONSTRAINT_NAME = IKU.CONSTRAINT_NAME
WHERE IC.TABLE_CATALOG = 'base_datos'
and IC.TABLE_SCHEMA = 'dbo'
and IC.TABLE_NAME = 'Table'
order by IC.ORDINAL_POSITION


SELECT schemas.name AS SchemaName ,all_objects.name AS TableName, syscolumns.id AS ColumnId ,syscolumns.name AS ColumnName, systypes.name AS DataType
,syscolumns.length AS CharacterMaximumLength ,sysproperties.[value] AS ColumnDescription ,syscomments.TEXT AS ColumnDefault, syscolumns.isnullable AS IsNullable
FROM syscolumns INNER JOIN sys.systypes ON syscolumns.xtype= systypes.xtype
LEFT JOIN sys.all_objects ON syscolumns.id=all_objects.[object_id]
LEFT OUTER JOIN sys.extended_properties AS sysproperties ON (sysproperties.minor_id = syscolumns.colid AND sysproperties.major_id = syscolumns.id)
LEFT OUTER JOIN sys.syscomments ON syscolumns.cdefault = syscomments.id
LEFT OUTER JOIN sys.schemas ON schemas.[schema_id] = all_objects.[schema_id]
WHERE syscolumns.id IN (SELECT id
FROM sysobjects
WHERE xtype = 'U') AND (systypes.name<> 'sysname')
