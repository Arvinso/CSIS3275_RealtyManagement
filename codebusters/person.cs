using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codebusters
{
    class person
    {
        private int personID;
        private string fname;
        private string lname;
           
        public int personid
        {
            set
            {
                personID = value;
            }
            get
            {
                return personID;
            }
         }           

        public string FirstName
        {
            set
            {
                fname = value;
            }
            get
            {
                return fname;
            }
        }

        public string LastName
        {
            set
            {
                lname = value;
            }
            get
            {
                return lname;
            }
        }
        

        public override string ToString()
        {
            string s = fname + " " + lname;
            return s;
        }
               
    }
}
