#region Copyright 2017 Shane Delany

// Filename:  ICommandLineArgument.cs
// Modified:  30/09/2017

#endregion

namespace DelCore.CommandLine.Abstract
{
    /// <summary>
    ///     Defines an interface for a command line argument which can be a value for any other element.
    /// </summary>
    public interface ICommandLineArgument : ICommandLineElement
    {
        #region Properties

        /// <summary>
        ///     Gets the name of the argument.
        /// </summary>
        /// <value>The name of the argument.</value>
        string Name { get; }

        /// <summary>
        ///     Gets the description of the argument.
        /// </summary>
        /// <value>The description of the argument.</value>
        string Description { get; }

        /// <summary>
        ///     Gets the type of value the argument accepts.
        /// </summary>
        /// <value>The type of value the argument accepts.</value>
        ArgumentType Type { get; }

        /// <summary>
        ///     Gets a value indicating whether the argument is required.
        /// </summary>
        /// <value><see langword="true" />, if the argument is required; otherwise, <see langword="false"/>.</value>
        bool Required { get; }

        /// <summary>
        ///     Gets a value indicating whether the argument can be used multiple times.
        /// </summary>
        /// <value><see langword="true" />, if the argument can be used multiple times; otherwise, <see langword="false"/>.</value>
        bool Repeatable { get; }

        #endregion
    }
}
