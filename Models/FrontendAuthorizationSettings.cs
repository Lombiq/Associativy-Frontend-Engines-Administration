using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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