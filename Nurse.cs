using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HospitalManagementSystem.Doctor;

namespace HospitalManagementSystem
{
    public class Nurse : Person
    {
        public int NurseID { get; private set; }
        public Clinic AssignedClinic { get; private set; }
        public enum NurseSpecialization
        {
            General, Pediatrics, Surgery
        }
        public NurseSpecialization NurseSpec { get; private set; }
        public List<Patient> AssignedPatients { get; private set; }

        public Nurse(int NurseID, string Name, int Age, Gender gender, Clinic AssignedClinic, NurseSpecialization specialization) : base(Name, Age, gender)
        {
            this.NurseID = NurseID;
            this.AssignedClinic = AssignedClinic;
            NurseSpec = specialization;
            AssignedPatients = new List<Patient>() { };
        }

        public void AssistDoctor(Doctor doctor, Patient patient)
        {

        }

        public void CheckVitals(Patient patient)
        {

        }

        public void AdministerMedication(Patient patient, string medication)
        {

        }

        public override void DisplayInfo()
        {
            
        }
    }
}
