using System.Net;
using System.Net.Mail;

namespace Library.Company.PL.Helpers
{
    public  static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
			try
			{
				var Client = new SmtpClient("smtp.gmail.com", 587);
				Client.EnableSsl = true;

				Client.Credentials = new NetworkCredential("www.wissamtarik2021@gmail.com", "zvkdymobozxsxliy");

				Client.Send("www.wissamtarik2021@gmail.com", email.To,email.Subject, email.Body);

				return true;
			}
			catch (Exception)
			{

				return false;
			}
        }
    }
}
