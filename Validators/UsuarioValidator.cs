using FluentValidation;
using ServiceSoap.Models;

namespace ServiceSoap.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("O sobrenome é obrigatório")
                .Length(2, 100).WithMessage("O sobrenome deve ter entre 2 e 100 caracteres");

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("O login é obrigatório")
                .Length(3, 50).WithMessage("O login deve ter entre 3 e 50 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres")
                .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula")
                .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula")
                .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número")
                .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial");
        }
    }
}
