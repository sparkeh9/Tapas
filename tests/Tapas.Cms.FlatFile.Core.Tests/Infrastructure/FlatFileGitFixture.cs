namespace Tapas.Cms.FlatFile.Core.Tests.Infrastructure
{
    using System;
    using Microsoft.Extensions.Configuration;

    public class FlatFileGitFixture : IDisposable
    {
        public TestFlatFileCmsOptions Options { get; set; }

        public FlatFileGitFixture()
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile( "appsettings.json" )
                                .Build();

            Options = configuration.Get<TestFlatFileCmsOptions>();
        }

        public void Dispose() { }
    }
}