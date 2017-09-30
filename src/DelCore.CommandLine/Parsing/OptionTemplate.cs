#region Copyright 2017 Shane Delany

// Filename:  OptionTemplate.cs
// Modified:  30/09/2017

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DelCore.CommandLine.Parsing
{
    public class OptionTemplate
        : IEquatable<OptionTemplate>
    {
        #region Constructors and Destructors

        public OptionTemplate(string template, string name = "", string shortName = "", IEnumerable<string> valueNames = null)
        {
            this.Template = template ?? throw new ArgumentNullException(nameof(template));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.ShortName = shortName ?? throw new ArgumentNullException(nameof(shortName));
            this.ValueNames = valueNames ?? new ReadOnlyCollection<string>(new string[] { });
        }

        #endregion

        #region Properties

        public string Template { get; }

        public string Name { get; }

        public string ShortName { get; }

        public IEnumerable<string> ValueNames { get; }

        #endregion

        #region Methods

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:DelCore.CommandLine.Parsing.OptionTemplate" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(OptionTemplate left, OptionTemplate right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:DelCore.CommandLine.Parsing.OptionTemplate" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(OptionTemplate left, OptionTemplate right)
        {
            return !Equals(left, right);
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(OptionTemplate other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return string.Equals(this.Template, other.Template) && string.Equals(this.Name, other.Name) && string.Equals(this.ShortName, other.ShortName) && this.ValueNames.SequenceEqual(other.ValueNames);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;

            return this.Equals((OptionTemplate) obj);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Template.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Name.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ShortName.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ValueNames.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}
