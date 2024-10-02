using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public abstract class Patient : Person
    {
        public int PatientID { get; private set; }
        public string Ailment { get; private set; }

        public Patient(int PatientID, string Name, int Age, Gender gender, string Ailment) : base(Name, Age, gender)
        {
            this.PatientID = PatientID;
            this.Ailment = Ailment;
        }

        public override abstract void DisplayInfo();
    }
}
