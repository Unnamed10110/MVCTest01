namespace MVCTest01.Models
{
    public class MockAmigoRepositorio : IAmigoAlmacen
    {
        private List<Amigo> amigosLista;
        public MockAmigoRepositorio()
        {
            amigosLista = new List<Amigo>();
            amigosLista.Add(new Amigo() { Id = 1, Nombre = "Pedro", Ciudad = Provincia.Madrid, Email = "Pedro@mail.com" });
            amigosLista.Add(new Amigo() { Id = 2, Nombre = "Juan", Ciudad = Provincia.Toledo, Email = "Juan@mail.com" });
            amigosLista.Add(new Amigo() { Id = 3, Nombre = "Sara", Ciudad = Provincia.Cuenca, Email = "Sara@mail.com" });
        }
        public Amigo dameDatosAmigo(int Id)
        {
            return this.amigosLista.FirstOrDefault(e => e.Id == Id);
        }

        public List<Amigo> DameTodosLosAMigos()
        {
            return amigosLista;
        }

        public Amigo nuevo(Amigo amigo)
        {
            amigo.Id=amigosLista.Max(e => e.Id)+1;
            amigosLista.Add(amigo);
            return amigo;
        }

        public Amigo modificar(Amigo modificarAmigo)
        {
            Amigo amigo = amigosLista.FirstOrDefault(e => e.Id == modificarAmigo.Id);
            if (amigo != null)
            {
                amigo = modificarAmigo;
            }
            return amigo;
            
        }

        public Amigo borrar(int id)
        {
            var amigo = amigosLista.FirstOrDefault(a => a.Id == id);
            if (amigo != null)
            {
                amigosLista.Remove(amigo);
            }
            return amigo;
        }
    }
}