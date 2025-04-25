using System.Drawing;
using Mihcelle.Hwavmvid.Modules.Roulette;

namespace Mihcelle.Hwavmvid.Modules.Roulette
{

    public class Programstartup : Mihcelle.Hwavmvid.Programinterface
    {

        public async Task Configure(IServiceCollection services)
        {

            services.AddScoped<Applicationdbcontext, Applicationdbcontext>();

            services.AddScoped<Itellisense.RouletteitellisenseService, Itellisense.RouletteitellisenseService>();
            services.AddScoped<Bets.RouletteBetsService, Bets.RouletteBetsService>();
            services.AddScoped<Betoptions.RouletteBetoptionsService, Betoptions.RouletteBetoptionsService>();
            services.AddScoped<Betscores.RouletteBetscoresService, Betscores.RouletteBetscoresService>();
            services.AddScoped<Coins.RoulettecoinsService, Coins.RoulettecoinsService>();
            services.AddScoped<Roulette.RouletteService, Roulette.RouletteService>();
            services.AddScoped<Surface.RoulettesurfaceService, Surface.RoulettesurfaceService>();

        }

        public async Task Configureapp(WebApplication application)
        {

        }

    }
}
