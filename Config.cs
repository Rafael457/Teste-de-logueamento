using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;

namespace bibliotecalogteste
{
    public class Config
    {
        private readonly IConfiguration Configuration;

        public Config(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureKissLog(IOptionsBuilder options)
        {
            KissLogConfiguration.Listeners.Add(new RequestLogsApiListener(new Application(
                Configuration["KissLog.OrganizationId"],    //  "e1c63e10-9f4f-4826-a11c-1487e90cf9fa"
                Configuration["KissLog.ApplicationId"])     //  "ba6ad2be-c494-48af-918b-931988d6d245"
            )
            {
                ApiUrl = Configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });
        }
    }
}
