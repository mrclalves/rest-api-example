using System.Security.Claims;

namespace Compuletra.RestApiExample.Test.Setup {
    public class MockClaimsPrincipalProvider {
        public MockClaimsPrincipalProvider(ClaimsPrincipal user)
        {
            User = user;
        }

        public ClaimsPrincipal User { get; }
    }
}
