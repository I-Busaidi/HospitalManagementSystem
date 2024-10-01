using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Doctor : Person
    {
        public int DoctorID;
        public enum DocSpecialization
        {
            Cardiology, Neurology, Dermatology
        }

        public DocSpecialization specialization;

        public List<Clinic> AssignedClinics = new List<Clinic>();
        public List<Patient> PatientsList = new List<Patient>();

        public Doctor(int DoctorID, string Name, int Age, Gender gender, DocSpecialization specialization) : base(Name, Age, gender)
        {
            this.DoctorID = DoctorID;
            this.specialization = specialization;
            PatientsList = new List<Patient>();
        }

        public void AssignToClinic(Clinic clinic, DateTime day, TimeSpan period)
        {
            
            AssignedClinics.Add(clinic);
            if (clinic.AvailableAppointments.ContainsKey(this))
            {
                clinic.AddAvailableAppointment(this, day, period);
            }
            else
            {
                clinic.AvailableAppointments.Add(this, new List<Appointment>{});
                clinic.AddAvailableAppointment(this, day, period);
            }
        }

        public void DisplayAssignedClinics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} Assigned to:");
            sb.AppendLine($"{"Clinic ID", -10} | {"Clinic Name", -20} | {"Clinic Type", -20}");
            for (int i = 0; i < AssignedClinics.Count; i++)
            {
                sb.AppendLine($"{AssignedClinics[i].ClinicID, -10} | {AssignedClinics[i].ClinicName, -20} | {AssignedClinics[i].specialization, -20}");
            }
            Console.WriteLine( sb.ToString() );
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
            Console.WriteLine($"DoctorID: {DoctorID}, Specialization: {specialization}");
        }
    }
}
