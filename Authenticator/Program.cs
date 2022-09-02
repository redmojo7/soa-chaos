using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            var a = GenerateToken();
            Console.WriteLine(Guid.NewGuid());
            Console.WriteLine(a);
            var result = ValidateToken("a1c3ef46-bd7e-4ff1-b55e-8a82d71e3079", a);
            Console.WriteLine(result);

            Console.WriteLine("hey so like welcome to Auth server");
            // This is the actual host service system
            ServiceHost host;
            // This represents a tcp/ip binding in the Windows network stack
            NetTcpBinding tcp = new NetTcpBinding();
            // Bind server to the implementation of DataServer
            host = new ServiceHost(typeof(AuthServiceImpl));
            /* Present the publicly accessible interface to the client. 0.0.0.0 tells .net to
             accept on any interface. :8100 means this will use port 8100. DataService is a name for the
             actual service, this can be any string.*/

            host.AddServiceEndpoint(typeof(AuthServiceInterface), tcp, "net.tcp://0.0.0.0:8100/AuthService");
            // And open the host for business!
            host.Open();
            Console.WriteLine("System Online");
            Console.ReadLine();
            // Don't forget to close the host after you're done!
            host.Close();


            Console.ReadLine();

        }

 
        public static string GenerateToken()
        {
            byte[] _time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] _userName = Guid.Parse("a1c3ef46-bd7e-4ff1-b55e-8a82d71e3079").ToByteArray();
            //byte[] _password = Guid.Parse("password").ToByteArray();
            //byte[] data = new byte[_time.Length + _userName.Length + _password.Length];
            byte[] data = new byte[_time.Length + _userName.Length];

            System.Buffer.BlockCopy(_time, 0, data, 0, _time.Length);
            System.Buffer.BlockCopy(_userName, 0, data, _time.Length, _userName.Length);
            //System.Buffer.BlockCopy(_password, 0, data, _time.Length + _userName.Length, _password.Length);

            return Convert.ToBase64String(data.ToArray());
        }

        public static string ValidateToken(string userName, string token)
        {
            var result = "";
            byte[] data = Convert.FromBase64String(token);
            byte[] _time = data.Take(8).ToArray();
            byte[] _userName = data.Skip(8).ToArray();

            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(_time, 0));
            if (when < DateTime.UtcNow.AddHours(-24))
            {
                //result.Errors.Add(TokenValidationStatus.Expired);
            }

            Guid gUserName = new Guid(_userName);
            if (gUserName.ToString() != userName)
            {
                Console.WriteLine("WrongGuid");
            }
            return result;
        }


    }
}
