using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppApi00.Model;


namespace WebAppApi00.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings; 
        private readonly ApplicationDbContext _dbContext;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDbContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }
        // Método para autenticar al usuario
        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request)
        {
            var user = await
            _dbContext.Users.SingleOrDefaultAsync(
            u => u.Username == request.Username && u.Password == request.Password);

            if (user == null) return null;

            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        // Método para obtener todos los usuarios activos
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users.Where(u => u.IsActive).ToListAsync();
        }

        // Método para buscar un usuario por ID
        public async Task<User?> GetById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        // Método para agregar o actualizar un usuario
        public async Task<User?> AddUpdateUser(User user)
        {
            if (user.Id > 0)
            {
                var existingUser = await
                _dbContext.Users.FindAsync(user.Id);
                if (existingUser != null)
                {
                    existingUser.FirstName = user.FirstName; 
                    existingUser.Lastname = user.Lastname; 
                    existingUser.Username = user.Username; 
                    existingUser.Password = user.Password; 
                    existingUser.IsActive = user.IsActive;

                    _dbContext.Users.Update(existingUser); 
                    await _dbContext.SaveChangesAsync(); 
                    return existingUser;
                }
            }
            else
            {
                await _dbContext.Users.AddAsync(user); 
                await _dbContext.SaveChangesAsync(); 
                return user;
            }
            return null;
        }

        // Método privado para generar un token JWT
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); 
            return tokenHandler.WriteToken(token);
        }
    }
}

