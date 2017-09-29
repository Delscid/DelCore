#region Copyright 2017 Shane Delany

// Filename:  CommandLineArgument.cs
// Modified:  30/09/2017

#endregion

using DelCore.CommandLine.Abstract;

namespace DelCore.CommandLine
{
    /// <summary>
    /// </summary>
    public class CommandLineArgument : ICommandLineArgument
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandLineArgument"/> class.
        /// </summary>
        /// <param name="name">The name of the argument.</param>
        /// <param name="description">The description of the argument.</param>
        /// <param name="type">The type of value the argument accepts.</param>
        /// <param name="required">A value indicating whether the argument is required.</param>
        /// <param name="repeatable">A value indicating whether the argument can be used multiple times.</param>
        internal CommandLineArgument(string name, string description, ArgumentType type, bool required = true, bool repeatable = false)
        {
            this.Name = name;
            this.Description = description;
            this.Type = type;
            this.Required = required;
            this.Repeatable = repeatable;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the name of the argument.
        /// </summary>
        /// <value>The name of the argument.</value>
        public string Name { get; }

        /// <summary>
        ///     Gets the description of the argument.
        /// </summary>
        /// <value>The description of the argument.</value>
        public string Description { get; }

        /// <summary>
        ///     Gets the type of value the argument accepts.
        /// </summary>
        /// <value>The type of value the argument accepts.</value>
        public ArgumentType Type { get; }

        /// <summary>
        ///     Gets a value indicating whether the argument is required.
        /// </summary>
        /// <value><see langword="true" />, if the argument is required; otherwise, <see langword="false"/>.</value>
        public bool Required { get; }

        /// <summary>
        ///     Gets a value indicating whether the argument can be used multiple times.
        /// </summary>
        /// <value><see langword="true" />, if the argument can be used multiple times; otherwise, <see langword="false"/>.</value>
        public bool Repeatable { get; }

        #endregion
    }
}
