using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class UserInformation
    {
        public string emailAddress;
        public DateTime birthDate;
        public UserInformation(string emailAddress, DateTime birthDate)
        {
            this.emailAddress = emailAddress;
            this.birthDate = birthDate;
        }
        public override string ToString()
        {
            return $"Email: {this.emailAddress}, birth date: {this.birthDate.Day}/{this.birthDate.Month}/{this.birthDate.Year}";
        }
    }
}
