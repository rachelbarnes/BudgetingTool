using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetTool.Startup))]
namespace BudgetTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
