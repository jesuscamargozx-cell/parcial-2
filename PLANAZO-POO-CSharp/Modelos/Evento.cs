namespace PLANAZO.Modelos;

public abstract class Evento
{
    public int Id { get; }
    public string Nombre { get; private set; }
    public DateTime Fecha { get; private set; }
    public decimal PrecioBase { get; private set; }
    public int CapacidadMaxima { get; }
    public int CuposDisponibles { get; private set; }

    protected Evento(int id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
    {
        if (id <= 0) throw new ArgumentException("El identificador debe ser positivo.");
        if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.");
        if (precioBase < 0) throw new ArgumentException("El precio base no puede ser negativo.");
        if (capacidadMaxima <= 0) throw new ArgumentException("La capacidad debe ser mayor que cero.");
        Id = id; Nombre = nombre.Trim(); Fecha = fecha; PrecioBase = precioBase;
        CapacidadMaxima = capacidadMaxima; CuposDisponibles = capacidadMaxima;
    }

    public abstract decimal CalcularPrecioFinal();

    public void ReservarCupos(int cantidad)
    {
        if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (cantidad > CuposDisponibles) throw new InvalidOperationException("No hay suficientes cupos disponibles.");
        CuposDisponibles -= cantidad;
    }

    public void DevolverCupos(int cantidad)
    {
        if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (CuposDisponibles + cantidad > CapacidadMaxima) throw new InvalidOperationException("La devolución supera la capacidad máxima.");
        CuposDisponibles += cantidad;
    }

    public override string ToString() => $"[{Id}] {Nombre} | {GetType().Name} | {Fecha:dd/MM/yyyy HH:mm} | Precio: {CalcularPrecioFinal():C0} | Cupos: {CuposDisponibles}/{CapacidadMaxima}";
}
