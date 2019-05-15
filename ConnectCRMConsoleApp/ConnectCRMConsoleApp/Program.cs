using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;

namespace ConnectCRMConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = ConfigurationManager.AppSettings["userName"].ToString();
            clientCredentials.UserName.Password = ConfigurationManager.AppSettings["password"];
            Console.WriteLine("User name: "+ ConfigurationManager.AppSettings["userName"].ToString()); 
            //Below security protocol wasn't present in earlier versions of CRM
            // For Dynamics 365 Customer Engagement V9.X, set Security Protocol as TLS12
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Get the URL from CRM, Navigate to Settings -> Customizations -> Developer Resources
            // Copy and Paste Organization Service Endpoint Address URL              
            //string environment = "https://fellow.crm8.dynamics.com/main.aspx";
            string uri = ConfigurationManager.AppSettings["uri"].ToString();
            Console.WriteLine("uri: "+ ConfigurationManager.AppSettings["uri"].ToString());

            Console.WriteLine("Creating organizationService...");
            IOrganizationService organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri(uri), null, clientCredentials, null);
            Console.WriteLine("Executing whoamirequest...");
            Guid userid = ((WhoAmIResponse)organizationService.Execute(new WhoAmIRequest())).UserId;

            if (userid != Guid.Empty)
            {
                Console.WriteLine("Who am i?: " + userid);
                Console.WriteLine("Connection Established Successfully!");
                Console.Read();
            }
        }
    }
}
