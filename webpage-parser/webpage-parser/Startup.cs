using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webpage_parser.Startup))]
namespace webpage_parser
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
