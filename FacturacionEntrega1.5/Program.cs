using Facturacion1._5.Data.Utils;
using Facturacion1._5.Domain;

var clienterepo = new ClienteRepository();
var articulorepo = new ArticuloRepository();
var formaPagoRepo = new FormaPagoRepository();
var facturaRepo = new FacturaRepository();
var detalleFacturaRepo = new DetalleFacturaRepository();

//obtener clientes
Console.WriteLine("Clientes disponibles");
var clientes = clienterepo.GetAll();
foreach (var cliente in clientes)
{
    Console.WriteLine("Id: " + cliente.Id + " Nombre: " + cliente.Nombre + " Apelliedo: " + cliente.Apellido + " Dni: " + cliente.Dni);
}

//obtener Articulos
Console.WriteLine("\nArticulos disponibles:");
var articulos =  articulorepo.GetAll();
foreach(var articulo in articulos)
{
    Console.WriteLine("Id: " + articulo.Id + " Nombre: " + articulo.Nombre + " Precio Unitario: " + articulo.PrecioUnitario);
}

//Actualizar Articulo
Console.WriteLine("\nActualizando un Articulo");
var articuloActualizar = articulos.First();
articuloActualizar.PrecioUnitario = 2000;
bool actualizado = articulorepo.Update(articuloActualizar);
Console.WriteLine("Actualizacion: "+actualizado);

//Borrar Articulo
Console.WriteLine("\nBorrar Articuoo");
int idEliminar = 1;
bool borrado = articulorepo.Delete(idEliminar);
Console.WriteLine("Articulo borrado: " +borrado);

//mostrar Forma de pago
Console.WriteLine("\nFormas de pago disponible: ");
var formasPago = formaPagoRepo.GetAll();
foreach (var formaPago in formasPago)
{
    Console.WriteLine("Id: " + formaPago.Id + " Nombre: " + formaPago.Nombre);
}

//Crear nueva factura
Console.WriteLine("\nCreando una nueva factura:");
var factura = new Factura
{
    Fecha = DateTime.Now,
    IdFormaPago = formasPago.First().Id,
    ClienteId = clientes.First().Id,
    Detalles = new List<DetalleFactura>
    {
        new DetalleFactura
        {
            Cantidad = 2,
            Articulo = articulos.First(),
            Precio = articulos.First().PrecioUnitario
        }
    }
};
bool facturaCreada = facturaRepo.Add(factura);
Console.WriteLine("Factura creada: " + facturaCreada);

// Obtener todas las facturas
Console.WriteLine("\nFacturas Disponibles:");
var facturas = facturaRepo.GetAll();
foreach (var fa in facturas)
{
    Console.WriteLine("Id: " + fa.Id + " Fecha: " + fa.Fecha + " Forma de Pago: " + fa.IdFormaPago + " Cliente: " + fa.ClienteId + " Total: " + fa.Total());
    // Detalles de la factura
    Console.WriteLine("Detalles: ");
    foreach (var detalle in fa.Detalles)
    {
        Console.WriteLine("Articulo: " + detalle.Articulo.Id + " Nombre: " + detalle.Articulo.Nombre + " Cantidad: " + detalle.Cantidad + " Precio: " + detalle.Precio + " Subtotal: " + detalle.SubTotal());
    }
}