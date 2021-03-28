using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaIllico.Mobile.ViewModels
{
    class User
    {
        private static Lazy<User> user = new Lazy<User>(()=> new User());
        private string username;
        private string password;
        private string clientId;
        private string clientSecret;
        private string accessToken;
        private string refreshToken;

        private User()
        {
            clientId = "MOBILE";
            clientSecret = "UNIV";
        }
        
        public static User getInstance()
        {
            return user.Value;
        }

        public string getAccessToken()
        {
            return this.accessToken;
        }public string getUsername()
        {
            return this.username;
        }
        public string getRefreshToken()
        {
            return this.refreshToken;
        }
        public string getPassword()
        {
            return this.password;
        }

        public void setUsername(string new_username)
        {
            this.username = new_username;
        }

        public void setPassword(string new_password)
        {
            this.password = new_password;
        }

        public void setRefreshToken(string refreshToken)
        {
            this.refreshToken = refreshToken;
        }
        public void setAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public void configure(string username, string password, string refreshToken, string accessToken)
        {
            this.username = username;
            this.password = password;
            this.refreshToken = refreshToken;
            this.accessToken = accessToken;
        }
    }
}
