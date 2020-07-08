namespace Compuletra.RestApiExample.Web.Rest.Problems {
    public class LoginAlreadyUsedException : BadRequestAlertException {
        public LoginAlreadyUsedException() : base(ErrorConstants.EmailAlreadyUsedType, "Login name is already in use!",
            "userManagement", "userexists")
        {
        }
    }
}
