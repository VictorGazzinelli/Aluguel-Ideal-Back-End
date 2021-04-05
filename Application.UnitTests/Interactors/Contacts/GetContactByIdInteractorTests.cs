using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Interactors.Contacts
{
    [Trait("Contacts", "GetContactByIdInteractor")]
    public class GetContactByIdInteractorTests
    {
        public async Task ShouldReturnClients()
        {
            var t = true;
            t.Should().BeTrue();
        }
    }
}
