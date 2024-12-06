using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAppApi00.Model;
using WebAppApi00.Services;

namespace WebAppApi00.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly AppSettings _appSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await AttachUserToContext(context, userService, token);
            }
            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParams, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(c => c.Type == "id").Value);

                // Adjuntar el usuario al contexto si el token es válido

                context.Items["User"] = await
                userService.GetById(userId);
            }
            catch
            {
                // Si la validación del token falla, no se hace nada
                // El usuario no estará asociado al contexto
            }
        }
    }
}
