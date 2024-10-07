using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Patient : Person
    {
        public int PatientID { get; private set; }
        public string Ailment { get; private set; }
        public string RecordedPB { get; private set; }
        public float RecordedHeartRate { get; private set; }
        public float RecordedTemp { get; private set; }
        public List<string> AdministeredMedications { get; private set; }
        public Patient(int PatientID, string Name, int Age, Gender gender, string Ailment) : base(Name, Age, gender)
        {
            this.PatientID = PatientID;
            this.Ailment = Ailment;
            AdministeredMedications = new List<string>() { };
        }

        public void AddMedication(string medication)
        {
            AdministeredMedications.Add(medication);
        }

        public void RecordVitalsCheck(string BP, float HR, float Temp)
        {
            RecordedPB = BP;
            RecordedHeartRate = HR;
            RecordedTemp = Temp;
        }

        public override void DisplayInfo()
        {

        }
    }
}
