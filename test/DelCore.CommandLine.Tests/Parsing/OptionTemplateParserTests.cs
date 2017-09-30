#region Copyright 2017 Shane Delany

// Filename:  OptionTemplateParserTests.cs
// Modified:  30/09/2017

#endregion

using System;
using System.Collections;

using DelCore.CommandLine.Parsing;

using NUnit.Framework;

namespace DelCore.CommandLine.Tests.Parsing
{
    [TestFixture]
    public class OptionTemplateParserTests
    {
        #region Constants and Fields

        private OptionTemplateParser Parser;

        #endregion

        #region Methods

        [SetUp]
        public void SetUp()
        {
            this.Parser = new OptionTemplateParser();
        }

        [TearDown]
        public void TearDown()
        {
            this.Parser = null;
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.TemplateValues))]
        public OptionTemplate Parse_ParsesTemplateValuesCorrectly(string value)
        {
            return this.Parser.Parse(value);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.ParseNameValues))]
        public string ParseName_ParsesNameValuesCorrectly(string value)
        {
            return this.Parser.ParseName(value);
        }

        [Test]
        public void ParseName_ThrowsArgumentNullExceptionOnNullValue()
        {
            Assert.That(() => this.Parser.ParseName(null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.ParseShortNameValues))]
        public string ParseShortName_ParsesShortNameValuesCorrectly(string value)
        {
            return this.Parser.ParseShortName(value);
        }

        [Test]
        public void ParseShortName_ThrowsArgumentNullExceptionOnNullValue()
        {
            Assert.That(() => this.Parser.ParseShortName(null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.ParseArgumentValues))]
        public string ParseArgument_ParsesArgumentValuesCorrectly(string value)
        {
            return this.Parser.ParseArgument(value);
        }

        [Test]
        public void ParseArgument_ThrowsArgumentNullExceptionOnNullValue()
        {
            Assert.That(() => this.Parser.ParseArgument(null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.IsNameValues))]
        public bool IsName_IdentifiesNameValuesCorrectly(string value)
        {
            return this.Parser.IsName(value);
        }

        [Test]
        public void IsName_ThrowsArgumentNullExceptionOnNullValue()
        {
            Assert.That(() => this.Parser.IsName(null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.IsShortNameValues))]
        public bool IsShortName_IdentifiesShortNameValuesCorrectly(string value)
        {
            return this.Parser.IsShortName(value);
        }

        [Test]
        public void IsShortName_ThrowsArgumentNullExceptionOnNullValue()
        {
            Assert.That(() => this.Parser.IsShortName(null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.IsArgumentValues))]
        public bool IsArgument_IdentifiesArgumentValuesCorrectly(string value)
        {
            return this.Parser.IsArgument(value);
        }

        [Test]
        public void IsArgument_ThrowsArgumentNullExceptionOnNullValue()
        {
            Assert.That(() => this.Parser.IsArgument(null), Throws.ArgumentNullException);
        }

        #endregion

        #region Types

        private static class TestData
        {
            #region Constants and Fields

            public static IEnumerable TemplateValues
            {
                get
                {
                    yield return new TestCaseData("--name").Returns(new OptionTemplate("--name", "name"));
                    yield return new TestCaseData("-n").Returns(new OptionTemplate("-n", shortName: "n"));
                    yield return new TestCaseData("-n | --name").Returns(new OptionTemplate("-n | --name", "name", "n"));
                    yield return new TestCaseData("--name <RequiredArgument>").Returns(new OptionTemplate("--name <RequiredArgument>", "name", valueNames: new[] {"RequiredArgument"}));
                    yield return new TestCaseData("-n <RequiredArgument>").Returns(new OptionTemplate("-n <RequiredArgument>", shortName: "n", valueNames: new[] {"RequiredArgument"}));
                    yield return new TestCaseData("-n | --name <RequiredArgument>").Returns(new OptionTemplate("-n | --name <RequiredArgument>", "name", "n", new[] {"RequiredArgument"}));
                    yield return new TestCaseData("--name [OptionalArgument]").Returns(new OptionTemplate("--name [OptionalArgument]", "name", valueNames: new[] {"OptionalArgument"}));
                    yield return new TestCaseData("-n [OptionalArgument]").Returns(new OptionTemplate("-n [OptionalArgument]", shortName: "n", valueNames: new[] {"OptionalArgument"}));
                    yield return new TestCaseData("-n | --name [OptionalArgument]").Returns(new OptionTemplate("-n | --name [OptionalArgument]", "name", "n", new[] {"OptionalArgument"}));
                }
            }

            public static IEnumerable ParseNameValues
            {
                get { yield return new TestCaseData("--name").Returns("name"); }
            }

            public static IEnumerable ParseShortNameValues
            {
                get { yield return new TestCaseData("-n").Returns("n"); }
            }

            public static IEnumerable ParseArgumentValues
            {
                get
                {
                    yield return new TestCaseData("<RequiredArgument>").Returns("RequiredArgument");
                    yield return new TestCaseData("[OptionalArgument]").Returns("OptionalArgument");
                }
            }

            public static IEnumerable IsNameValues
            {
                get
                {
                    // Valid
                    yield return new TestCaseData("--name").Returns(true);

                    // Invalid
                    yield return new TestCaseData("--name1").Returns(false);
                    yield return new TestCaseData("--name$").Returns(false);
                    yield return new TestCaseData("-n").Returns(false);
                    yield return new TestCaseData("-$").Returns(false);
                    yield return new TestCaseData("name").Returns(false);
                    yield return new TestCaseData("<argument>").Returns(false);
                    yield return new TestCaseData("[argument]").Returns(false);
                }
            }

            public static IEnumerable IsShortNameValues
            {
                get
                {
                    // Valid
                    yield return new TestCaseData("-n").Returns(true);

                    // Invalid
                    yield return new TestCaseData("--name").Returns(false);
                    yield return new TestCaseData("-name").Returns(false);
                    yield return new TestCaseData("-$").Returns(false);
                    yield return new TestCaseData("-1").Returns(false);
                    yield return new TestCaseData("name").Returns(false);
                    yield return new TestCaseData("<argument>").Returns(false);
                    yield return new TestCaseData("[argument]").Returns(false);
                }
            }

            public static IEnumerable IsArgumentValues
            {
                get
                {
                    // Valid
                    yield return new TestCaseData("<argument>").Returns(true);
                    yield return new TestCaseData("[argument]").Returns(true);

                    // Invalid
                    yield return new TestCaseData("<argument1>").Returns(false);
                    yield return new TestCaseData("[argument1]").Returns(false);
                    yield return new TestCaseData("<argument$>").Returns(false);
                    yield return new TestCaseData("[argument$]").Returns(false);
                    yield return new TestCaseData("(argument)").Returns(false);
                }
            }

            #endregion
        }

        #endregion
    }
}
