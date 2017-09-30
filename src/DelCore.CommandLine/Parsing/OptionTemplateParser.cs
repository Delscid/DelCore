#region Copyright 2017 Shane Delany

// Filename:  OptionTemplateParser.cs
// Modified:  28/09/2017

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace DelCore.CommandLine.Parsing
{
    public class OptionTemplateParser
    {
        #region Methods

        public OptionTemplate Parse(string template)
        {
            template = template ?? throw new ArgumentNullException(nameof(template));

            string name = String.Empty;
            string shortName = String.Empty;
            List<string> values = new List<string>();

            string[] parts = template.Split(new[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (this.IsArgument(part))
                {
                    values.Add(this.ParseArgument(part));
                }
                else if (this.IsShortName(part))
                {
                    shortName = this.ParseShortName(part);
                }
                else if (this.IsName(part))
                {
                    name = this.ParseName(part);
                }
            }

            return new OptionTemplate(template, name, shortName, values);
        }

        public bool IsName(string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            return value.StartsWith("--") && this.IsEnglishString(value.Substring(2));
        }

        public bool IsShortName(string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            return value.StartsWith("-") && value.Length == 2 && this.IsEnglishString(value.Substring(1));
        }

        public bool IsArgument(string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            return value.StartsWith("<") && value.EndsWith(">") && this.IsEnglishString(value.Trim('[', ']', '<', '>')) ||
                   value.StartsWith("[") && value.EndsWith("]") && this.IsEnglishString(value.Trim('[', ']', '<', '>'));
        }

        public string ParseName(string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            return value.TrimStart('-');
        }

        public string ParseShortName(string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            return value.TrimStart('-');
        }

        public string ParseArgument(string value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));
            return value.Trim('<', '>', '[', ']');
        }

        private bool IsEnglishString(string value)
        {
            return value.ToCharArray().All(this.IsEnglishCharacter);
        }

        private bool IsEnglishCharacter(char value)
        {
            return (value >= 'a' && value <= 'z') || (value >= 'A' && value <= 'Z');
        }

        #endregion
    }
}
