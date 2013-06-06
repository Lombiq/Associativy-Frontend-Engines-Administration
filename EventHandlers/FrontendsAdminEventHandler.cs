using Associativy.Administration;
using Associativy.Frontends.Administration.Models.Pages.Admin;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Administration.EventHandlers
{
    public class FrontendsAdminEventHandler : IPageEventHandler
    {
        public void OnPageInitializing(PageContext pageContext)
        {
            if (pageContext.Group != AdministrationPageConfigs.Group) return;

            var page = pageContext.Page;
            if (page.IsPage("ManageGraph", pageContext.Group))
            {
                page.ContentItem.Weld(new AssociativyManageGraphFrontendsPart());
            }
        }

        public void OnPageInitialized(PageContext pageContext)
        {
        }

        public void OnPageBuilt(PageContext pageContext)
        {
        }

        public void OnAuthorization(PageAutorizationContext authorizationContext)
        {
        }
    }
}