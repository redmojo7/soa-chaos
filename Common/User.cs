namespace Common
{
    public class User
    {
        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public string userName { get; set; }
        public string password { get; set; }
    }
}