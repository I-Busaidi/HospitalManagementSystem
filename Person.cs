using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public abstract class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public enum Gender
        {
            Male,
            Female,
            Other
        }
        public Gender gender;

        public Person(string Name, int Age, Gender gender)
        {
            this.Name = Name;
            this.Age = Age;
            this.gender = gender;
        }

        public abstract void DisplayInfo();
    }
}
