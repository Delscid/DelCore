#region Copyright 2017 Shane Delany

// Filename:  ICommandLineOption.cs
// Modified:  30/09/2017

#endregion

namespace DelCore.CommandLine.Abstract
{
    /// <summary>
    ///     Defines an interface for a command line option.
    /// </summary>
    public interface ICommandLineOption
        : ICommandLineElement,
          IArgumentContainer
    {
        #region Properties

        /// <summary>
        ///     Gets a template describing the option.
        /// </summary>
        /// <remarks>For rules on how templates should be structured, please refer to the online documentation.</remarks>
        /// <value>A template describing the option.</value>
        string Template { get; }

        /// <summary>
        ///     Gets the full name of the option.
        /// </summary>
        /// <value>The full name of the option.</value>
        string Name { get; }

        /// <summary>
        ///     Gets the short name of the option.
        /// </summary>
        /// <value>The short name of the option.</value>
        string ShortName { get; }

        /// <summary>
        ///     Gets the description of the option.
        /// </summary>
        /// <value>The description of the option.</value>
        string Description { get; }

        #endregion
    }
}
