using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public abstract class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public enum Gender
        {
            Male,
            Female,
            Other
        }
        public Gender gender { get; private set; }

        public Person(string Name, int Age, Gender gender)
        {
            this.Name = Name;
            this.Age = Age;
            this.gender = gender;
        }

        public abstract void DisplayInfo();
    }
}
