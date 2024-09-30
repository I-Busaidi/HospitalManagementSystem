using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Doctor : Person
    {
        private int DoctorID;
        private string Specialization;
        private List<Patient> PatientsList;

        public Doctor(int DoctorID, string Name, int Age, Gender gender, string Specialization) : base(Name, Age, gender)
        {
            this.DoctorID = DoctorID;
            this.Specialization = Specialization;
            PatientsList = new List<Patient>();
        }

        public void AddPatient(Patient patient)
        {
            PatientsList.Add(patient);
        }

        public void RemovePatient(Patient patient)
        {
            PatientsList?.Remove(patient);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Gender: {gender}");
            Console.WriteLine($"DoctorID: {DoctorID}, Specialization: {Specialization}");
        }

        public List<Patient> GetPatients()
        {
            return PatientsList;
        }

        public int GetID()
        {
            return DoctorID;
        }

        public string GetSpec()
        {
            return Specialization;
        }
    }
}
