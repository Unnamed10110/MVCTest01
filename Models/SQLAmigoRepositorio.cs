namespace MVCTest01.Models
{
    public class SQLAmigoRepositorio : IAmigoAlmacen
    {
        private readonly AppDbContext contexto;
        private List<Amigo> amigosLista;

        public SQLAmigoRepositorio(AppDbContext contexto)
        {
            this.contexto = contexto;
        }

        public Amigo nuevo(Amigo amigo)
        {
            contexto.Amigos.Add(amigo);
            contexto.SaveChanges();
            return amigo;
        }

        public Amigo borrar(int id)
        {
            Amigo amigo = contexto.Amigos.Find(id);
            if (amigo != null)
            {
                contexto.Amigos.Remove(amigo);
                contexto.SaveChanges();
                
            }
            return amigo;
        }

        public List<Amigo> DameTodosLosAMigos()
        {
            amigosLista = contexto.Amigos.ToList<Amigo>();
            return amigosLista;

        }

        public Amigo dameDatosAmigo(int id)
        {
            return contexto.Amigos.Find(id);
        }

        public Amigo modificar(Amigo amigo)
        {
            var ami = contexto.Amigos.Attach(amigo);
            ami.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            contexto.SaveChanges();
            return amigo;
        }
    }
}
