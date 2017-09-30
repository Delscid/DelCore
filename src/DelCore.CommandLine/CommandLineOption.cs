#region Copyright 2017 Shane Delany

// Filename:  CommandLineOption.cs
// Modified:  30/09/2017

#endregion

using System;
using System.Collections.Generic;

using DelCore.CommandLine.Abstract;
using DelCore.CommandLine.Parsing;
using DelCore.Linq;

namespace DelCore.CommandLine
{
    public class CommandLineOption : ICommandLineOption
    {
        #region Constants and Fields

        private readonly OptionTemplateParser TemplateParser = new OptionTemplateParser();

        private readonly ICollection<ICommandLineArgument> ArgumentsValue = new List<ICommandLineArgument>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandLineOption"/> class.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="description"></param>
        internal CommandLineOption(string template = "", string description = "")
        {
            this.UpdateTemplate(template);
            this.Description = description;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a template describing the option.
        /// </summary>
        /// <remarks>For rules on how templates should be structured, please refer to the online documentation.</remarks>
        /// <value>A template describing the option.</value>
        public string Template { get; private set; }

        /// <summary>
        ///     Gets the full name of the option.
        /// </summary>
        /// <value>The full name of the option.</value>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the short name of the option.
        /// </summary>
        /// <value>The short name of the option.</value>
        public string ShortName { get; private set; }

        /// <summary>
        ///     Gets the description of the option.
        /// </summary>
        /// <value>The description of the option.</value>
        public string Description { get; }

        /// <summary>
        ///     Gets a <see cref="ICollection{T}" /> containing the arguments defined in the <see cref="IArgumentContainer" />.
        /// </summary>
        /// <returns>A <see cref="ICollection{ICommandLineArgument}" /> containing the arguments defined in the <see cref="IArgumentContainer" />.</returns>
        public ICollection<ICommandLineArgument> Arguments => this.ArgumentsValue.AsReadOnlyCollection();

        #endregion

        #region Methods

        /// <summary>
        ///     Defines a new <see cref="ICommandLineArgument" /> in this <see cref="CommandLineOption" />.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="type">The type of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="required">A value indicating whether the <see cref="ICommandLineArgument" /> is required.</param>
        /// <param name="repeatable">A value indicating whether the <see cref="ICommandLineArgument" /> can be used multiple times.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineArgument" /> before it is added to the <see cref="CommandLineOption"/>.</param>
        /// <returns>The new <see cref="ICommandLineArgument" /> instance defined in this <see cref="CommandLineOption" />.</returns>
        public virtual ICommandLineArgument AddArgument(string name, ArgumentType type, bool required = true, bool repeatable = false, Action<ICommandLineArgument> configure = null)
        {
            return this.AddArgument(name, String.Empty, type, required, repeatable, configure);
        }

        /// <summary>
        ///     Defines a new <see cref="ICommandLineArgument" /> in this <see cref="CommandLineOption" />.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="description">A description of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="type">The type of the <see cref="ICommandLineArgument" /> being defined.</param>
        /// <param name="required">A value indicating whether the <see cref="ICommandLineArgument" /> is required.</param>
        /// <param name="repeatable">A value indicating whether the <see cref="ICommandLineArgument" /> can be used multiple times.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineArgument" /> before it is added to the <see cref="CommandLineOption"/>.</param>
        /// <returns>The new <see cref="ICommandLineArgument" /> instance defined in this <see cref="CommandLineOption" />.</returns>
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

        protected void UpdateTemplate(string template)
        {
            var parsed = this.TemplateParser.Parse(template);

            this.Template = parsed.Template;
            this.Name = parsed.Name;
            this.ShortName = parsed.ShortName;
        }

        #endregion
    }
}
