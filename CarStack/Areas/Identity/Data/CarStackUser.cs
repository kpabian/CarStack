using Microsoft.AspNetCore.Identity;

namespace CarStack.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CarStackUser class
public class CarStackUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LicenseNumber { get; set; }
}

