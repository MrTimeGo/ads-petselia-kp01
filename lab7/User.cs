using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class User
    {
        public string username;
        public string password;
        
        public User(string username, string password)
        {
            this.username = username;
            this.password = GetPasswordHash(password);
        }
        private string GetPasswordHash(string oldPassword)
        {
            string newPassword = "";
            for (int i = 0; i < oldPassword.Length; i++)
            {
                newPassword += (char)((oldPassword[i] + i + 128) % 128);
            }
            return newPassword;
        }
        public override int GetHashCode()
        {
            const int prime = 37;
            const int seed = 173;
            long hash = seed;

            hash = prime * hash + GetHashCodeString(this.username);
            hash = prime * hash + GetHashCodeString(this.password);

            return (int)(hash % int.MaxValue);
        }
        private long GetHashCodeString(string text)
        {
            const int N = 27; 
            long hashString = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    hashString += (text[i] - 31) * (long)Math.Pow(N, text.Length - 1 - i);
                }
                else
                {
                    hashString += text[i] * i;
                }
            }
            return hashString;
        }
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                User other = (User)obj;
                return (this.username == other.username) && (this.password == other.password);
            }
        }
        public override string ToString()
        {
            return $"Username: {this.username}, password: {this.password}";
        }
    }
}
