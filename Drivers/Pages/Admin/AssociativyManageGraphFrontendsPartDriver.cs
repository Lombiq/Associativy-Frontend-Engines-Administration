using Associativy.Administration.Models.Pages.Admin;
using Associativy.Administration.Services;
using Associativy.Frontends.Administration.Models;
using Associativy.Frontends.Administration.Models.Pages.Admin;
using Associativy.Frontends.EngineDiscovery;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Associativy.Frontends.Administration.Drivers.Pages.Admin
{
    public class AssociativyManageGraphFrontendsPartDriver : ContentPartDriver<AssociativyManageGraphFrontendsPart>
    {
        private readonly IEngineManager _engineManager;
        private readonly IGraphSettingsService _settingsService;

        protected override string Prefix
        {
            get { return "Associativy.Frontends.Administration.AssociativyManageGraphFrontendsPart"; }
        }


        public AssociativyManageGraphFrontendsPartDriver(
            IEngineManager engineManager,
            IGraphSettingsService settingsService)
        {
            _engineManager = engineManager;
            _settingsService = settingsService;
        }


        protected override DriverResult Display(AssociativyManageGraphFrontendsPart part, string displayType, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }

        protected override DriverResult Editor(AssociativyManageGraphFrontendsPart part, dynamic shapeHelper)
        {
            SetupLazyLoaders(part);

            return Combined(
                ContentShape("Pages_AssociativyManageGraphFrontends_Engines",
                    () => shapeHelper.DisplayTemplate(
                                    TemplateName: "Pages/Admin/ManageGraphFrontends.Engines",
                                    Model: part,
                                    Prefix: Prefix)),
                ContentShape("Pages_AssociativyManageGraphFrontends_Settings",
                    () => shapeHelper.DisplayTemplate(
                                    TemplateName: "Pages/Admin/ManageGraphFrontends.Settings",
                                    Model: part,
                                    Prefix: Prefix))
                );
        }

        protected override DriverResult Editor(AssociativyManageGraphFrontendsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            SetupLazyLoaders(part);

            updater.TryUpdateModel(part, Prefix, null, null);
            if (part.GraphSettings.InitialZoomLevel > part.GraphSettings.ZoomLevelCount) part.GraphSettings.InitialZoomLevel = part.GraphSettings.ZoomLevelCount - 1;
            _settingsService.Set(part.As<AssociativyManageGraphPart>().GraphDescriptor.Name, part.GraphSettings);

            return Editor(part, shapeHelper);
        }

        private void SetupLazyLoaders(AssociativyManageGraphFrontendsPart part)
        {
            part.SettingsField.Loader(() => _settingsService.GetNotNull<GraphSettings>(part.As<AssociativyManageGraphPart>().GraphDescriptor.Name));
            part.FrontendEnginesField.Loader(() => _engineManager.GetEngines());
        }
    }
}