
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ClasesEjercicioExamen.Data;
using ClasesEjercicioExamen.Repository;
using ClasesEjercicioExamen.Models;


const string connectionString = "Server=LAPTOP-J7C6C098\\SQLEXPRESS;Database=ExamenVentasDB;Trusted_Connection=True;";

// regsitro de servicios
var serviceProvider = new ServiceCollection()
    // registrar el DbContext
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString))

    // registrar todos los Repositorios
    .AddTransient<IClienteRepository, ClienteRepository>()
    .AddTransient<IProductoRepository, ProductoRepository>()
    .AddTransient<IVentaRepository, VentaRepository>()

    .BuildServiceProvider();

// instancias de los repositorios
var clienteRepo = serviceProvider.GetRequiredService<IClienteRepository>();
var productoRepo = serviceProvider.GetRequiredService<IProductoRepository>();
var ventaRepo = serviceProvider.GetRequiredService<IVentaRepository>();

// menú y control de flujo)
bool salir = false;

while (!salir)
{
    MostrarMenu();
    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            RegistrarNuevoCliente(clienteRepo);
            break;
        case "2":
            RegistrarNuevoProducto(productoRepo);
            break;
        case "3":
            RegistrarNuevaVenta(clienteRepo, productoRepo, ventaRepo);
            break;
        case "4":
            MostrarReporteVentasPorProducto(ventaRepo);
            break;
        case "5":
            salir = true;
            break;
        default:
            Console.WriteLine("\nOpción no válida. Presione cualquier tecla...");
            Console.ReadKey();
            break;
    }
}

void MostrarMenu()
{
    Console.Clear();
    Console.WriteLine("---------------------------------------------");
    Console.WriteLine(" SISTEMA DE GESTIÓN DE VENTAS ");
    Console.WriteLine("---------------------------------------------");
    Console.WriteLine("1. Registrar Nuevo Cliente");
    Console.WriteLine("2. Registrar Nuevo Producto");
    Console.WriteLine("3. Registrar Nueva Venta (asociando Cliente y Productos)");
    Console.WriteLine("4. Mostrar Reporte de Ventas por Producto");
    Console.WriteLine("5. Salir");
    Console.Write("\nSeleccione una opción: ");
}

void RegistrarNuevoCliente(IClienteRepository repo)
{
    Console.Clear();
    Console.WriteLine("--- REGISTRO DE NUEVO CLIENTE ---");
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine();
    Console.Write("Email: ");
    string email = Console.ReadLine();

    var nuevoCliente = new Cliente { Nombre = nombre, Email = email };
    repo.AgregarCliente(nuevoCliente);

    Console.WriteLine($"\nCliente '{nombre}' registrado con ID: {nuevoCliente.Id}");
    Console.ReadKey();
}

void RegistrarNuevoProducto(IProductoRepository repo)
{
    Console.Clear();
    Console.WriteLine("--- REGISTRO DE NUEVO PRODUCTO ---");
    Console.Write("Nombre del Producto: ");
    string nombre = Console.ReadLine();
    Console.Write("Precio (ej: 15.50): ");
    decimal precio = decimal.Parse(Console.ReadLine());

    var nuevoProducto = new Producto { Nombre = nombre, Precio = precio };
    repo.AgregarProducto(nuevoProducto);

    Console.WriteLine($"\nProducto '{nombre}' registrado con ID: {nuevoProducto.Id}");
    Console.ReadKey();
}

void RegistrarNuevaVenta(IClienteRepository cRepo, IProductoRepository pRepo, IVentaRepository vRepo)
{
    Console.Clear();
    Console.WriteLine("--- REGISTRO DE NUEVA VENTA ---");

    //obtener cliente
    Console.Write("Ingrese el ID del Cliente: ");
    if (!int.TryParse(Console.ReadLine(), out int clienteId))
    {
        Console.WriteLine("ID de cliente inválido.");
        Console.ReadKey();
        return;
    }

    var cliente = cRepo.ObtenerPorId(clienteId);
    if (cliente == null)
    {
        Console.WriteLine("ERROR: Cliente no encontrado.");
        Console.ReadKey();
        return;
    }
    Console.WriteLine($"Cliente asociado: {cliente.Nombre}");

    //crear detalles de venta asociando productos
    var nuevaVenta = new Venta { ClienteId = clienteId };
    decimal totalVenta = 0;

    bool agregarMas = true;
    while (agregarMas)
    {
        Console.WriteLine("\n--- AGREGAR PRODUCTO ---");
        Console.Write("ID del Producto a vender: ");
        if (!int.TryParse(Console.ReadLine(), out int productoId))
        {
            Console.WriteLine("ID de producto inválido.");
            continue;
        }

        var producto = pRepo.ObtenerPorId(productoId);
        if (producto == null)
        {
            Console.WriteLine("Producto no encontrado. Intente de nuevo.");
            continue;
        }

        Console.Write($"Cantidad de '{producto.Nombre}' (Precio: {producto.Precio:C}): ");
        if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
        {
            Console.WriteLine("Cantidad inválida.");
            continue;
        }

        // calculo subtotal
        decimal subtotalDetalle = cantidad * producto.Precio;
        totalVenta += subtotalDetalle;

        // crear el detalle y añadirlo a la venta
        nuevaVenta.Detalles.Add(new VentaDetalle
        {
            ProductoId = producto.Id,
            Cantidad = cantidad,
            PrecioUnitario = producto.Precio // se guarda el precio al momento de la venta
        });

        Console.Write("¿Desea agregar otro producto? (s/n): ");
        agregarMas = Console.ReadLine()?.ToLower() == "s";
    }

    // registra en la base de datos
    if (nuevaVenta.Detalles.Count > 0)
    {
        nuevaVenta.Total = totalVenta;
        vRepo.RegistrarVenta(nuevaVenta);
        Console.WriteLine($"\n✅ Venta registrada con éxito. Total: {totalVenta:C}");
    }
    else
    {
        Console.WriteLine("Venta cancelada. No se agregaron productos.");
    }

    Console.ReadKey();
}

void MostrarReporteVentasPorProducto(IVentaRepository repo)
{
    Console.Clear();
    Console.WriteLine("--- REPORTE DE VENTAS POR PRODUCTO ---");

    var detalles = repo.ObtenerReporteVentasPorProducto();

    if (detalles.Count == 0)
    {
        Console.WriteLine("No hay detalles de venta registrados.");
        Console.ReadKey();
        return;
    }

    // agruoa  los detalles obtenidos por el nombre del producto
    var reporteAgrupado = detalles
        .GroupBy(d => d.Producto.Nombre)
        .Select(g => new
        {
            Producto = g.Key,
            CantidadTotal = g.Sum(d => d.Cantidad),
            IngresoTotal = g.Sum(d => d.Cantidad * d.PrecioUnitario)
        })
        .OrderByDescending(r => r.IngresoTotal);

    Console.WriteLine("{0,-20} {1,-10} {2,15}", "PRODUCTO", "CANTIDAD", "INGRESOS TOTALES");
    Console.WriteLine("-------------------- ---------- ---------------");

    foreach (var item in reporteAgrupado)
    {
        Console.WriteLine("{0,-20} {1,-10} {2,15:C}", item.Producto, item.CantidadTotal, item.IngresoTotal);
    }

    Console.ReadKey();
}