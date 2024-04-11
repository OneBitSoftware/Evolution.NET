using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RazorClasses.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClasses
{
    public class TestMailCreation
    {
        static async Task Main(string[] args)
        {
            await BuildEmails();
        }

        private static async Task BuildEmails()
        {
            IServiceProvider serviceProvider = ConfigureServices();
            ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            await using HtmlRenderer renderer = new(serviceProvider, loggerFactory);

            var emailData = GetEmailData();

            await renderer.Dispatcher.InvokeAsync(async () =>
            {
                foreach (var item in emailData)
                {
                    var parameters = ParameterView.FromDictionary(ConvertToDictionary(item));
                    var output = await renderer.RenderComponentAsync<Body>(parameters);
                    var result = output.ToHtmlString();
                    await Console.Out.WriteLineAsync(result);
                    result += Environment.NewLine;
                };
            });
        }

        private static IList<EmailContext> GetEmailData()
        {
            return new List<EmailContext>
                {
                    new EmailContext() { CompanyName = "OneBit Software", Username = "Tony Troeff", Pronoun = "He/Him", Copywrite = "2024", Disclaimer = "Make GDPR equal for all."},
                    new EmailContext() { CompanyName = "OneBit Software", Username = "Radi Atanassov", Pronoun = "He/Her", Copywrite = "2024", Disclaimer = "Make GDPR equal for all."}
                };
        }

        private static Dictionary<string, object?> ConvertToDictionary(EmailContext emailData)
        {
            return new Dictionary<string, object?>()
            {
                { "Context", emailData }
            };
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddLogging();

            return services.BuildServiceProvider();
        }
    }
}
