using ServiceSoap.Data;
using ServiceSoap.Interface;
using ServiceSoap.Models;
using ServiceSoap.Validators;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using FluentValidation;
using System.ServiceModel;
using ServiceSoap.Dto;
using System.Threading.Tasks;
using System.Security.Cryptography.Xml;

namespace ServiceSoap.Repository
{
    public class RUsuario(AppDbContext context) : IUsuario
    {
        private readonly AppDbContext _context = context;
        private readonly UsuarioValidator _validator = new();
        private readonly UsuarioDtoValidator _validatorDto = new();

        public async Task<UsuarioDto> Create(Usuario entity)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var isExistUser = await GetIsExistUsuario(entity);

            if (isExistUser == null)
            {
                await _context.Usuarios.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException("Usuario Já cadastrado.");
            }

            return entity.GetUsuarioDto();
        }

        public async Task<UsuarioDto> Read(int id)
        {
            var usuario = await _context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (usuario == null) throw new KeyNotFoundException($"Usuario com ID {id} não encontrado");

            return usuario.GetUsuarioDto();
        }

        public async Task<IEnumerable<UsuarioDto>> ReadAll()
        {
            return await _context.Usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email
            }).ToListAsync(); ;
        }

        public async Task<UsuarioDto> Update(UsuarioDto entity)
        {
            ValidationResult validationResult = await _validatorDto.ValidateAsync(entity);
            
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var usuario = await _context.Usuarios.FindAsync(entity.Id) ?? throw new KeyNotFoundException($"Usuario com ID {entity.Id} não encontrado");
            
            _context.Entry(usuario).CurrentValues.SetValues(new Usuario()
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Email = entity.Email
            });

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await _context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (usuario == null) throw new ValidationException($"Usuario não encontrado no sistema com ID: {id}");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<Usuario?> GetIsExistUsuario(Usuario usuario) => await _context.Usuarios.Where(x => x.Name == usuario.Name && x.Surname == usuario.Surname).FirstOrDefaultAsync();

        public async Task<int> GetIdUsuario(string nome, string login, string Email)
        {
            var Result = await _context.Usuarios.Where(x => x.Name == nome || x.Login == login || x.Email == Email).FirstOrDefaultAsync();

            if (Result == null) throw new ValidationException("Usuario não encontrado");

            return Result.Id;
        }
    }
}
