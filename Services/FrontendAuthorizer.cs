using System.Collections.Generic;
using System.Linq;
using Associativy.Administration.Services;
using Associativy.Frontends.Administration.Models;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Roles.Models;
using Orchard.Security;

namespace Associativy.Frontends.Administration.Services
{
    [OrchardFeature("Associativy.Frontends.Administration.Authorization")]
    public class FrontendAuthorizer : IFrontendAuthorizer
    {
        private readonly IGraphSettingsService _settingsService;


        public FrontendAuthorizer(IGraphSettingsService settingsService)
        {
            _settingsService = settingsService;
        }


        public void SetAuthorizedToView(IGraphContext graphContext, IEnumerable<string> roles)
        {
            _settingsService.Set(graphContext.Name, new FrontendAuthorizationSettings { Roles = roles.ToList() });
        }

        public IEnumerable<string> GetAuthorizedToView(IGraphContext graphContext)
        {
            return _settingsService.GetNotNull<FrontendAuthorizationSettings>(graphContext.Name).Roles;
        }

        public bool IsAuthorizedToView(IUser user, IGraphContext graphContext)
        {
            var roles = GetAuthorizedToView(graphContext);
            if (roles.Contains("Anonymous")) return true;
            if (user == null) return false;
            return user.As<IUserRoles>().Roles.Intersect(roles).Count() != 0;
        }
    }
}