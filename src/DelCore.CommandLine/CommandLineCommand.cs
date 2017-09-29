#region Copyright 2017 Shane Delany

// Filename:  CommandLineCommand.cs
// Modified:  30/09/2017

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DelCore.CommandLine.Abstract;
using DelCore.CommandLine.Internal;

namespace DelCore.CommandLine
{
    public class CommandLineCommand : ICommandLineCommand
    {
        #region Constants and Fields

        private readonly ICollection<ICommandLineCommand> CommandsValue = new List<ICommandLineCommand>();
        private readonly ICollection<ICommandLineOption> OptionsValue = new List<ICommandLineOption>();
        private readonly ICollection<ICommandLineArgument> ArgumentsValue = new List<ICommandLineArgument>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandLineCommand"/> class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="description">The description of the command.</param>
        internal CommandLineCommand(string name = "", string description = "")
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        public string Name { get; }

        /// <summary>
        ///     Gets the description of the command.
        /// </summary>
        /// <value>The description of the command.</value>
        public string Description { get; }

        /// <summary>
        ///     Gets the callback to be executed when the command is matched.
        /// </summary>
        /// <value>The callback to be executed when the command is matched.</value>
        public Action EntryPoint { get; private set; }

        /// <summary>
        ///     Gets a <see cref="ICollection{T}"/> containing the commands defined in this <see cref="CommandLineCommand"/>.
        /// </summary>
        /// <returns>A <see cref="ICollection{ICommandLineCommand}"/> containing the commands defined in this <see cref="CommandLineCommand"/>.</returns>
        public ICollection<ICommandLineCommand> Commands => this.CommandsValue.AsReadOnlyCollection();

        /// <summary>
        ///     Gets a <see cref="ICollection{T}" /> containing the options defined in this <see cref="CommandLineCommand" />.
        /// </summary>
        /// <returns>A <see cref="ICollection{ICommandLineOption}" /> containing the options defined in the <see cref="CommandLineCommand" />.</returns>
        public ICollection<ICommandLineOption> Options => this.OptionsValue.AsReadOnlyCollection();

        /// <summary>
        ///     Gets a <see cref="ICollection{T}" /> containing the arguments defined in this <see cref="CommandLineCommand" />.
        /// </summary>
        /// <returns>A <see cref="ICollection{ICommandLineArgument}" /> containing the arguments defined in this <see cref="CommandLineCommand" />.</returns>
        public ICollection<ICommandLineArgument> Arguments => this.ArgumentsValue.AsReadOnlyCollection();

        #endregion

        #region Methods

        /// <summary>
        ///     Defines a new <see cref="ICommandLineCommand"/> in this <see cref="CommandLineCommand"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="entrypoint">The callback to be executed when the <see cref="ICommandLineCommand"/> is matched.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineCommand"/> before it is added to this <see cref="CommandLineCommand"/>.</param>
        /// <returns>The new <see cref="ICommandLineCommand" /> instance defined in this <see cref="CommandLineCommand" />.</returns>
        public virtual ICommandLineCommand AddCommand(string name, Action entrypoint, Action<ICommandLineCommand> configure = null)
        {
            return this.AddCommand(name, String.Empty, entrypoint, configure);
        }

        /// <summary>
        ///     Defines a new <see cref="ICommandLineCommand"/> in this <see cref="CommandLineCommand"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="entrypoint">The callback to be executed when the <see cref="ICommandLineCommand"/> is matched.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineCommand"/> before it is added to this <see cref="CommandLineCommand"/>.</param>
        /// <returns>The new <see cref="ICommandLineCommand" /> instance defined in this <see cref="CommandLineCommand" />.</returns>
        public virtual ICommandLineCommand AddCommand(string name, Func<Task> entrypoint, Action<ICommandLineCommand> configure = null)
        {
            return this.AddCommand(name, String.Empty, entrypoint, configure);
        }

        /// <summary>
        ///     Defines a new <see cref="ICommandLineCommand"/> in this <see cref="CommandLineCommand"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="description">A description of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="entrypoint">The callback to be executed when the <see cref="ICommandLineCommand"/> is matched.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineCommand"/> before it is added to this <see cref="CommandLineCommand"/>.</param>
        /// <returns>The new <see cref="ICommandLineCommand" /> instance defined in this <see cref="CommandLineCommand" />.</returns>
        public virtual ICommandLineCommand AddCommand(string name, string description, Action entrypoint, Action<ICommandLineCommand> configure)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (description is null)
                throw new ArgumentNullException(nameof(description));

            CommandLineCommand command = new CommandLineCommand(name, description);

            command.SetEntryPoint(entrypoint);
            configure?.Invoke(command);

            this.CommandsValue.Add(command);

            return command;
        }

        /// <summary>
        ///     Defines a new <see cref="ICommandLineCommand"/> in this <see cref="CommandLineCommand"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="description">A description of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="entrypoint">The callback to be executed when the <see cref="ICommandLineCommand"/> is matched.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineCommand"/> before it is added to this <see cref="CommandLineCommand"/>.</param>
        /// <returns>The new <see cref="ICommandLineCommand" /> instance defined in this <see cref="CommandLineCommand" />.</returns>
        public virtual ICommandLineCommand AddCommand(string name, string description, Func<Task> entrypoint, Action<ICommandLineCommand> configure = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (description is null)
                throw new ArgumentNullException(nameof(description));

            CommandLineCommand command = new CommandLineCommand(name, description);

            command.SetEntryPoint(entrypoint);
            configure?.Invoke(command);

            this.CommandsValue.Add(command);

            return command;
        }

