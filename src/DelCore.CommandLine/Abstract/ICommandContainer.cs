﻿#region Copyright 2017 Shane Delany

// Filename:  ICommandContainer.cs
// Modified:  30/09/2017

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DelCore.CommandLine.Abstract
{
    /// <summary>
    ///     Defines a set of method for interacting with a command line type which can hold commands.
    /// </summary>
    public interface ICommandContainer
    {
        #region Properties

        /// <summary>
        ///     Gets a <see cref="ICollection{T}"/> containing the commands defined in this <see cref="ICommandContainer"/>.
        /// </summary>
        /// <returns>A <see cref="ICollection{ICommandLineCommand}"/> containing the commands defined in this <see cref="ICommandContainer"/>.</returns>
        ICollection<ICommandLineCommand> Commands { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Defines a new <see cref="ICommandLineCommand"/> in this <see cref="ICommandContainer"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="entrypoint">The callback to be executed when the <see cref="ICommandLineCommand"/> is matched.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineCommand"/> before it is added to this <see cref="ICommandContainer"/>.</param>
        /// <returns>The new <see cref="ICommandLineCommand" /> instance defined in this <see cref="ICommandContainer" />.</returns>
        ICommandLineCommand AddCommand(string name, Action entrypoint, Action<ICommandLineCommand> configure = null);

        /// <summary>
        ///     Defines a new <see cref="ICommandLineCommand"/> in this <see cref="ICommandContainer"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="entrypoint">The asynchronous callback to be executed when the <see cref="ICommandLineCommand"/> is matched.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineCommand"/> before it is added to the <see cref="ICommandContainer"/>.</param>
        /// <returns>The new <see cref="ICommandLineCommand" /> instance defined in this <see cref="ICommandContainer" />.</returns>
        ICommandLineCommand AddCommand(string name, Func<Task> entrypoint, Action<ICommandLineCommand> configure = null);

        /// <summary>
        ///     Defines a new <see cref="ICommandLineCommand"/> in this <see cref="ICommandContainer"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="description">A description of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="entrypoint">The callback to be executed when the <see cref="ICommandLineCommand"/> is matched.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineCommand"/> before it is added to the <see cref="ICommandContainer"/>.</param>
        /// <returns>The new <see cref="ICommandLineCommand" /> instance defined in the <see cref="ICommandContainer" />.</returns>
        ICommandLineCommand AddCommand(string name, string description, Action entrypoint, Action<ICommandLineCommand> configure = null);

        /// <summary>
        ///     Defines a new <see cref="ICommandLineCommand"/> in the <see cref="ICommandContainer"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="description">A description of the <see cref="ICommandLineCommand"/> being defined.</param>
        /// <param name="entrypoint">The asynchronous callback to be executed when the <see cref="ICommandLineCommand"/> is matched.</param>
        /// <param name="configure">A callback used to configure the <see cref="ICommandLineCommand"/> before it is added to the <see cref="ICommandContainer"/>.</param>
        /// <returns>The new <see cref="ICommandLineCommand" /> instance defined in the <see cref="ICommandContainer" />.</returns>
        ICommandLineCommand AddCommand(string name, string description, Func<Task> entrypoint, Action<ICommandLineCommand> configure = null);

        #endregion
    }
}
