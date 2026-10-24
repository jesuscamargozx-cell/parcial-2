namespace PLANAZO.Modelos;

// Reto adicional: aplica un recargo del 8 % sobre el precio base.
public sealed class EventoDeportivo : Evento
{
    public EventoDeportivo(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima) { }
    public override decimal CalcularPrecioFinal() => PrecioBase * 1.08m;
}
