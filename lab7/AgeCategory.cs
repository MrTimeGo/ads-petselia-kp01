using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class AgeCategory
    {
        public string categoryName;
        public AgeCategory(string categoryName)
        {
            this.categoryName = categoryName;
        }
        public override int GetHashCode()
        {
            const int N = 27;
            int hashString = 0;
            for (int i = 0; i < this.categoryName.Length; i++)
            {
                if (char.IsLetter(this.categoryName[i]))
                {
                    hashString += (this.categoryName[i] - 31) * (int)Math.Pow(N, this.categoryName.Length - 1 - i);
                }
                else
                {
                    hashString += this.categoryName[i] * i;
                }
            }
            return hashString;
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                AgeCategory other = (AgeCategory)obj;
                return this.categoryName == other.categoryName;
            }
        }
    }
}
