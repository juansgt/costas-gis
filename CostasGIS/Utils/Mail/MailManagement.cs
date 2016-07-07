using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace Utils.Mail
{
    public static class MailManagement
    {
        //private static readonly string MAIL_ADRESS = "noreply@shopinshock.com";
        //private static readonly string DISPLAY_NAME = "ShopInShock";

        private static readonly string MAIL_ADRESS = "toponort.es@gmail.com";
        private static readonly string DISPLAY_NAME = "Gestión Catastro";


        //internal static void SendConfirmRegistrationMailToUser(string mail, string password, string confirmRegistrarionUrl, string salt, Usuario.Tipo tipoUsuario)
        //{
        //    MailMessage correo = new MailMessage();
        //    SmtpClient client;

        //    correo.From = new MailAddress(MAIL_ADRESS, DISPLAY_NAME);
        //    //correo.From = new MailAddress("shopinshock@gmail.com", "ShopInShock");
        //    correo.To.Add(mail);
        //    correo.Subject = "Bienvenido";
        //    //Hay que poner el correo en HTML
        //    correo.Body = GenerarHtml("Bienvenido a ShopInShock, gracias por registrarte en nuestro sitio Web<p></p><p></p>" +
        //                  "Para activar tu cuenta por favor haz clic en " + GenerarHyperLinkHtmlTag(confirmRegistrarionUrl + "?" + "mail=" + mail + "&" + "salt=" + salt, confirmRegistrarionUrl + "?" + "mail=" + mail + "&" + "salt=" + salt + "&" + "tipoUsuario=" + StringEnum.GetStringValue(tipoUsuario)) + "<p></p>" +
        //                  " Si el texto anterior no aparece marcado como enlace copie la dirección, pegela en la barra de direcciones de su navegador y presione intro");
        //    correo.IsBodyHtml = true;

        //    client = getSmtpMailClient();
        //    try
        //    {
        //        client.Send(correo);
        //    }
        //    catch (System.Net.Mail.SmtpException ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static void SendConfirmRegistrationMailToShop(string mail)
        {
            MailMessage correo = new MailMessage();
            SmtpClient client;

            correo.From = new MailAddress(MAIL_ADRESS, DISPLAY_NAME);
            //correo.From = new MailAddress("shopinshock@gmail.com", "ShopInShock");
            correo.To.Add(mail);
            correo.Subject = "Bienvenido";
            //Hay que poner el correo en HTML
            correo.Body = GenerarHtml("Bienvenido a ShopInShock, gracias por registrarte en nuestro sitio Web, comprobaremos los datos" +
                                      " que nos has facilitado y activaremos tu cuenta en breve");
            correo.IsBodyHtml = true;

            client = getSmtpMailClient();
            try
            {
                client.Send(correo);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }

        public static void SendShopRegistrationMailToAdministrators(List<string> mails, string registrationManagementUrl)
        {
            MailMessage correo = new MailMessage();
            SmtpClient client;

            correo.From = new MailAddress(MAIL_ADRESS, DISPLAY_NAME);
            //correo.From = new MailAddress("shopinshock@gmail.com", "ShopInShock");

            foreach (string mail in mails)
            {
                correo.To.Add(mail);
            }
            correo.Subject = "Bienvenido";
            //Hay que poner el correo en HTML
            correo.Body = "Nueva solicitud de registro de tienda, siga el enlace:  " + registrationManagementUrl;
            correo.IsBodyHtml = true;

            client = getSmtpMailClient();
            try
            {
                client.Send(correo);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }

        public static void SendMailToUser(string mailAddress, string subject, string message)
        {
            MailMessage correo = new MailMessage();
            SmtpClient client;

            correo.From = new MailAddress(MAIL_ADRESS, DISPLAY_NAME);
            //correo.From = new MailAddress("shopinshock@gmail.com", "ShopInShock");
            correo.To.Add(mailAddress);
            correo.Subject = subject;
            //Hay que poner el correo en HTML
            correo.Body = GenerarHtml(message);
            correo.IsBodyHtml = true;

            client = getSmtpMailClient();
            try
            {
                client.Send(correo);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //throw ex;
            }
        }

        public static void SendMailToUsers(IEnumerable<string> mailAddresses, string subject, string message)
        {
            MailMessage correo = new MailMessage();
            SmtpClient client;

            correo.From = new MailAddress(MAIL_ADRESS, DISPLAY_NAME);
            //correo.From = new MailAddress("shopinshock@gmail.com", "ShopInShock");
            foreach (string mail in mailAddresses)
            {
                correo.To.Add(mail);
            }
            correo.Subject = subject;
            //Hay que poner el correo en HTML
            correo.Body = GenerarHtml(message);
            correo.IsBodyHtml = true;

            client = getSmtpMailClient();
            try
            {
                client.Send(correo);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }

        private static string GenerarHtml(string texto)
        {
            string startHtml = "<html><head><title></title></head><body><style></style>";
            string endHtml = "</body></html>";

            return startHtml + texto + endHtml;
        }

        public static string GenerarHyperLinkHtmlTag(string href, string text)
        {
            return "<a href=\"" + href + "\">" + text + "</a>";
        }

        //private static SmtpClient getSmtpMailClient()
        //{
        //    SmtpClient client = new SmtpClient();

        //    client.Credentials = new System.Net.NetworkCredential("contacto@shopinshock.com", "meristation");
        //    client.Host = "mail.shopinshock.com";

        //    return client;
        //}

        private static SmtpClient getSmtpMailClient()
        {
            SmtpClient smtp = new SmtpClient();

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("toponort.es@gmail.com", "TD2719DT");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            return smtp;

            //SmtpClient client = new SmtpClient();

            //client.Credentials = new System.Net.NetworkCredential("shopinshock@gmail.com", "meristation");
            //client.Port = 587; //25; 
            //client.Host = "smtp.gmail.com";
            //client.EnableSsl = true; //habilitar el envio usando ssl, esto es obligatorio en gmail

            //return client;
        }

        private static string ParseLinks(string text)
        {
            if (text != null)
            {
                MatchCollection matches = Regex.Matches(text.StripHtml(), @"(((file|gopher|ed2k|news|nntp|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&%_\./-~-]*)?");

                foreach (Match matchUrl in matches)
                {

                    if (matchUrl.Success)
                    {

                        for (int i = 0; i < matchUrl.Captures.Count; i++)
                        {
                            //find all url's and add href values

                            Capture urlText = matchUrl.Captures[i];
                            string formatMe = urlText.Value;
                            string formatMeText = urlText.Value;

                            if (!formatMe.Contains("//"))
                                formatMe = "<a href='http://" + formatMe + "' target='_blank' class='userLinks'>" + formatMeText + "</a>";
                            else
                                formatMe = "<a href='" + formatMe + "' target='_blank' class='userLinks'>" + formatMeText + "</a>";
                            text = text.Replace(urlText.Value, formatMe);
                            matchUrl.NextMatch();
                        }
                    }
                }
            }
            return text;
        }

        private static string StripHtml(this string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        private static string StripTags(string str, string allowed_tags)
        {

            /*

            // Coder: Mustafa Turan

            // Date: 05.09.2008

            // Contact:

            // http://mustafaturan.net/

            // http://mustafaturan.wordpress.com/

            // wm [ #at# ] mustafaturan.net

            // Licence: GNU and MIT Licence

            // EXAMPLES

            // > function call1: strip_tags("<a href=\"asdadsadsad.html\">doctor</a> <p>pirasa</p> <img src=\"asd.jpg\" /> <h1>hey you</h1>", "<a>,<p>,<img />")

            // > result: <a href="">doctor</a> <p>pirasa</p> <img src="asd.jpg" /> hey you

            // > function call2: strip_tags("<a href=\"asdadsadsad.html\">doctor</a> <p>pirasa</p> <img src=\"asd.jpg\" /> <h1>hey you</h1>", "")

            // > result: doctor pirasa hey you

            */

            // START

            // pattern for getting all tags

            string pattern_for_all_tags = "</?[^><]+>";

            // pattern for allowed tags

            string allowed_patterns = "";

            if (allowed_tags != "")
            {

                // get allowed tags if any exists

                Regex r = new Regex("[\\/<> ]+");

                allowed_tags = r.Replace(allowed_tags, "");

                string[] allowed_tags_array = allowed_tags.Split(',');

                foreach (string s in allowed_tags_array)
                {

                    if (s == "") continue;

                    // Definin patterns

                    string p_1 = "<" + s + " [^><]*>$";

                    string p_2 = "<" + s + ">";

                    string p_3 = "</" + s + ">";

                    if (allowed_patterns != "")

                        allowed_patterns += "|";

                    allowed_patterns += p_1 + "|" + p_2 + "|" + p_3;

                }

            }

            // Get all html tags included on string

            Regex strip_tags = new Regex(pattern_for_all_tags);

            MatchCollection all_tags_matched = strip_tags.Matches(str);

            if (allowed_patterns != "")

                foreach (Match m in all_tags_matched)
                {

                    Regex r_1 = new Regex(allowed_patterns);

                    Match m_1 = r_1.Match(m.Value);

                    if (!m_1.Success)
                    {

                        // if not allowed replace it

                        str = str.Replace(m.Value, "");

                    }

                }

            else

                // if not allow anyone replace all

                str = strip_tags.Replace(str, "");

            return str;

        }
    }
}
