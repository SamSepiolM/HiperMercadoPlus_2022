USE PIA_MAD_GPO01
GO

CREATE VIEW [ReporteVentas] AS
SELECT Convert(DATE, V.Fecha) AS Fecha, SUM(Cantidad) AS Cantidad, SUM(Subtotal) AS Subtotal, SUM(DV.Descuento) AS Descuento, SUM(PrecioVenta) AS PrecioVenta, PrecioUnitario, SUM(Utilidad) AS Utilidad, Precio_unitario, Codigo_producto, Clave, D.Nombre, C.Id, C.Num_caja FROM DetalleVenta DV
INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto INNER JOIN Departamento D ON D.Clave=P.Depto INNER JOIN Venta V ON V.IdVenta=DV.IdVenta INNER JOIN Caja C ON C.Id = V.IdCaja
GROUP BY Convert(DATE, V.Fecha), D.Nombre, P.Codigo_producto, DV.PrecioUnitario, P.Precio_unitario, D.Clave, C.Id, C.Num_caja
GO

CREATE VIEW [ReporteCajero] AS
SELECT Convert(DATE, V.Fecha) AS Fecha, E.Nombre, E.ApePaterno, E.ApeMaterno, D.Nombre[Nombre_Depto], SUM(DV.Cantidad) AS Cantidad, SUM(DV.PrecioVenta) AS PrecioVenta, SUM(DV.Utilidad) AS Utilidad, SUM(DV.Subtotal) AS Subtotal, D.Clave FROM DetalleVenta DV
INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto INNER JOIN Departamento D ON D.Clave=P.Depto INNER JOIN Venta V ON V.IdVenta= DV.IdVenta INNER JOIN Empleado E ON E.IdEmpleado= V.IdCajero
GROUP BY Convert(DATE, V.Fecha), E.Nombre, E.ApePaterno, E.ApeMaterno, D.Nombre, P.Codigo_producto, D.Clave
GO

CREATE VIEW [BusquedaFolioVenta] AS
SELECT V.IdVenta, V.MontoTotal, V.MontoPago, V.MontoCambio, V.DescuentoT, DV.IdDetalleVenta, DV.NombreProducto, DV.Cantidad, DV.Subtotal, DV.PrecioVenta, DV.PrecioUnitario, DV.Descuento, V.Fecha, DV.IdProducto, DV.Devuelto, C.Id, C.Num_caja FROM Venta V
INNER JOIN DetalleVenta DV ON V.IdVenta=DV.IdVenta
INNER JOIN Caja C ON V.IdCaja=C.Id INNER JOIN Producto P ON DV.IdProducto=P.Codigo_producto
GO

CREATE VIEW [BusquedaAvanzadaVenta] AS
SELECT V.IdVenta, V.Fecha, C.Id, C.Num_caja FROM Venta V
INNER JOIN Caja C ON V.IdCaja=C.Id
GO

CREATE VIEW [ListarFolioMetodoPago] AS
SELECT MP.IdMetodo, MP.NombreMetodo, DMP.MontoPago, IdVenta FROM DetalleMetodoPago DMP
INNER JOIN MetodoPago MP ON DMP.IdMetodo = MP.IdMetodo
INNER JOIN VENTA V ON V.IdVenta = DMP.IdVentaM
GO


CREATE VIEW [BusquedaFolioNota] AS
SELECT V.IdVenta, V.Fecha, DV.NombreProducto, DV.Cantidad, DV.IdProducto, DV.Devuelto, C.Id, C.Num_caja FROM Venta V
INNER JOIN DetalleVenta DV ON V.IdVenta=DV.IdVenta
INNER JOIN CAJA C ON V.IdCaja=C.Id INNER JOIN Producto P ON P.Codigo_producto=DV.IdProducto
GO

CREATE VIEW [BusquedaFolioNotaD] AS
SELECT NC.Num_Devolucion, NC.IdTicketV, ND.Num_Recibo, ND.Prod_regresado, ND.Cantidad, ND.Subtotal, ND.Total, ND.PrecioUni, ND.DescuentoP, NC.Fecha, ND.IdProDev, C.Id, C.Num_caja FROM NotaCredito NC
INNER JOIN NotaCreditoDetalle ND ON ND.Id_Num_Dev= NC.Num_Devolucion
INNER JOIN Caja C ON NC.IdCaja= C.Id INNER JOIN Producto P ON ND.IdProDev=P.Codigo_producto
GO

CREATE VIEW [BusquedaAvanzadaNota] AS
SELECT NC.Num_Devolucion, NC.Fecha, C.Id, C.Num_caja FROM NotaCredito NC
INNER JOIN Caja C ON NC.IdCaja= C.Id
GO

CREATE VIEW [BusquedaCantidadExisteNota] AS
SELECT  IdTicketV, IdProDev ,SUM(Cantidad) AS Cantidad FROM NotaCreditoDetalle NCD
INNER JOIN NotaCredito NC ON NCD.Id_Num_Dev = NC.Num_Devolucion
GROUP BY IdProDev, IdTicketV
GO
