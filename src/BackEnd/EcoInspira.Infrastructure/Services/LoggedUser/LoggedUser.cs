using EcoInspira.Domain.Entities;
using EcoInspira.Domain.Security.Tokens;
using EcoInspira.Domain.Services.LoggedUser;
using EcoInspira.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EcoInspira.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly EcoInspiraDbContext _dbcontext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(EcoInspiraDbContext dbcontext, ITokenProvider tokenProvider)
        {
            _dbcontext = dbcontext;
            _tokenProvider = tokenProvider;
        }
        public async Task<User> User ()
        {
            var token = _tokenProvider.Value();

            var tokenHandler = new JwtSecurityTokenHandler();   

            var jwtSecurity = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurity.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            var userIdentifier = Guid.Parse(identifier);

            return await _dbcontext
                .User
                .AsNoTracking()
                .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
        }
    }
}
