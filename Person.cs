﻿using System;
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
        public enum Gender;

        public Person(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }

        public abstract void DisplayInfo();
    }
}
