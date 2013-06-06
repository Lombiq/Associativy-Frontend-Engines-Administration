using System.Collections.Generic;

namespace Associativy.Frontends.Administration.Models
{
    public class FrontendAuthorizationSettings
    {
        public IList<string> Roles { get; set; }

        public FrontendAuthorizationSettings()
        {
            Roles = new List<string>();
        }
    }
}