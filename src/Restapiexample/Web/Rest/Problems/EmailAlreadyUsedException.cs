namespace Compuletra.RestApiExample.Web.Rest.Problems {
    public class EmailAlreadyUsedException : BadRequestAlertException {
        public EmailAlreadyUsedException() : base(ErrorConstants.EmailAlreadyUsedType, "Email is already in use!",
            "userManagement", "emailexists")
        {
        }
    }
}
