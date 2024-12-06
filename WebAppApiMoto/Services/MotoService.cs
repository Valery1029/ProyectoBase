using WebAppApiMoto.Model;
namespace WebAppApiMoto.Services
{
    public class MotoService : IMotoService
    {
        private readonly List<Moto> _motosList; 
        public MotoService()
        {
            _motosList = new List<Moto>
            {
                new Moto
                {
                Id = 1,
                Codigo = "12345", 
                Marca = "Suzuki", 
                Descripcion = "Gixxer 250",
                Modelo = 2024,
                Precio = 100, 
                Disponible = true
                }
            };
        }

        public Moto AddMoto(AddUpdateMoto obj)
        {
            var newMoto = new Moto
            {
                Id = _motosList.Any() ? _motosList.Max(mot
                => mot.Id) + 1 : 1,
                Codigo = obj.Codigo,
                Marca = obj.Marca,
                Descripcion = obj.Descripcion,
                Modelo = obj.Modelo,
                Precio = obj.Precio,
                Disponible = obj.Disponible,
            };

            _motosList.Add(newMoto); return newMoto;
        }

        public bool DeleteMotoByID(int id)
        {
            var motoIndex = _motosList.FindIndex(mot => mot.Id == id);
            if (motoIndex >= 0)
            {
                _motosList.RemoveAt(motoIndex); return true;
            }
            return false;
        }

        public List<Moto> GetAllMotos(bool? Disponible)
        {
            return Disponible == null ? _motosList :
            _motosList.Where(mot => mot.Disponible == Disponible).ToList();
        }

        public Moto? GetMotoByID(int id)
        {
            return _motosList.FirstOrDefault(mot => mot.Id == id);
        }

        public Moto? UpdateMoto(int id, AddUpdateMoto obj)
        {
            var moto = _motosList.FirstOrDefault(mot => mot.Id == id);
            if (moto == null) return null;

            moto.Codigo = obj.Codigo; 
            moto.Marca = obj.Marca;
            moto.Descripcion = obj.Descripcion;
            moto.Modelo = obj.Modelo;
            moto.Precio = obj.Precio;
            moto.Disponible = obj.Disponible;

            return moto;
        }
    }
}
