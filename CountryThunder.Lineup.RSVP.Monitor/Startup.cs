using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Messaging.RabbitMQ.Extensions;

namespace CountryThunder.Lineup.RSVP.Monitor
{
    public class Startup
    {
        public const string RECEIVE_AND_CONVERT_QUEUE = "steeltoe_message_queue";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add steeltoe rabbit services
            services.AddRabbitServices();

            // Add singleton that will process incoming messages
            services.AddSingleton<RabbitListenerService>();

            // Tell steeltoe about singleton so it can wire up queues with methods to process queues
            services.AddRabbitListeners<RabbitListenerService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
