using Application.Service;
using AutoMapper;
using Domain.Common.Enum;
using Domain.Dto.Request;
using Domain.Dto.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Application.Services
{
    /// <summary>
    /// This class represent all service of validation  token
    /// 
    /// </summary>
    public class TokenHandler : IRequestHandler<TokenRequest, bool>,
        IRequestHandler<TokenCreateRequest, TokenResponse>
    {
        private readonly IConfigurationRepository configuiuracionRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IValidator<TokenCreateRequest> validator;

        public TokenHandler(IConfigurationRepository configuiuracionRepository, IUsuarioRepository usuarioRepository, IValidator<TokenCreateRequest> _validator)
        {
            this.configuiuracionRepository = configuiuracionRepository;
            this.usuarioRepository = usuarioRepository;
            validator = _validator;
        }



        public async Task<TokenResponse> Handle(TokenCreateRequest request, CancellationToken cancellationToken)
        {
            TokenResponse UserTokenResponse;
            try
            {


                var result = await validator.ValidateAsync(request);
                if (!result.IsValid && result is null)
                {

                    throw new ApiException(result.Errors.ToString() ?? "", (int)System.Net.HttpStatusCode.Unauthorized);
                }


                var user = await ValidateUserName(request.Email);
                if (user is null)
                {
                    throw new ApiException("Usuario no encontrado", (int)System.Net.HttpStatusCode.Unauthorized);
                }

                string pass = request.Password.DecodeBase64Password();
                if (!await ValidatePassword(pass, user.Password))
                {
                    throw new ApiException("Password no valido", (int)System.Net.HttpStatusCode.Unauthorized);
                }


                UserTokenResponse = await MapperUserTokenResponse(user);
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
            return UserTokenResponse;
        }
        private async Task<TokenResponse> MapperUserTokenResponse(Usuario user)
        {
            TokenResponse UserTokenResponse = new();
            UserTokenResponse.Token = await GenerateToken(user.NameUsuario);
            UserTokenResponse.IdRol = user.Idrol;
            return UserTokenResponse;
        }
        private async Task<Usuario?> ValidateUserName(string? correo)
        {
            var user = await usuarioRepository.GetByParam(x => x.Email.Equals(correo));
            return user;
        }

        public async Task<bool> ValidatePassword(string? password, string encryptedPassword)
        {
            var keyEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.KeyEncrypted.ToString())))?.Value ?? string.Empty;
            var iVEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.IVEncrypted.ToString())))?.Value ?? string.Empty;
            byte[] key = Encoding.UTF8.GetBytes(keyEncrypted);
            byte[] iv = Encoding.UTF8.GetBytes(iVEncrypted);
            using (TripleDES aes = TripleDES.Create())
            {

                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);
                byte[] decryptedPasswordBytes = decryptor.TransformFinalBlock(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
                string decryptedPassword = Encoding.UTF8.GetString(decryptedPasswordBytes);
                return decryptedPassword == password;
            }


        }
        private async Task<string> GenerateToken(string? userName = "")
        {
            var secretKey = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtSecretKey.ToString())))?.Value ?? string.Empty;
            var jwtIssuerToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value ?? string.Empty;
            var jwtAudienceToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value ?? string.Empty;
            var jwtExpireTime = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtExpireTime.ToString())))?.Value ?? string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            ClaimsIdentity claimsIdentity = new(new[] { new Claim(ClaimTypes.Name, userName) });
            var currentDate = DateTime.Now;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: jwtAudienceToken,
                issuer: jwtIssuerToken,
                subject: claimsIdentity,
                notBefore: currentDate,
                expires: currentDate.AddMinutes(Convert.ToInt32(jwtExpireTime)),
                signingCredentials: signingCredentials);
            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }


        public async Task<bool> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            return await ValidateToken(request.Token);
        }
        public async Task<bool> ValidateToken(string token)
        {

            try
            {
                var tokenHeader = new JwtSecurityTokenHandler();
                var secreKey = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtSecretKey.ToString())))?.Value ?? string.Empty;
                var jwtIssuerToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value;
                var jwtAudienceToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value;
                var key = Encoding.ASCII.GetBytes(secreKey);
                var tokenParameter = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuerToken,
                    ValidateAudience = true,
                    ValidAudience = jwtAudienceToken,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHeader.ValidateToken(token, tokenParameter, out SecurityToken securutyToken);
                var jwtToken = (JwtSecurityToken)securutyToken;
                var isOk = await SearchUser(jwtToken.Claims.First(t => t.Type == "unique_name").Value);
                return isOk;
            }
            catch (Exception ex)
            {

                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }
        private async Task<bool> SearchUser(string username)
        {
            var user = await usuarioRepository.GetByParam(x => x.NameUsuario.Equals(username));
            return user != null;
        }
    }
}
