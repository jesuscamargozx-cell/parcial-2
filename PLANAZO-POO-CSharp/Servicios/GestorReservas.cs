using PLANAZO.Modelos;

namespace PLANAZO.Servicios;

public sealed class GestorReservas
{
    private readonly List<Evento> _eventos = new();
    private readonly List<Reserva> _reservas = new();
    private readonly List<Usuario> _usuarios = new();
    private int _siguienteReserva = 1;
    private int _siguienteUsuario = 1;

    public IReadOnlyList<Evento> Eventos => _eventos.AsReadOnly();
    public IReadOnlyList<Reserva> Reservas => _reservas.AsReadOnly();

    public GestorReservas() => CargarEventosIniciales();

    private void CargarEventosIniciales()
    {
        var fecha = DateTime.Today.AddDays(7).AddHours(18);
        _eventos.AddRange(new Evento[] {
            new Cine(1, "Aventura espacial", fecha, 18000m, 50),
            new Cine(2, "Misterio en la ciudad", fecha.AddDays(2), 20000m, 40),
            new Concierto(3, "Noche de rock", fecha.AddDays(4), 50000m, 100),
            new Concierto(4, "Festival acústico", fecha.AddDays(6), 40000m, 80),
            new Taller(5, "Fotografía para principiantes", fecha.AddDays(8), 30000m, 25),
            new Taller(6, "Pintura creativa", fecha.AddDays(10), 25000m, 20),
            new EventoDeportivo(7, "Torneo de exhibición", fecha.AddDays(12), 35000m, 60)
        });
    }

    public Evento ObtenerEvento(int id) => _eventos.FirstOrDefault(e => e.Id == id)
        ?? throw new KeyNotFoundException($"No existe un evento con el identificador {id}.");

    public Reserva CrearReserva(string nombreUsuario, int idEvento, int cantidad)
    {
        if (string.IsNullOrWhiteSpace(nombreUsuario)) throw new ArgumentException("Escribe el nombre de la persona.");
        if (cantidad <= 0) throw new ArgumentException("La cantidad de entradas debe ser mayor que cero.");
        var evento = ObtenerEvento(idEvento);
        var usuario = _usuarios.FirstOrDefault(u => u.Nombre.Equals(nombreUsuario.Trim(), StringComparison.OrdinalIgnoreCase));
        if (usuario is null) { usuario = new Usuario(_siguienteUsuario++, nombreUsuario); _usuarios.Add(usuario); }
        var reserva = new Reserva(_siguienteReserva++, usuario, evento, cantidad);
        _reservas.Add(reserva);
        return reserva;
    }

    public Reserva ObtenerReserva(int id) => _reservas.FirstOrDefault(r => r.Id == id)
        ?? throw new KeyNotFoundException($"No existe una reserva con el identificador {id}.");

    public void CancelarReserva(int id) => ObtenerReserva(id).Cancelar();

    public IEnumerable<Reserva> ReservasDeUsuario(string nombre) => _reservas
        .Where(r => r.Usuario.Nombre.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase));

    public int ReservasActivas => _reservas.Count(r => r.Estado == EstadoReserva.Activa);
    public int EntradasActivas => _reservas.Where(r => r.Estado == EstadoReserva.Activa).Sum(r => r.CantidadEntradas);
    public decimal ValorActivo => _reservas.Where(r => r.Estado == EstadoReserva.Activa).Sum(r => r.ValorTotal);
}
