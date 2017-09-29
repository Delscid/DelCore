#region Copyright 2017 Shane Delany

// Filename:  IOptionContainer.cs
// Modified:  30/09/2017

#endregion

using System;
using System.Collections.Generic;

namespace DelCore.CommandLine.Abstract
{
    /// <summary>
    ///     Defines a set of methods for interacting with a command line type which can hold options.
    /// </summary>
    public interface IOptionContainer
    {
        #region Properties

        /// <summary>
        ///     Gets a <see cref="ICollection{T}" /> containing the options defined in the <see cref="IOptionContainer" />.
        /// </summary>
        /// <returns>A <see cref="ICollection{ICommandLineOption}" /> containing the options defined in the <see cref="IOptionContainer" />.</returns>
        ICollection<ICommandLineOption> Options { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Defines a new option in the <see cref="IOptionContainer"/>.
        /// </summary>
        /// <remarks>For rules on how templates should be structured, please refer to the online documentation.</remarks>
        /// <param name="template">A template string describing the option.</param>
        /// <param name="configure">A callback used to configure the option before it is added to the <see cref="IOptionContainer"/>.</param>
        /// <returns>The new <see cref="ICommandLineOption" /> instance defined in the <see cref="IOptionContainer" />.</returns>
        ICommandLineOption AddOption(string template, Action<ICommandLineOption> configure = null);

        /// <summary>
        ///     Defines a new option in the <see cref="IOptionContainer"/>.
        /// </summary>
        /// <remarks>For rules on how templates should be structured, please refer to the online documentation.</remarks>
        /// <param name="template">A template string describing the option to define.</param>
        /// <param name="description">A description of the option being defined.</param>
        /// <param name="configure">A callback used to configure the option before it is added to the <see cref="IOptionContainer"/>.</param>
        /// <returns>The new <see cref="ICommandLineOption" /> instance defined in the <see cref="IOptionContainer" />.</returns>
        ICommandLineOption AddOption(string template, string description, Action<ICommandLineOption> configure = null);

        #endregion
    }
}
