using WebAppApiMoto.Model;
namespace WebAppApiMoto.Services
{
    public interface IMotoService
    {
        List<Moto> GetAllMotos(bool? isActive); Moto? GetMotoByID(int id);
        Moto AddMoto(AddUpdateMoto obj);
        Moto? UpdateMoto(int id, AddUpdateMoto obj); bool DeleteMotoByID(int id);
    }
}
