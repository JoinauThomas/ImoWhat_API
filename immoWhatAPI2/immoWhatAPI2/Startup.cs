using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(immoWhatAPI2.Startup))]
namespace immoWhatAPI2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
