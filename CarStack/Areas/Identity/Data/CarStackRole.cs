using Microsoft.AspNetCore.Identity;

namespace CarStack.Areas.Identity.Data;

public class CarStackRole : IdentityRole<int>
{
    public CarStackRole() { }

    public CarStackRole(string roleName) : base(roleName) { }
}
