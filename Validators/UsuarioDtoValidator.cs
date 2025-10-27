using System;
using FluentValidation;
using ServiceSoap.Dto;

namespace ServiceSoap.Validators;

public class UsuarioDtoValidator : AbstractValidator<UsuarioDto>
{
    public UsuarioDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O ID não pode ser nulo ou vazio.")
            .GreaterThan(0).WithMessage("O ID deve ser maior que zero.");

        RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("O sobrenome é obrigatório")
            .Length(2, 100).WithMessage("O sobrenome deve ter entre 2 e 100 caracteres");

        RuleFor(x => x.Email)
               .NotEmpty().WithMessage("O email é obrigatório")
               .EmailAddress().WithMessage("Email inválido");
    }
}
