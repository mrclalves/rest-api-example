using System.Security.Authentication;

namespace Compuletra.RestApiExample.Security {
    public class UsernameNotFoundException : AuthenticationException {
        public UsernameNotFoundException(string message) : base(message)
        {
        }
    }
}
