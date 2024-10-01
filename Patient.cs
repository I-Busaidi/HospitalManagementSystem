using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public abstract class Patient : Person
    {
        public int PatientID;
        public string Ailment;

        public Patient(int PatientID, string Name, int Age, Gender gender, string Ailment) : base(Name, Age, gender)
        {
            this.PatientID = PatientID;
            this.Ailment = Ailment;
        }

        public override abstract void DisplayInfo();
    }
}
