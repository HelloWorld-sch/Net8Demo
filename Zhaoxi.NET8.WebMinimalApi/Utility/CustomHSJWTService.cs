using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zhaoxi.NET8.WebCore;

namespace Zhaoxi.NET8.WebMinimalApi.Utility
{
    public class CustomHSJWTService : ICustomJWTService
    {

        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        public CustomHSJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            _JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        #endregion

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetToken(CurrentUser user)
        {
            //准备有效载荷
            Claim[] claims = new[]
             {
               new Claim(ClaimTypes.Name, user.Name),
               new Claim("NickName",user.NikeName),
               new Claim(ClaimTypes.Role,user.RoleList),//传递其他信息   
               new Claim("Description",user.Description),
               new Claim("Age",user.Age.ToString()),
            };

            //准备加密key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

            //Sha256 加密方式
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken token = new JwtSecurityToken(
                  issuer: _JWTTokenOptions.Issuer,
                  audience: _JWTTokenOptions.Audience,
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(5),//5分钟有效期
                  signingCredentials: creds);

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);

            return returnToken;

        }
    }
}
