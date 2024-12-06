using WebAppApi00.Model;

namespace WebAppApi00.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
        Task<IEnumerable<User>> GetAll(); 
        Task<User?> GetById(int id); 
        Task<User?> AddUpdateUser(User user);
    }
}
