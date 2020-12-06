using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace REST.Models
{
	public class ModelFactory
	{
		private UrlHelper _UrlHelper;
		private ApplicationUserManager _AppUserManager;

		public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
		{
			_UrlHelper = new UrlHelper(request);
			_AppUserManager = appUserManager;
		}

		public RoleReturnModel Create(IdentityRole appRole)
		{

			return new RoleReturnModel
			{
				Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
				Id = appRole.Id,
				Name = appRole.Name
			};
		}
	}

	public class RoleReturnModel
	{
		public string Url { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
	}
}