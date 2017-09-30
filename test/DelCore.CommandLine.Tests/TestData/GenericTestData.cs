#region Copyright 2017 Shane Delany

// Filename:  GenericTestData.cs
// Modified:  26/09/2017

#endregion

using System;
using System.Collections;

using NUnit.Framework;

namespace DelCore.CommandLine.Tests.TestData
{
    public static class GenericTestData
    {
        #region Constants and Fields

        public static IEnumerable NullEmptyWhitespaceString
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData("");
                yield return new TestCaseData(" ");
            }
        }

        #endregion
    }
}
