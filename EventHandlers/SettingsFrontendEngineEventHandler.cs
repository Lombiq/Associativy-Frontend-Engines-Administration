using Associativy.Administration.Services;
using Associativy.Frontends.Models;
using Associativy.Services;
using Orchard.ContentManagement;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Administration.EventHandlers
{
    public class SettingsFrontendEngineEventHandler : IPageEventHandler
    {
        private readonly IGraphSettingsService _settingsService;
        private readonly IGraphCacheService _cacheService;


        public SettingsFrontendEngineEventHandler(IGraphSettingsService settingsService, IGraphCacheService cacheService)
        {
            _settingsService = settingsService;
            _cacheService = cacheService;
        }


        public void OnPageInitializing(PageContext pageContext)
        {
        }

        public void OnPageInitialized(PageContext pageContext)
        {
            if (pageContext.Group != FrontendsPageConfigs.Group) return;

            var config = pageContext.Page.As<IEngineConfigurationAspect>();
            var settings = _settingsService.GetNotNull<Associativy.Frontends.Administration.Models.GraphSettings>(config.GraphDescriptor.Name);

            _cacheService.SetEnabledStateForRequest(config.GraphDescriptor, settings.UseCache);
            config.MindSettings.MaxDistance = settings.MaxDistance;
            config.GraphSettings.InitialZoomLevel = settings.InitialZoomLevel;
            config.GraphSettings.ZoomLevelCount = settings.ZoomLevelCount;
            config.GraphSettings.MaxConnectionCount = settings.MaxConnectionCount;
        }

        public void OnPageBuilt(PageContext pageContext)
        {
        }

        public void OnAuthorization(PageAutorizationContext authorizationContext)
        {
        }
    }
}