using System.Security.Authentication;

namespace Compuletra.RestApiExample.Security {
    public class UserNotActivatedException : AuthenticationException {
        public UserNotActivatedException(string message) : base(message)
        {
        }
    }
}
