using System;
using Associativy.Frontends.Administration.Services;
using Associativy.Frontends.Models;
using Associativy.GraphDiscovery;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Orchard.Security;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Administration.Handlers
{
    [OrchardFeature("Associativy.Frontends.Administration.Authorization")]
    public class AuthorizationFrontendEnginePageHandler : IAuthorizationServiceEventHandler
    {
        private readonly IFrontendAuthorizer _frontendAuthorizationService;
        private readonly IWorkContextAccessor _workContextAccessor;


        public AuthorizationFrontendEnginePageHandler(
            IFrontendAuthorizer frontendAuthorizationService,
            IWorkContextAccessor workContextAccessor)
        {
            _frontendAuthorizationService = frontendAuthorizationService;
            _workContextAccessor = workContextAccessor;
        }

    
        public void Checking(CheckAccessContext context)
        {
        }

        public void Adjust(CheckAccessContext context)
        {
        }

        public void Complete(CheckAccessContext context)
        {
            if (context.Content == null) return;

            var pageContext = context.PageContext();

            if (pageContext.Group != FrontendsPageConfigs.Group) return;

            context.Granted = _frontendAuthorizationService.IsAuthorizedToView(
                _workContextAccessor.GetContext().CurrentUser,
                pageContext.Page.As<IEngineConfigurationAspect>().GraphDescriptor.MaximalContext());
        }
    }
}