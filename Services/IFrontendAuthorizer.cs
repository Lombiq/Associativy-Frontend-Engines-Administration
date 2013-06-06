using System.Collections.Generic;
using Associativy.GraphDiscovery;
using Orchard;
using Orchard.Security;

namespace Associativy.Frontends.Administration.Services
{
    public interface IFrontendAuthorizer : IDependency
    {
        void SetAuthorizedToView(IGraphContext graphContext, IEnumerable<string> roles);
        IEnumerable<string> GetAuthorizedToView(IGraphContext graphContext);
        bool IsAuthorizedToView(IUser user, IGraphContext graphContext);
    }
}
