using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace Authenticator
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class AuthServiceImpl : AuthServiceInterface
    {

        private static string usersFilePath = Path.Combine(System.AppContext.BaseDirectory, "Users.txt");

        private static string tokensFilePath = Path.Combine(System.AppContext.BaseDirectory, "Tokens.txt");

        public AuthServiceImpl()
        {
  
        }

        public void Login(string name, string password, out string result)
        {
            result = null;
            List<User> registedUsers = AllUsers();
            var users = registedUsers.FindAll(p => p.userName == name);
            if (users != null && users.Count > 0)
            {
                //checck password
                if (users[0].password == password)
                {
                    result = CreateNameToekn();
                }
            }
        }

        

        public void Register(string name, string password, out string result)
        {
            List<User> registedUsers = AllUsers();
            registedUsers.Add(new User(name, password));
            var jsonString = JsonConvert.SerializeObject(registedUsers);
            File.WriteAllText(usersFilePath, jsonString);
            result = "successfully registere";
        }


        public void Validate(string token, out string result)
        {
            result = "not validated";
            List<string> activedToekns = AllTokens();
            if (activedToekns.Contains(token))
            {
                result = "validated";
            }
        }
        private string CreateNameToekn()
        {
            // create token
            string token = Guid.NewGuid().ToString();
            List<string> activedToekns = AllTokens();
            activedToekns.Add(token);
            // save to file
            var jsonString = JsonConvert.SerializeObject(activedToekns);
            File.WriteAllText(tokensFilePath, jsonString);
            return token;
        }

        private List<User> AllUsers()
        {
            // Open the file to read from.
            string readText = File.ReadAllText(usersFilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(readText);
            if (users == null)
                users = new List<User>();
            return users;
        }

        private List<string> AllTokens()
        {
            // Open the file to read from.
            string readText = File.ReadAllText(tokensFilePath);
            List<string> tokens = JsonConvert.DeserializeObject<List<string>>(readText);
            if (tokens == null)
                tokens = new List<string>();
            return tokens;
        }
    }
}