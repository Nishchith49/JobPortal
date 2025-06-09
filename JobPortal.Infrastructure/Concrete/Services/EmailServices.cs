using JobPortal.Infrastructure.Concrete.IServices;
using JobPortal.Infrastructure.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics;

namespace JobPortal.Infrastructure.Concrete.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public EmailServices(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        private async Task<string> ReadTemplate(string templateName)
        {
            string pathToFile = $"{_environment.ContentRootPath}/Templates{Path.DirectorySeparatorChar}{templateName}";
            string builder = "";
            using (StreamReader reader = System.IO.File.OpenText(pathToFile))
            {
                builder = await reader.ReadToEndAsync();
            }
            return builder;
        }

        public async Task SendOtp(string OTP, string email)
        {
            var template = await ReadTemplate(TemplateName.Otp);
            template = template.Replace(TemplateReplaceStrings.ReplaceOTP, OTP);
            var emails = new List<string>
            {
                email.ToLower() == "admin@admin.com" ? "nishchithdn5551@gmail.com" : email
            };
            await SendMultipleEmail(EmailSubject.OtpSubject, emails, template);
        }


        public async Task SendExceptionMail(IExceptionHandlerFeature ex, HttpContext context)
        {
            var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.Source}<hr />{context.Request.Path}<br />";

            var code = context.Response.StatusCode;
            err += $"Query Parameters if present :- {context.Request.QueryString}<hr/>";
            err += $"Response Status Code :- {code}<hr/>";
            err += $"Time :- {DateTime.Now} <hr/>";
            err += $"BaseUrl :- {context.Request.Host.Host}<hr/>";

            context.Request.EnableBuffering();

            // Leave the body open so the next middleware can read it.
            using (var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                err += $"Payload :- {body}<hr/>";
            }

            string ip = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string ipAddress = context.Connection.RemoteIpAddress.ToString();
            string ipPort = context.Connection.RemotePort.ToString();
            err += $"ip :- {ip} , ipAddress :- {ipAddress} , ipPort :- {ipPort} <hr />";

            var userAgent = context.Request.Headers["User-Agent"].ToString();
            err += $"userAgent :- {userAgent}<hr/>";

            string strHostName = Dns.GetHostName();
            string clientIPAddress = Dns.GetHostAddresses(strHostName).GetValue(1).ToString();
            err += $" Clinet Ip :- {clientIPAddress}<hr/>";

            err += $"Host :- {strHostName}<hr/>";

            string user = userAgent.ToLower();

            string os = "";
            string browser = "";
            //=================OS=======================
            if (userAgent.ToLower().IndexOf("windows") >= 0)
            {
                os = "Windows";
            }
            else if (userAgent.ToLower().IndexOf("mac") >= 0)
            {
                os = "Mac";
            }
            else if (userAgent.ToLower().IndexOf("x11") >= 0)
            {
                os = "Unix";
            }
            else if (userAgent.ToLower().IndexOf("android") >= 0)
            {
                os = "Android";
            }
            else if (userAgent.ToLower().IndexOf("iphone") >= 0)
            {
                os = "IPhone";
            }
            else
            {
                os = "UnKnown, More-Info: " + userAgent;
            }
            //===============Browser===========================
            if (user.Contains("msie"))
            {
                string Substring = userAgent.Substring(userAgent.IndexOf("MSIE")).Split(";")[0];
                browser = Substring.Split(" ")[0].Replace("MSIE", "IE") + "-" + Substring.Split(" ")[1];
            }
            else if (user.Contains("safari") && user.Contains("version"))
            {
                browser = userAgent.Substring(userAgent.IndexOf("Safari")).Split(" ")[0].Split("/")[0] + "-" + userAgent.Substring(userAgent.IndexOf("Version")).Split(" ")[0].Split("/")[1];
            }
            else if (user.Contains("opr") || user.Contains("opera"))
            {
                if (user.Contains("opera"))
                    browser = userAgent.Substring(userAgent.IndexOf("Opera")).Split(" ")[0].Split("/")[0] + "-" + userAgent.Substring(userAgent.IndexOf("Version")).Split(" ")[0].Split("/")[1];
                else if (user.Contains("opr"))
                    browser = userAgent.Substring(userAgent.IndexOf("OPR")).Split(" ")[0].Replace("/", "-").Replace("OPR", "Opera");
            }
            else if (user.Contains("chrome"))
            {
                browser = userAgent.Substring(userAgent.IndexOf("Chrome")).Split(" ")[0].Replace("/", "-");
            }
            else if (user.IndexOf("mozilla/7.0") > -1 || user.IndexOf("netscape6") != -1 || user.IndexOf("mozilla/4.7") != -1 || user.IndexOf("mozilla/4.78") != -1 || user.IndexOf("mozilla/4.08") != -1 || user.IndexOf("mozilla/3") != -1)
            {
                //browser=(userAgent.Substring(userAgent.IndexOf("MSIE")).Split(" ")[0]).Replace("/", "-");
                browser = "Netscape-?";

            }
            else if (user.Contains("firefox"))
            {
                browser = userAgent.Substring(userAgent.IndexOf("Firefox")).Split(" ")[0].Replace("/", "-");
            }
            else if (user.Contains("rv"))
            {
                browser = "IE-" + user.Substring(user.IndexOf("rv") + 3, user.IndexOf(")"));
            }
            else
            {
                browser = "UnKnown, More-Info: " + userAgent;
            }

            err += $"Os :- {os}<hr/>";
            err += $"Browser :- {browser}<hr/>";

            err += $"Stack Trace<hr />{ex.Error.StackTrace.Replace(Environment.NewLine, "<br />")}";

            if (ex.Error.InnerException != null)
                err +=
                    $"Inner Exception<hr />{ex.Error.InnerException?.Message.Replace(Environment.NewLine, "<br />")}";
            // This bit here to check for a form collection!

            if (context.Request.HasFormContentType && context.Request.Form.Any())
            {
                err += "<table border=\"1\"><tr><td colspan=\"2\">Form collection:</td></tr>";
                foreach (var form in context.Request.Form)
                {
                    err += $"<tr><td>{form.Key}</td><td>{form.Value}</td></tr>";
                }
                err += "</table>";
            }


            var email = _configuration.GetValue<string>("ContactUs:Email");
            //var email2 = _configuration.GetValue<string>("ContactUs:FrontEndEmail");
            //var email3 = _configuration.GetValue<string>("ContactUs:karthik");
            List<string> emaillist = new List<string>();
            emaillist.Add(email);
            //emaillist.Add(email2);
            //emaillist.Add(email3);

            await SendMultipleEmail("Api Error Email", emaillist, err, null);
        }

        public async Task SendMultipleEmail(string subject, List<string> email, string content, List<string> attachments = null)
        {
            using (var client = new SmtpClient(_configuration["EmailConfiguration:SmtpServer"], int.Parse(_configuration["EmailConfiguration:Port"]))
            {
                Credentials = new NetworkCredential(_configuration["EmailConfiguration:Username"], _configuration["EmailConfiguration:Password"]),
                EnableSsl = true
            })
            {
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_configuration["EmailConfiguration:From"]);
                    for (int i = 0; i < email.Count; i++)
                    {
                        mailMessage.To.Insert(i, new MailAddress(email[i]));
                    }
                    mailMessage.Subject = subject;
                    mailMessage.Body = content;
                    mailMessage.IsBodyHtml = true;
                    if (attachments != null)
                    {
                        attachments.ForEach(attachment =>
                        {
                            mailMessage.Attachments.Add(new Attachment(attachment));
                        });
                    }
                    //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    await client.SendMailAsync(mailMessage);
                }
            }
        }

        public async Task SendMultipleEmail(string subject, string email, string content, List<string> attachments = null)
        {
            using (var client = new SmtpClient(_configuration["EmailConfiguration:SmtpServer"], int.Parse(_configuration["EmailConfiguration:Port"]))
            {
                Credentials = new NetworkCredential(_configuration["EmailConfiguration:Username"], _configuration["EmailConfiguration:Password"]),
                EnableSsl = true
            })
            {
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_configuration["EmailConfiguration:From"]);

                    mailMessage.To.Insert(0, new MailAddress(email));

                    mailMessage.Subject = subject;
                    mailMessage.Body = content;
                    mailMessage.IsBodyHtml = true;
                    if (attachments != null)
                    {
                        attachments.ForEach(attachment =>
                        {
                            mailMessage.Attachments.Add(new Attachment(attachment));
                        });
                    }
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    await client.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
