using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Codidact
{
    public class Role : IdentityRole<int> { }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }
    public class CustomUserToken : IdentityUserToken<int> { }

    // Add profile data for application users by adding properties to the Users class
    public class Users : IdentityUser<int>
    {
        [PersonalData]
        public string AboutMe { get; set; }

        public DateTime CreationDate { get; set; }

        [Required, MaxLength(40), PersonalData]
        public string DisplayName { get; set; }

    }
}
