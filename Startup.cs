using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bài_test.Startup))]
namespace Bài_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
