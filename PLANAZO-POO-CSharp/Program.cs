using System.Globalization;
using PLANAZO.Modelos;
using PLANAZO.Servicios;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("es-CO");
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("es-CO");
var gestor = new GestorReservas();

while (true)
{
    Console.WriteLine("\n================================");
    Console.WriteLine("        PLANAZO - EVENTOS");
    Console.WriteLine("================================");
    Console.WriteLine("1. Ver eventos disponibles");
    Console.WriteLine("2. Consultar evento");
    Console.WriteLine("3. Reservar entradas");
    Console.WriteLine("4. Ver reservas realizadas");
    Console.WriteLine("5. Cancelar una reserva");
    Console.WriteLine("6. Ver resumen de reservas");
    Console.WriteLine("7. Consultar reservas de una persona (reto)");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opción: ");
    var opcion = Console.ReadLine()?.Trim();

    try
    {
        switch (opcion)
        {
            case "1":
                foreach (var evento in gestor.Eventos) Console.WriteLine(evento);
                break;
            case "2":
                Console.Write("Identificador del evento: ");
                var idEventoConsulta = LeerEntero();
                Console.WriteLine(gestor.ObtenerEvento(idEventoConsulta));
                Console.WriteLine($"Precio final por entrada: {gestor.ObtenerEvento(idEventoConsulta).CalcularPrecioFinal():C0}");
                break;
            case "3":
                Console.Write("Nombre de la persona: "); var nombre = Console.ReadLine() ?? "";
                Console.Write("Identificador del evento: "); var idEvento = LeerEntero();
                Console.Write("Cantidad de entradas: "); var cantidad = LeerEntero();
                var reserva = gestor.CrearReserva(nombre, idEvento, cantidad);
                Console.WriteLine("Reserva creada correctamente:"); Console.WriteLine(reserva);
                break;
            case "4":
                if (gestor.Reservas.Count == 0) Console.WriteLine("Todavía no hay reservas.");
                foreach (var r in gestor.Reservas) Console.WriteLine(r);
                break;
            case "5":
                Console.Write("Identificador de la reserva: "); var idReserva = LeerEntero();
                gestor.CancelarReserva(idReserva);
                Console.WriteLine("Reserva cancelada. Los cupos fueron devueltos.");
                break;
            case "6":
                Console.WriteLine($"Reservas activas: {gestor.ReservasActivas}");
                Console.WriteLine($"Entradas reservadas: {gestor.EntradasActivas}");
                Console.WriteLine($"Valor total activo: {gestor.ValorActivo:C0}");
                break;
            case "7":
                Console.Write("Nombre de la persona: "); var nombreBuscar = Console.ReadLine() ?? "";
                var reservasUsuario = gestor.ReservasDeUsuario(nombreBuscar).ToList();
                if (reservasUsuario.Count == 0) Console.WriteLine("No hay reservas registradas para esa persona.");
                else foreach (var r in reservasUsuario) Console.WriteLine(r);
                break;
            case "0": Console.WriteLine("Gracias por usar PLANAZO."); return;
            default: Console.WriteLine("Opción no válida. Intenta de nuevo."); break;
        }
    }
    catch (FormatException) { Console.WriteLine("Entrada inválida: escribe un número entero donde se solicita."); }
    catch (ArgumentException ex) { Console.WriteLine($"Dato incorrecto: {ex.Message}"); }
    catch (InvalidOperationException ex) { Console.WriteLine($"No se pudo completar la operación: {ex.Message}"); }
    catch (KeyNotFoundException ex) { Console.WriteLine(ex.Message); }
    catch (Exception ex) { Console.WriteLine($"Ocurrió un error controlado: {ex.Message}"); }
}

static int LeerEntero()
{
    var entrada = Console.ReadLine();
    if (!int.TryParse(entrada, out var numero)) throw new FormatException();
    return numero;
}
