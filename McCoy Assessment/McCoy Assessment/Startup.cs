using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(McCoy_Assessment.Startup))]
namespace McCoy_Assessment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
