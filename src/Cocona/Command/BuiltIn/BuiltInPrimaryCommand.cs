﻿using Cocona.Help;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocona.Command.BuiltIn
{
    public class BuiltInPrimaryCommand
    {
        private readonly ICoconaCommandHelpProvider _commandHelpProvider;
        private readonly ICoconaHelpRenderer _helpRenderer;
        private readonly ICoconaCommandProvider _commandProvider;

        public BuiltInPrimaryCommand(ICoconaCommandHelpProvider commandHelpProvider, ICoconaHelpRenderer helpRenderer, ICoconaCommandProvider commandProvider)
        {
            _commandHelpProvider = commandHelpProvider;
            _helpRenderer = helpRenderer;
            _commandProvider = commandProvider;
        }

        public static CommandDescriptor GetCommand(CommandCollection commandCollection)
        {
            var t = typeof(BuiltInPrimaryCommand);
            var method = t.GetMethod(nameof(ShowDefaultMessage), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            return new CommandDescriptor(
                method,
                method.Name,
                Array.Empty<string>(),
                commandCollection.Description,
                new CommandParameterDescriptor[]{ new CommandOptionDescriptor(typeof(bool), "help", new[] { 'h' }, "Show help message", new CoconaDefaultValue(false)) },
                isPrimaryCommand: true
            );
        }

        private void ShowDefaultMessage([Option('h')]bool help = false)
        {
            Console.Write(_helpRenderer.Render(_commandHelpProvider.CreateCommandsIndexHelp(_commandProvider.GetCommandCollection())));
        }
    }
}