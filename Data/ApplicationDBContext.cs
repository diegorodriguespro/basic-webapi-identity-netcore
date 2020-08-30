using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace web_identity_csharp_base.Data
{
    public class ApplicationDBContext: IdentityDbContext
    {
        public ApplicationDBContext()
        {

        }

        public ApplicationDBContext(DbContextOptions options): base (options)
        {

        }

    }
}
