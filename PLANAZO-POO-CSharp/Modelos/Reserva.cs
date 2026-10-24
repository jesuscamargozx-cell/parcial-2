namespace PLANAZO.Modelos;

public enum EstadoReserva { Activa, Cancelada }

public sealed class Reserva
{
    public int Id { get; }
    public Usuario Usuario { get; }
    public Evento Evento { get; }
    public int CantidadEntradas { get; }
    public decimal ValorTotal { get; }
    public EstadoReserva Estado { get; private set; }

    public Reserva(int id, Usuario usuario, Evento evento, int cantidadEntradas)
    {
        if (id <= 0) throw new ArgumentException("El identificador de reserva debe ser positivo.");
        ArgumentNullException.ThrowIfNull(usuario);
        ArgumentNullException.ThrowIfNull(evento);
        if (cantidadEntradas <= 0) throw new ArgumentException("La cantidad de entradas debe ser mayor que cero.");
        // Se descuentan los cupos antes de confirmar la reserva.
        evento.ReservarCupos(cantidadEntradas);
        Id = id; Usuario = usuario; Evento = evento; CantidadEntradas = cantidadEntradas;
        ValorTotal = evento.CalcularPrecioFinal() * cantidadEntradas;
        Estado = EstadoReserva.Activa;
    }

    public void Cancelar()
    {
        if (Estado == EstadoReserva.Cancelada) throw new InvalidOperationException("Esta reserva ya fue cancelada.");
        Evento.DevolverCupos(CantidadEntradas);
        Estado = EstadoReserva.Cancelada;
    }

    public override string ToString() => $"Reserva #{Id} | {Usuario.Nombre} | {Evento.Nombre} | Entradas: {CantidadEntradas} | Total: {ValorTotal:C0} | Estado: {Estado}";
}
