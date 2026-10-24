namespace PLANAZO.Modelos;

public sealed class Cine : Evento
{
    public Cine(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(id, nombre, fecha, precioBase, capacidadMaxima) { }
    public override decimal CalcularPrecioFinal() => PrecioBase;
}
