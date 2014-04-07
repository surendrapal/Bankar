using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.UI;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Xml.Xsl;
using System.Net.Mail;
namespace BM.Common
{
    /// <summary>
    /// Application Settings wrapper, Default values reflect Live settings
    /// </summary>
    public sealed class Config
    {
        #region Public Methods
 
        /// <summary>
        /// Base url of the site
        /// </summary>
        public static string SiteRoot
        {
            get
            {
                return GetString("SiteRoot", "");
            }
        }
        public static string CompanyName
        {
            get
            {
                return GetString("CompanyName", "");
            }
        }


        public static string Capitalize(string value)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }
        public static void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);

            g.InterpolationMode = InterpolationMode.Low;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
          //  Bitmap bmpCrop = b.Clone(new Rectangle(new Point(0, 0), size), b.PixelFormat);
            return (Image)b;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", " ", RegexOptions.Compiled);
        }

       
        public static bool SendMail(string From, string FromName, string To, string ToName, string Subject, string Body, string ReplyTo, string ReplyToName, string CC, string CCName, string BCC, string BCCName)
        {
            MailMessage Obj_MailMesage = new MailMessage();
            Obj_MailMesage.From = new MailAddress(From, FromName);
            Obj_MailMesage.To.Add(new MailAddress(To, ToName));
            Obj_MailMesage.ReplyToList.Add(new MailAddress(ReplyTo, ReplyToName));
            Obj_MailMesage.Subject = Subject;
            Obj_MailMesage.Body = Body;
            return SendMail(Obj_MailMesage);
        }

        public static bool SendMail(string From, string FromName, string To, string ToName, string Subject, string Body, string ReplyTo, string ReplyToName)
        {
            MailMessage Obj_MailMesage = new MailMessage();
            Obj_MailMesage.From = new MailAddress(From, FromName);
            Obj_MailMesage.To.Add(new MailAddress(To, ToName));
            Obj_MailMesage.ReplyToList.Add(new MailAddress(ReplyTo, ReplyToName));
            Obj_MailMesage.Subject = Subject;
            Obj_MailMesage.Body = Body;
            return SendMail(Obj_MailMesage);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns value from config string type.
        /// </summary>
        /// <param name="name">key in config</param>
        /// <returns>value</returns>
        private static string GetString(string name)
        {
            string optionValue = ConfigurationManager.AppSettings[name];
            if (optionValue == null)
            {
                throw new InvalidOperationException(String.Format("'{0}' must be set in appSettings in the configuration file.", name));
            }
            return optionValue;
        }

        /// <summary>
        /// if config does not contain key in config then use default passed
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>value of key</returns>
        private static string GetString(string name, string defaultValue)
        {
            string optionValue = ConfigurationManager.AppSettings[name];
            if (optionValue == null || optionValue.Length == 0)
            {
                return defaultValue;
            }
            return optionValue;
        }

        public static bool SendMail(MailMessage Obj_MailMesage)
        {
            try
            {
                Obj_MailMesage.Priority = MailPriority.Normal;
                Obj_MailMesage.IsBodyHtml = true;
                SmtpClient ObjSmtp = new SmtpClient();
                System.Net.NetworkCredential Net_Crd = new System.Net.NetworkCredential(GetString("noReplyMail"), GetString("noReplyPassword"));
                ObjSmtp.EnableSsl = false;
                ObjSmtp.UseDefaultCredentials = false;
                ObjSmtp.Port = int.Parse(GetString("Port"));
                ObjSmtp.Host = GetString("SmtpServer");
                ObjSmtp.Credentials = Net_Crd;
                ObjSmtp.Send(Obj_MailMesage);
            }
            catch { throw; }
            return true;
        }
        #endregion
    }

    public class DateTimeComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            DateTime dt1 = DateTime.Parse(x.ToString());
            DateTime dt2 = DateTime.Parse(y.ToString());
            return dt2.CompareTo(dt1);
        }
    }
    
}
