using Microsoft.AspNetCore.Authorization;

namespace Dev.Naamloos.Ducker.Attributes
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiAuthorizeAttribute() : base("api")
        {

        }
    }
}
