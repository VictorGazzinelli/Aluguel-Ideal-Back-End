using AluguelIdeal.Application.UnitTests.Interactors.Contacts.Fixtures.Customizations;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System;

namespace AluguelIdeal.Application.UnitTests.Interactors.Contacts.Fixtures
{
    public class ContactInteractorHappyPathFixtureAttribute : AutoDataAttribute
    {
        public static Func<IFixture> fixtureFactory = () =>
            new Fixture()
            .Customize(new AutoMoqCustomization())
            .Customize(new ContactInteractorHappyPathCustomization());

        public ContactInteractorHappyPathFixtureAttribute() : base(fixtureFactory)
        {
        }
    }
}
