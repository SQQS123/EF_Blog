using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogSystem.MVCSite.Startup))]
namespace BlogSystem.MVCSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
