﻿using BlazorDraggableList;
using BlazorDynamicLayout;
using BlazorSlider;
using Microsoft.AspNetCore.Http.Connections;
using Mihcelle.Hwavmvid.BrowserResize;
using Mihcelle.Hwavmvid.ColorPicker;
using Mihcelle.Hwavmvid.Devices;
using Mihcelle.Hwavmvid.Download;
using Mihcelle.Hwavmvid.Jsapigeolocation;
using Mihcelle.Hwavmvid.Jsapinotifications;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Hubs;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Tasks;
using Mihcelle.Hwavmvid.Video;
using Mihcelle.Hwavmvid.VideoPlayer;
using Oqtane.ChatHubs.Models;


namespace Mihcelle.Hwavmvid.Modules.ChatHubs
{

    public class Programstartup : Mihcelle.Hwavmvid.Programinterface
    {

        public async Task Configure(IServiceCollection services)
        {


            try
            {

                services.AddScoped<Services.ChatHubService, Services.ChatHubService>();
                services.AddScoped<Services.ScrollService, Services.ScrollService>();
                services.AddScoped<BlazorDraggableListService, BlazorDraggableListService>();
                services.AddScoped<BlazorDynamicLayoutService, BlazorDynamicLayoutService>();
                services.AddScoped<BlazorSliderService, BlazorSliderService>();
                services.AddScoped<BrowserResizeService, BrowserResizeService>();
                services.AddScoped<ColorPickerService, ColorPickerService>();
                services.AddScoped<DevicesService, DevicesService>();
                services.AddScoped<DownloadService, DownloadService>();
                services.AddScoped<Jsapigeolocationservice, Jsapigeolocationservice>();
                services.AddScoped<Jsapibingmapservice, Jsapibingmapservice>();
                services.AddScoped<JsapinotificationService, JsapinotificationService>();
                services.AddScoped<VideoService, VideoService>();
                services.AddScoped<VideoPlayerService, VideoPlayerService>();

                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubRoom>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubRoom>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubUser>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubUser>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubCam>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubCam>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubInvitation>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubInvitation>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubIgnore>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubIgnore>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubIgnoredBy>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubIgnoredBy>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubModerator>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubModerator>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubBlacklistUser>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubBlacklistUser>>();
                services.AddScoped<Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubWhitelistUser>, Mihcelle.Hwavmvid.Pager.Pagerservice<ChatHubWhitelistUser>>();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            services.AddScoped<Applicationdbcontext, Applicationdbcontext>();
            services.AddScoped<Mihcelle.Hwavmvid.Modules.ChatHubs.Providers.ChatHubService, Mihcelle.Hwavmvid.Modules.ChatHubs.Providers.ChatHubService>();
            services.AddScoped<Mihcelle.Hwavmvid.Modules.ChatHubs.Repository.ChatHubRepository, Mihcelle.Hwavmvid.Modules.ChatHubs.Repository.ChatHubRepository>();
            services.AddSingleton<CachingFrontpageTask, CachingFrontpageTask>();
            services.AddSingleton<OnStartupApplicationTask, OnStartupApplicationTask>();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = false;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                    options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
                    options.JsonSerializerOptions.DefaultBufferSize = 4096;
                    options.JsonSerializerOptions.MaxDepth = 41;
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddServerSideBlazor()
                .AddHubOptions(options => options.MaximumReceiveMessageSize = 512 * 1024);

            /*
            services.AddCors(option =>
            {
                option.AddPolicy("wasmcorspolicy", (builder) =>
                {
                    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                           .SetIsOriginAllowed(isOriginAllowed => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            */

            services.AddSignalR()
                .AddHubOptions<ChatHub>(options =>
                {
                    options.EnableDetailedErrors = true;
                    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
                    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
                    options.MaximumReceiveMessageSize = Int64.MaxValue;
                    options.StreamBufferCapacity = Int32.MaxValue;
                })
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.WriteIndented = false;
                    options.PayloadSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                    options.PayloadSerializerOptions.AllowTrailingCommas = true;
                    options.PayloadSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
                    options.PayloadSerializerOptions.DefaultBufferSize = 4096;
                    options.PayloadSerializerOptions.MaxDepth = 41;
                    options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                });
            
        }

        public async Task Configureapp(WebApplication app)
        {
            
            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseTenantResolution();
            //app.UseBlazorFrameworkFiles();
            //app.UseRouting();
            //app.UseCors("wasmcorspolicy");
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapHub<ChatHub>("/api/chathub", options =>
            {
                options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
                options.ApplicationMaxBufferSize = long.MaxValue;
                options.TransportMaxBufferSize = long.MaxValue;
                options.WebSockets.CloseTimeout = TimeSpan.FromSeconds(10);
                options.LongPolling.PollTimeout = TimeSpan.FromSeconds(10);
            });
            
        }
    }
}
