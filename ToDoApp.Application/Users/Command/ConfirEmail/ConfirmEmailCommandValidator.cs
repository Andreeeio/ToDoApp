using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Application.Users.Command.ConfirEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
           public ConfirmEmailCommandValidator() 
        {
            RuleFor(x => x.Email)
                .EmailAddress();
        }
    }
}
