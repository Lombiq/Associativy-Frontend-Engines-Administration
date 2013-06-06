using System.Linq;
using Associativy.Frontends.Administration.Models.Pages.Admin;
using Associativy.Frontends.Administration.Services;
using Associativy.GraphDiscovery;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Mvc;
using Orchard.Roles.Services;

namespace Associativy.Frontends.Administration.Drivers.Pages.Admin
{
    [OrchardFeature("Associativy.Frontends.Administration.Authorization")]
    public class AssociativyManageGraphAuthorizationPartDiver : ContentPartDriver<AssociativyManageGraphAuthorizationPart>
    {
        private readonly IFrontendAuthorizer _frontendAuthorizer;
        private readonly IRoleService _roleService;

        protected override string Prefix
        {
            get { return "Associativy.Frontends.Administration.AssociativyManageGraphAuthorizationPart"; }
        }


        public AssociativyManageGraphAuthorizationPartDiver(
            IFrontendAuthorizer frontendAuthorizer,
            IRoleService roleService)
        {
            _frontendAuthorizer = frontendAuthorizer;
            _roleService = roleService;
        }


        protected override DriverResult Display(AssociativyManageGraphAuthorizationPart part, string displayType, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }

        protected override DriverResult Editor(AssociativyManageGraphAuthorizationPart part, dynamic shapeHelper)
        {
            return ContentShape("Pages_AssociativyManageGraphAuthorization",
                () =>
                {
                    var authorizedRoles = _frontendAuthorizer.GetAuthorizedToView(CurrentContext(part));
                    part.Roles = _roleService.GetRoles().Select(role => new RoleEntry { Name = role.Name, IsAuthorized = authorizedRoles.Contains(role.Name) }).ToList();

                    return shapeHelper.DisplayTemplate(
                                TemplateName: "Pages/Admin/ManageGraphAuthorization",
                                Model: part,
                                Prefix: Prefix);
                });
        }

        protected override DriverResult Editor(AssociativyManageGraphAuthorizationPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if (updater.TryUpdateModel(part, Prefix, null, null))
            {
                _frontendAuthorizer.SetAuthorizedToView(CurrentContext(part), part.Roles.Where(role => role.IsAuthorized).Select(role => role.Name));
            }

            return Editor(part, shapeHelper);
        }

        private static IGraphContext CurrentContext(AssociativyManageGraphAuthorizationPart part)
        {
            return part.As<Associativy.Administration.Models.Pages.Admin.AssociativyManageGraphPart>().GraphDescriptor.MaximalContext();
        }
    }
}