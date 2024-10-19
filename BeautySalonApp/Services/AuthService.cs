namespace BeautySalonApp.Services
{
    public class AuthService
    {
        public AuthService() { }

        public bool Login(User user)
        {
            // user.Login
            return true;
        }
    }

    public abstract class User()
    {
        public abstract bool Login();
    }
}
