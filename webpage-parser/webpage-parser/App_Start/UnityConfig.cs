using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using webpage_parser.Services;

namespace webpage_parser
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();
			container.RegisterType<IParserService, ParserService>();
			container.RegisterType<IDocumentProcessService, DocumentProcessService>();
			DependencyResolver.SetResolver(new UnityDependencyResolver(container));
		}
	}
}