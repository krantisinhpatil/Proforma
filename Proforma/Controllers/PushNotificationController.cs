using Proforma.DAL;
using Proforma.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;

namespace Proforma.Controllers
{
    public class PushNotificationController : ApiController
    {
        SourcingGuideDevEntities _db = new SourcingGuideDevEntities();

        [HttpPost]
        public HttpResponseMessage PushNotifications()
        {
           
            PushResponse response = new PushResponse();
            try
            {              
                string Certificate = "KeyLive.p12";               
                var users = _db.ProformaUsers.Where(a => a.DeviceToken != null && a.DeviceType != null).ToList();
                string Message = "Hello";
                //string DeviceToken = "a9ec044e9ce03a9b2e42b22c19034b6e93e17bb3a897bddf5c332c9567b29655";
                //string DeviceType = "iOS";
               
                if (users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        NotifyIPhones(Message, user.DeviceToken, Certificate, user.DeviceType);
                        //var userNotifications = _db.UserNotifications.Where(k => k.UserId == user.UserID && k.Status==Status.UnRead.ToString()).ToList();
                        //if(userNotifications.Count()>0)
                        //{
                        //    foreach (var notify in userNotifications)
                        //    {
                        //        var notification = _db.Notifications.FirstOrDefault(t => t.NotificationId ==notify.NotificationId);
                        //        NotifyIPhones(notification.Description, user.DeviceToken, Certificate, user.DeviceType);
                        //    }
                        //}
                        //else
                        //{
                        //    response.Flag = "false";
                        //    response.MESSAGE = "No records found";
                        //}

                    }
                    response.Flag = "true";
                    response.MESSAGE = "Notification Sent Successfully";
                   
                }
                else
                {
                    response.Flag = "false";
                    response.MESSAGE = "No records found";
                    
                }
            }
            catch (Exception ex)
            {
                response.Flag = "false";
                response.MESSAGE = ex.Message;
                
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        public async void NotifyIPhones(string message, string DeviceToken, string Certificate, string DeviceType)
        {
            if (DeviceType == "iOS")
            {

                int port = 2195;
                
               String hostname = "gateway.push.apple.com";  // For Production
              //String hostname = "gateway.sandbox.push.apple.com";  //For Sandbox
                String certificatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Certificates/KeyLive.p12");
                X509Certificate2 clientCertificate = new X509Certificate2(System.IO.File.ReadAllBytes(certificatePath), "");
                X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);
                TcpClient client = new TcpClient(hostname, port);
                SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                String deviceId = DeviceToken;
               // try
               // {
               sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Tls, false);           
                MemoryStream memoryStream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(memoryStream);
                    writer.Write((byte)0);
                    writer.Write((byte)0);
                    writer.Write((byte)32);

                    writer.Write(HexStringToByteArray(deviceId.ToUpper()));
                    String payload = "{\"aps\":{\"alert\":\"" + message + "\",\"badge\":1,\"sound\":\"default\"}}";
                    writer.Write((byte)0);
                    writer.Write((byte)payload.Length);
                    byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
                    writer.Write(b1);
                    writer.Flush();
                    byte[] array = memoryStream.ToArray();
                    sslStream.Write(array);
                    sslStream.Flush();
                    //    CheckFeedbackService();
                    client.Close();
                //}
               // catch (System.Security.Authentication.AuthenticationException ex)
               // {
                //    client.Close();
                //}
               // catch (Exception e)
               // {
                   // client.Close();
               // }
            }
            else
            {
                var applicationID = "AIzaSyDlRHxQnt9LyT3w6kKlHmwUZaNWMOhLBIU";
                var regId = DeviceToken;
                var SENDER_ID = "880851471489";
                var value = message;
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                // string postData = "{ 'registration_id': [ '" + regId + "' ], 'data': {'message': '" + txtMsg.Text + "'}}";
                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.Message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId + "";
                Console.WriteLine(postData);
                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                String sResponseFromServer = tReader.ReadToEnd();
                if (sResponseFromServer == "Error=NotRegistered")
                {
                    PushNotification _obj = new PushNotification();
                   
                }
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
        }

        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
