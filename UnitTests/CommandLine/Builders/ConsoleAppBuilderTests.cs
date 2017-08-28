﻿using CommandLine.Builders;
using CommandLine.NDesk.Options;
using ErrorHandling;
using UnitTests.TestUtilities;
using Xunit;

namespace UnitTests.CommandLine.Builders
{
    public sealed class ConsoleAppBuilderTests : RandomFileBase
    {
        [Fact]
        public void Parse_UnsupportedOption()
        {
            var ops = new OptionSet { { "test=", "test", v => { } } };

            var data = new ConsoleAppBuilder(new[] { "--if", "-" }, ops)
                .Parse()
                .Data;

            Assert.Equal(data.Errors.Count, 1);
            Assert.Equal(data.UnsupportedOps.Count, 2);
        }

        [Fact]
        public void Parse_Version()
        {
            var ops = new OptionSet { { "test=", "test", v => { } } };

            var validator = new ConsoleAppBuilder(new[] {"--version"}, ops)
                .Parse();

            Assert.True(validator.Data.ShowVersion);

            var exitCode = validator
                .CheckInputFilenameExists("dummy", "vcf", "--in")
                .ShowBanner("authors")
                .ShowHelpMenu("description", "example")
                .ShowErrors()
                .Execute(() => ExitCodes.Success);

            Assert.Equal(ExitCodes.Success, exitCode);
        }

        [Fact]
        public void Parse_HelpMenu()
        {
            var ops = new OptionSet { { "test=", "test", v => { } } };

            var validator = new ConsoleAppBuilder(new[] { "--help" }, ops)
                .Parse();

            Assert.True(validator.Data.ShowHelpMenu);

            var exitCode = validator
                .CheckInputFilenameExists("dummy", "vcf", "--in")
                .ShowBanner("authors")
                .ShowHelpMenu("description", "example")
                .ShowErrors()
                .Execute(() => ExitCodes.Success);

            Assert.Equal(ExitCodes.Success, exitCode);
        }

        [Fact]
        public void Parse_ShowOutput()
        {
            var ops = new OptionSet { { "test=", "test", v => { } } };

            var exitCode = new ConsoleAppBuilder(new[] { "--test", "test" }, ops)
                .Parse()
                .ShowBanner("authors")
                .ShowHelpMenu("description", "example")
                .ShowErrors()
                .Execute(() => ExitCodes.Success);

            Assert.Equal(ExitCodes.Success, exitCode);
        }
    }
}