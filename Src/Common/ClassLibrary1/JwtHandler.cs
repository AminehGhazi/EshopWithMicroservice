
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _JwtSecurityTokenHandler=new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _IssuerSigningKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public JwtHandler(IOptions<JwtOptions> jwtOptions)
        {
            _options =  jwtOptions.Value;
            _IssuerSigningKey= new SymmetricSecurityKey (Encoding .UTF8 .GetBytes (_options .SecretKey));
            _signingCredentials=new SigningCredentials (_IssuerSigningKey ,SecurityAlgorithms .HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience  = false,
                ValidIssuer   = _options.Issuer,
                IssuerSigningKey = _IssuerSigningKey
            };
        }
        public JsonWebToken Create(Guid userId)
        {
           var nowUtc=DateTime .UtcNow;     
            var expires= nowUtc.AddMinutes(_options.ExpiryMinutes );
            var centuryBegine= new DateTime (1970,1,1).ToUniversalTime ();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegine.Ticks).TotalMicroseconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegine.Ticks).TotalMicroseconds);
            var payload = new JwtPayload
            {
                {"sub",userId },
                {"iss",_options.Issuer   },
                {"iat",now },
                {"exp",exp },
                {"uniqu_code",userId},  
            };
            var jwt= new JwtSecurityToken (_jwtHeader , payload);   
            var token=_JwtSecurityTokenHandler .WriteToken (jwt);
            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}
