using System.Collections.Generic;
using Associativy.Frontends.EngineDiscovery;
using Associativy.GraphDiscovery;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.Core.Common.Utilities;

namespace Associativy.Frontends.Administration.Models.Pages.Admin
{
    public class AssociativyManageGraphFrontendsPart : ContentPart
    {
        private readonly LazyField<IEnumerable<IEngineDescriptor>> _frontendEngines = new LazyField<IEnumerable<IEngineDescriptor>>();
        public LazyField<IEnumerable<IEngineDescriptor>> FrontendEnginesField { get { return _frontendEngines; } }
        public IEnumerable<IEngineDescriptor> FrontendEngines { get { return _frontendEngines.Value; } }

        private readonly LazyField<GraphSettings> _settings = new LazyField<GraphSettings>();
        public LazyField<GraphSettings> SettingsField { get { return _settings; } }
        public GraphSettings GraphSettings
        {
            get { return _settings.Value; }
            set { _settings.Value = value; }
        }
    }
}