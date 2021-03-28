using System;

namespace PizzaIllico.Mobile.ViewModels
{
    class User
    {
        private static Lazy<User> user = new(()=> new User());
        private string accessToken;
        private string refreshToken;

        private User()
        {
        }
        
        public static User getInstance()
        {
            return user.Value;
        }

        public string getAccessToken()
        {
            return this.accessToken;
        }
      
        public string getRefreshToken()
        {
            return this.refreshToken;
        }
        public void setRefreshToken(string refreshToken)
        {
            this.refreshToken = refreshToken;
        }
        public void setAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public void configure(string refreshToken, string accessToken)
        {
            this.refreshToken = refreshToken;
            this.accessToken = accessToken;
        }
    }
}
