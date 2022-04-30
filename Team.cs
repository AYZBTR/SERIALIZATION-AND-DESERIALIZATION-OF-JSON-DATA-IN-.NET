using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERIALIZATION_AND_DESERIALIZATION
{
    public class Team
    {
        public Team(string name, DateTime dateOfBirth, List<Continfo> contactInformation)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            ContactInformation = contactInformation;
        }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<Continfo> ContactInformation { get; set; }
        public string Email { get; internal set; }
        public string Mobile { get; internal set; }
    }

    public class Continfo
    {
        public Continfo(string email, string mobile)
        {
            Email = email;
            Mobile = mobile;
        }

        public string Email { get; set; }
        public string Mobile { get; set; }
    }


}




   

