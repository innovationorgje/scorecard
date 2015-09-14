using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(scorecard.Startup))]
namespace scorecard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
