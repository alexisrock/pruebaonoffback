using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.TransformDto;
using AutoMapper;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto.Request;
using Domain.Dto.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Service
{
    public class UsuarioHandler : IRequestHandler<UsuarioRequest, BaseResponse>,
         IRequestHandler<UsuarioUpdateRequest, BaseResponse>,
         IRequestHandler<UsuarioDeleteRequest, BaseResponse>,
         IRequestHandler<UsuarioIdRequest, UserResponse>,
         IRequestHandler<UsuarioAll, List<UserResponse>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfigurationRepository configuiuracionRepository;
        private readonly IMapper mapper;

        public UsuarioHandler(IUsuarioRepository usuarioRepository, IConfigurationRepository configuiuracionRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            this.configuiuracionRepository = configuiuracionRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(UsuarioRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            try
            {
                var validateUser = await ValidateUserName(request.NameUsuario);
                if (validateUser is null)
                {
                    var usuario = mapper.Map<Usuario>(request);
                    string pass = request.Password.DecodeBase64Password();
                    usuario.Password = await EncryptedPassword(pass);
                    usuario.Idrol = 3;
                    await InsertUser(usuario);
                    response.SetDataResponse(HttpStatusCode.OK, $"Created user {usuario.NameUsuario} with success");
                    return response;
                }
                throw new ApiException($"the user already exists with name {request.NameUsuario}", (int)System.Net.HttpStatusCode.Conflict);


            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }
        private async Task<string> EncryptedPassword(string password)
        {
            var keyEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.KeyEncrypted.ToString())))?.Value ?? string.Empty;
            var iVEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.IVEncrypted.ToString())))?.Value ?? string.Empty;


            byte[] key = Encoding.UTF8.GetBytes(keyEncrypted);
            byte[] iv = Encoding.UTF8.GetBytes(iVEncrypted);

            using (TripleDES aes = TripleDES.Create())
            {


                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedPasswordBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

                string encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);
                return encryptedPassword;
            }
        }
        private async Task<Usuario?> ValidateUserName(string? userName)
        {
            var user = await _usuarioRepository.GetByParam(x => x.NameUsuario.Equals(userName));
            return user;
        }
        private async Task InsertUser(Usuario usuario)
        {
            await _usuarioRepository.Insert(usuario);
        }

        public async Task<BaseResponse> Handle(UsuarioUpdateRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            try
            {
                var usuario = await _usuarioRepository.GetById(request.Id);
                if (usuario is null)
                {
                    throw new ApiException($"El id del usuario {request.NameUsuario} no existe", (int)System.Net.HttpStatusCode.BadRequest);
                }
                usuario.SetUsuarioUpdate(request);
                string pass = request.Password.DecodeBase64Password();
                usuario.Password = await EncryptedPassword(pass);
                await _usuarioRepository.Update(usuario);
                response.SetDataResponse(HttpStatusCode.OK, "Usuario actualizado con exito");
                return response;
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }

        }

        public async Task<BaseResponse> Handle(UsuarioDeleteRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            try
            {
                var usuario = await _usuarioRepository.GetById(request.Id);
                if (usuario is null)
                {
                    throw new ApiException($"El id del usuario no existe", (int)System.Net.HttpStatusCode.BadRequest);
                }
                await _usuarioRepository.Delete(usuario);
                response.SetDataResponse(HttpStatusCode.OK, "Usuario eliminado con exito");
                return response;
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }



        }

        public async Task<UserResponse> Handle(UsuarioIdRequest request, CancellationToken cancellationToken)
        {
            var response = new UserResponse();
            try
            {
                var usuario = await _usuarioRepository.GetById(request.Id);
                if (usuario is null)
                {
                    throw new ApiException($"El id del usuario no existe", (int)System.Net.HttpStatusCode.BadRequest);
                }

                response = mapper.Map<UserResponse>(usuario);
                return response;
            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public async Task<List<UserResponse>> Handle(UsuarioAll request, CancellationToken cancellationToken)
        {
            var response = new List<UserResponse>();

            try
            {
                var usuarios = await _usuarioRepository.GetAll();
                response.MapperListUserResponse(usuarios);
                return response;

            }
            catch (Exception ex) when (ex is ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiException("Ocurrió un error inesperado", (int)System.Net.HttpStatusCode.InternalServerError);
            }

        }
    }
}