        /// <summary>
        ///     Defines a new <see cref="ICommandLineOption" /> in this <see cref="CommandLineCommand"/>.
        /// </summary>
        /// <remarks>For rules on how templates should be structured, please refer to the online documentation.</remarks>
        /// <param name="template">A template string describing the <see cref="ICommandLineOption" />.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineOption" /> before it is added to this <see cref="CommandLineCommand"/>.</param>
        /// <returns>The new <see cref="ICommandLineOption" /> instance defined in the <see cref="CommandLineCommand" />.</returns>
        public virtual ICommandLineOption AddOption(string template, Action<ICommandLineOption> configure = null)
        {
            return this.AddOption(template, String.Empty, configure);
        }

        /// <summary>
        ///     Defines a new <see cref="ICommandLineOption" /> in this <see cref="CommandLineCommand"/>.
        /// </summary>
        /// <remarks>For rules on how templates should be structured, please refer to the online documentation.</remarks>
        /// <param name="template">A template string describing the <see cref="ICommandLineOption" /> being defined.</param>
        /// <param name="description">A description of the <see cref="ICommandLineOption" /> being defined.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineOption" /> before it is added to this <see cref="CommandLineCommand"/>.</param>
        /// <returns>The new <see cref="ICommandLineOption" /> instance defined in this <see cref="CommandLineCommand" />.</returns>
        public virtual ICommandLineOption AddOption(string template, string description, Action<ICommandLineOption> configure = null)
        {
            if (string.IsNullOrWhiteSpace(template))
                throw new ArgumentNullException(nameof(template));

            if (description is null)
                throw new ArgumentNullException(nameof(description));

            CommandLineOption option = new CommandLineOption(template, description);

            configure?.Invoke(option);

            this.OptionsValue.Add(option);

            return option;
        }

        /// <summary>
        ///     Defines a new <see cref="ICommandLineArgument" /> in this <see cref="CommandLineCommand" />.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="type">The type of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="required">A value indicating whether the <see cref="ICommandLineArgument" /> is required.</param>
        /// <param name="repeatable">A value indicating whether the <see cref="ICommandLineArgument" /> can be used multiple times.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineArgument" /> before it is added to the <see cref="CommandLineCommand"/>.</param>
        /// <returns>The new <see cref="ICommandLineArgument" /> instance defined in this <see cref="CommandLineCommand" />.</returns>
        public virtual ICommandLineArgument AddArgument(string name, ArgumentType type, bool required = true, bool repeatable = false, Action<ICommandLineArgument> configure = null)
        {
            return this.AddArgument(name, String.Empty, type, required, repeatable, configure);
        }

        /// <summary>
        ///     Defines a new <see cref="ICommandLineArgument" /> in this <see cref="CommandLineCommand" />.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="description">A description of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="type">The type of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="required">A value indicating whether the <see cref="ICommandLineArgument" /> is required.</param>
        /// <param name="repeatable">A value indicating whether the <see cref="ICommandLineArgument" /> can be used multiple times.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineArgument" /> before it is added to the <see cref="CommandLineCommand"/>.</param>
        /// <returns>The new <see cref="ICommandLineArgument" /> instance defined in this <see cref="CommandLineCommand" />.</returns>
        public virtual ICommandLineArgument AddArgument(string name, string description, ArgumentType type, bool required = true, bool repeatable = false, Action<ICommandLineArgument> configure = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (description is null)
                throw new ArgumentNullException(nameof(description));

            CommandLineArgument argument = new CommandLineArgument(name, description, type, required, repeatable);

            configure?.Invoke(argument);
            this.ArgumentsValue.Add(argument);

            return argument;
        }

        /// <summary>
        ///     Sets the callback to be executed when this <see cref="ICommandLineCommand"/> is matched.
        /// </summary>
        /// <param name="entrypoint">The callback to be executed when this <see cref="ICommandLineCommand"/> is matched.</param>
        public void SetEntryPoint(Action entrypoint)
        {
            this.EntryPoint = entrypoint ?? throw new ArgumentNullException(nameof(entrypoint));
        }

        /// <summary>
        ///     Sets the asynchronous callback to be executed when this <see cref="ICommandLineCommand"/> is matched.
        /// </summary>
        /// <param name="entrypoint">The asynchronous callback to be executed when this <see cref="ICommandLineCommand"/> is matched.</param>
        public void SetEntryPoint(Func<Task> entrypoint)
        {
            if (entrypoint is null)
                throw new ArgumentNullException(nameof(entrypoint));

            this.SetEntryPoint(() => entrypoint.Invoke().GetAwaiter().GetResult());
        }

        /// <summary>
        ///     Executes the callback associated with this <see cref="ICommandLineCommand"/> using the provided <paramref name="arguments"/>.
        /// </summary>
        /// <param name="arguments">The arguments to process and pass to the callback.</param>
        public void Execute(params string[] arguments) { }

        #endregion
    }
}
