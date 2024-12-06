using WebAppApiMoto.Model;
namespace WebAppApiMoto.Services
{
    public class MotoService : IMotoService
    {
        private readonly ApplicationDbContext _context;

        public MotoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Moto AddMoto(AddUpdateMoto motoData)
        {
            var newMoto = new Moto
            {
                Codigo = motoData.Codigo,
                Marca = motoData.Marca,
                Descripcion = motoData.Descripcion,
                Modelo = motoData.Modelo,
                Precio = motoData.Precio,
                Disponible = motoData.Disponible
            };

            _context.Motos.Add(newMoto);
            _context.SaveChanges(); return newMoto;
        }

        public bool DeleteMotoByID(int id)
        {
            var moto = _context.Motos.Find(id);
            if (moto == null) return false;

            _context.Motos.Remove(moto);
            _context.SaveChanges();
            return true;
        }
        public List<Moto> GetAllMotos(bool? Disponible = null)
        {
            return Disponible == null
            ? _context.Motos.ToList()
            : _context.Motos.Where(mot => mot.Disponible == Disponible).ToList();
        }

        public Moto? GetMotoByID(int id)
        {
            return _context.Motos.Find(id);
        }

        public Moto? UpdateMoto(int id, AddUpdateMoto motoData)
        {
            var moto = _context.Motos.Find(id);
            if (moto == null) return null;

            moto.Codigo = motoData.Codigo;
            moto.Marca = motoData.Marca;
            moto.Descripcion = motoData.Descripcion;
            moto.Precio = motoData.Precio;
            moto.Disponible = motoData.Disponible;

            _context.SaveChanges();
            return moto;
        }
    }
}
