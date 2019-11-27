using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoplightController.API.Logic;
using Unosquare.RaspberryIO;
using Unosquare.WiringPi;

namespace StoplightController.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public LightStateObservable LightStateObservable = new LightStateObservable();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsEnvironment("local"))
            {
                StoplightModeManager.Initialize(new LoggingStoplightStateManager(LightStateObservable));
            }
            else
            {
                Pi.Init<BootstrapWiringPi>();
                StoplightModeManager.Initialize(new GpioStoplightStateManager(LightStateObservable, new Dictionary<LightType, int>
                {
                    { LightType.Green, 17 },
                    { LightType.Yellow, 27 },
                    { LightType.Red, 22 }
                }));
            }

            app.UseStaticFiles();

            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var websocket = await context.WebSockets.AcceptWebSocketAsync();

                        var websocketConnection = new WebSocketConnection(websocket);

                        var unsub = LightStateObservable.Subscribe(
                            new LightStateWebsocketObserver(websocket));

                        await websocketConnection.ReceiveMessagesUntilCloseAsync();

                        if (websocketConnection.CloseStatus.HasValue)
                        {
                            await websocket.CloseAsync(websocketConnection.CloseStatus.Value,
                                websocketConnection.CloseStatusDescription, CancellationToken.None);
                        }

                        Console.WriteLine("Unsubscribing after Closing Websocket");
                        unsub.Dispose();
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });

            app.UseMvc();
        }
    }
}
