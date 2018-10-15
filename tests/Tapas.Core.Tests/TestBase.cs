namespace Tapas.Core.Tests
{
    using AutoFixture;
    using AutoFixture.AutoNSubstitute;

    public abstract class TestBase<T>
    {
        protected IFixture Fixture { get; set; }

        protected TestBase()
        {
            Fixture = new Fixture().Customize( new AutoNSubstituteCustomization() );
        }

        public T SystemUnderTest()
        {
            return Fixture.Create<T>();
        }
    }
}