using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Patient : Person
    {
        private int PatientID;
        private string Ailment;
        private Doctor AssignedDoctor;
        private Room Room;

        public Patient(string Name, int Age, int PatientID, string Ailment) : base(Name, Age)
        {
            this.PatientID = PatientID;
            this.Ailment = Ailment;
        }

        public void AssignRoom(Room room)
        {

        }

        public void Discharge()
        {

        }

        public override void DisplayInfo()
        {
            
        }
    }
}
