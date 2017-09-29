﻿#region Copyright 2017 Shane Delany

// Filename:  IArgumentContainer.cs
// Modified:  28/09/2017

#endregion

using System;
using System.Collections.Generic;

namespace DelCore.CommandLine.Abstract
{
    /// <summary>
    ///     Defines a set of methods for interacting with a command line type which can hold arguments.
    /// </summary>
    public interface IArgumentContainer
    {
        #region Properties

        /// <summary>
        ///     Gets a <see cref="ICollection{T}" /> containing the arguments defined in the <see cref="IArgumentContainer" />.
        /// </summary>
        /// <returns>A <see cref="ICollection{ICommandLineArgument}" /> containing the arguments defined in the <see cref="IArgumentContainer" />.</returns>
        ICollection<ICommandLineArgument> Arguments { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Defines a new argument in the <see cref="IArgumentContainer" />.
        /// </summary>
        /// <param name="name">The name of the argument to define.</param>
        /// <param name="type">The type of the argument to define.</param>
        /// <param name="required">A value indicating whether the argument is required.</param>
        /// <param name="repeatable">A value indicating whether the argument can be used multiple times.</param>
        /// <param name="configure">A callback used to configure the argument before it is added to the <see cref="IArgumentContainer"/>.</param>
        /// <returns>The new <see cref="ICommandLineArgument" /> instance defined in the <see cref="IArgumentContainer" />.</returns>
        ICommandLineArgument AddArgument(string name, ArgumentType type, bool required = true, bool repeatable = false, Action<ICommandLineArgument> configure = null);

        /// <summary>
        ///     Defines a new argument in the <see cref="IArgumentContainer" />.
        /// </summary>
        /// <param name="name">The name of the argument to define.</param>
        /// <param name="description">A description of the argument being defined.</param>
        /// <param name="type">The type of the argument to define.</param>
        /// <param name="required">A value indicating whether the argument is required.</param>
        /// <param name="repeatable">A value indicating whether the argument can be used multiple times.</param>
        /// <param name="configure">A callback used to configure the argument before it is added to the <see cref="IArgumentContainer"/>.</param>
        /// <returns>The new <see cref="ICommandLineArgument" /> instance defined in the <see cref="IArgumentContainer" />.</returns>
        ICommandLineArgument AddArgument(string name, string description, ArgumentType type, bool required = true, bool repeatable = false, Action<ICommandLineArgument> configure = null);

        #endregion
    }
}