
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;


namespace webapitestmail.Controllers;

[ApiController]
[Route("[controller]")]
public class mailtestController : ControllerBase
{

    [HttpPost("[action]")]

    public IActionResult testmail(string emailfrom, string emailto, string smtphost, int smtpports, string smtpuser, string smtppass, string subject, string msg, string protocal, bool configssl)
    {

        // SMTP settings for ms exchange 2021
        var smtpHost = smtphost;
        var smtpPort = smtpports;
        var smtpUsername = smtpuser;
        var smtpPassword = smtppass;

        if (protocal == "SystemDefault")
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
        }
        if (protocal == "Tls")
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        }
        if (protocal == "Tls11")
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
        }
        if (protocal == "Tls12")
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
        if (protocal == "Tls13")
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
        }


        // Sender and recipient email addresses
        var fromEmail = emailfrom;
        var toEmail = emailto;

        // Create a new SMTP client
        var smtpClient = new SmtpClient(smtpHost, smtpPort);
        smtpClient.EnableSsl = configssl;
        smtpClient.UseDefaultCredentials = false;

        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

        try
        {
            // Create a new email message
            var message = new MailMessage(fromEmail, toEmail);
            message.Subject = subject;
            message.Body = MailConfirm.mailbodyTest(msg);
            message.IsBodyHtml = true;

            // Send the email
            smtpClient.Send(message);
            Console.WriteLine("Email sent successfully.");



            // Return success response
            return new OkObjectResult("Email sent successfully.");
        }
        catch (SmtpException ex)
        {
            // Handle SMTP-specific errors
            var errorMessage = $"SMTP Error: {ex.StatusCode}";
            if (ex.InnerException != null)
            {
                errorMessage += " | Inner Exception: " + ex.InnerException.Message;
            }

            // Return a 400 Bad Request with error message
            return new BadRequestObjectResult(errorMessage);
        }
        catch (Exception ex)
        {
            // Handle general exceptions
            var errorMessage = "General Error: " + ex.Message;
            if (ex.InnerException != null)
            {
                errorMessage += " | Inner Exception: " + ex.InnerException.Message;
            }

            // Return a 500 Internal Server Error with error message
            return new ObjectResult(errorMessage) { StatusCode = 500 };
        }
    }


}

public class MailConfirm
{








    public static string mailbodyTest(string msg)
    {
        var _str = "";
        _str = @"<!DOCTYPE html>  <html> <head> <style>";
        _str += " body {  font-family: Arial, sans-serif;   background-color: #F1F9F4;  }";
        _str += " .container {";
        _str += "   max-width: 600px;";
        _str += "   margin: 0 auto;";
        _str += "   padding: 20px;";
        _str += "   background-color: #FFFFFF;";
        _str += "  border-radius: 5px;";
        _str += "   box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);";
        _str += "  }";
        _str += " h1 {";
        _str += "     color: #2E8B57;";
        _str += "    margin-bottom: 20px;";
        _str += " }";
        _str += " p {";
        _str += " color: #333333;";
        _str += "  line-height: 1.6;";
        _str += "}";
        _str += " .button {";
        _str += "    display: inline-block;";
        _str += "    padding: 10px 20px;";
        _str += "  background-color: #2E8B57;";
        _str += "  color: #FFFFFF;";
        _str += "  text-decoration: none;";
        _str += "  border-radius: 5px;";
        _str += " }";
        _str += ".button:hover {";
        _str += "    background-color: #228B22;";
        _str += " }";
        _str += "</style>";
        _str += "</head>";
        _str += "<body>";
        _str += " <div class=\"container\">";
        _str += "  <h1>Welcome  System!</h1>";
        _str += "  <p>" + msg + "</p>";


        _str += "</div>";
        _str += "</body>";
        _str += "</html>";

        return _str;
    }

}



