﻿using Reporter.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporter.Core.Command.Validation
{
    public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : IValidate
    {
        private readonly IValidator<TCommand> validator;
        private readonly ICommandHandler<TCommand> commandHandler; //decoratee

        public ValidationCommandHandlerDecorator(IValidator<TCommand> validator, ICommandHandler<TCommand> commandHandler)
        {
            this.validator = validator;
            this.commandHandler = commandHandler;
        }

        public async Task ExecuteAsync(TCommand command)
        {
            await this.validator.ValidateAsync(command);

            await this.commandHandler.ExecuteAsync(command);
        }
    }
}
