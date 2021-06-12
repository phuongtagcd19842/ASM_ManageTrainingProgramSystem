using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASM_ManageTrainingProgramSystem.Startup))]
namespace ASM_ManageTrainingProgramSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
