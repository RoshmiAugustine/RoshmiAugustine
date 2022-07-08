using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Opeeka.PICS.UnitTests.Api.Common
{
    public class MockUserIdentity
    {
        private readonly ClaimsPrincipal User;
        public MockUserIdentity()
        {
            this.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, "sooraj"),
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim("TenantId", "100"),
                    new Claim("UserId", "1"),
                    new Claim("b2cToken", "xxx-xxxx-xx"),
                    new Claim("Email", "tsooraj1990@gmail.com"),
                    new Claim("AgencyAbbrev", "10"),
                    new Claim("RoleID", "1"),
                     }, "mock"));
        }
        public ControllerContext MockGetClaimsIdentity()
        {
            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = User }
            };
        }
    }
}
