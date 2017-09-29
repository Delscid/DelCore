#region Copyright 2017 Shane Delany

// Filename:  ICommandLineCommand.cs
// Modified:  30/09/2017

#endregion

using System;
using System.Threading.Tasks;

namespace DelCore.CommandLine.Abstract
{
    /// <summary>
    ///  Defines an interface for a command line command.
    /// </summary>
    public interface ICommandLineCommand
        : ICommandLineElement,
          ICommandContainer,
          IOptionContainer,
          IArgumentContainer
    {
        #region Properties

        /// <summary>
        ///     Gets the name of the command.
        /// </summary>
        /// <value>The name of the command.</value>
        string Name { get; }

        /// <summary>
        ///     Gets the description of the command.
        /// </summary>
        /// <value>the description of the command.</value>
        string Description { get; }

        /// <summary>
        ///     Gets the callback to be executed when the command is matched.
        /// </summary>
        /// <value>The callback to be executed when the command is matched.</value>
        Action EntryPoint { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the callback to be executed when the command is matched.
        /// </summary>
        /// <param name="entrypoint">The callback to be executed when the command is matched.</param>
        void SetEntryPoint(Action entrypoint);

        /// <summary>
        ///     Sets the asynchronous callback to be executed when the command is matched.
        /// </summary>
        /// <param name="entrypoint">The asynchronous callback to be executed when the command is matched.</param>
        void SetEntryPoint(Func<Task> entrypoint);

        /// <summary>
        ///     Executes the callback associated with the command using the provided <paramref name="arguments"/>.
        /// </summary>
        /// <param name="arguments">The arguments to process and pass to the command.</param>
        void Execute(params string[] arguments);

        #endregion
    }
}
