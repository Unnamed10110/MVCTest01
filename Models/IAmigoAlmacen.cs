namespace MVCTest01.Models
{
    public interface IAmigoAlmacen
    {
        Amigo dameDatosAmigo(int Id);
        List<Amigo> DameTodosLosAMigos();
        Amigo nuevo(Amigo amigo);

        Amigo modificar(Amigo modificarAmigo);

        Amigo borrar(int id);
    }
}
