using Associativy.Administration;
using Associativy.Frontends.Administration.Models.Pages.Admin;
using Orchard.ContentManagement.Handlers;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using Piedone.HelpfulLibraries.Contents;

namespace Associativy.Frontends.Administration.Handlers
{
    public class FrontendsAdminPageHandler : ContentHandler
    {
        protected override void Initializing(InitializingContentContext context)
        {
            var pageContext = context.PageContext();

            if (pageContext.Group != AdministrationPageConfigs.Group) return;

            if (pageContext.Page.IsPage("ManageGraph", pageContext.Group))
            {
                pageContext.Page.Weld<AssociativyManageGraphFrontendsPart>();
            }
        }
    }
}