namespace PLANAZO.Modelos;

public sealed class Taller : Evento
{
    public Taller(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima) { }
    public override decimal CalcularPrecioFinal() => PrecioBase * 0.90m;
}
