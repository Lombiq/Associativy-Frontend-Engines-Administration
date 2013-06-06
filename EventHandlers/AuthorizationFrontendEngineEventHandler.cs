using Associativy.Frontends.Administration.Services;
using Associativy.Frontends;
using Associativy.Frontends.Models;
using Associativy.GraphDiscovery;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Administration.EventHandlers
{
    [OrchardFeature("Associativy.Frontends.Administration.Authorization")]
    public class AuthorizationFrontendEngineEventHandler : IPageEventHandler
    {
        private readonly IFrontendAuthorizer _frontendAuthorizationService;
        private readonly IWorkContextAccessor _workContextAccessor;


        public AuthorizationFrontendEngineEventHandler(
            IFrontendAuthorizer frontendAuthorizationService,
            IWorkContextAccessor workContextAccessor)
        {
            _frontendAuthorizationService = frontendAuthorizationService;
            _workContextAccessor = workContextAccessor;
        }


        public void OnPageInitializing(PageContext pageContext)
        {
        }

        public void OnPageInitialized(PageContext pageContext)
        {
        }

        public void OnPageBuilt(PageContext pageContext)
        {
        }

        public void OnAuthorization(PageAutorizationContext authorizationContext)
        {
            if (authorizationContext.Group != FrontendsPageConfigs.Group) return;

            authorizationContext.Granted = _frontendAuthorizationService.IsAuthorizedToView(
                _workContextAccessor.GetContext().CurrentUser,
                authorizationContext.Page.As<IEngineConfigurationAspect>().GraphDescriptor.MaximalContext());
        }
    }
}