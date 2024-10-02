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
        }

        public void AssignToClinic(Clinic clinic, DateTime day, TimeSpan period)
        {
            if (!AssignedClinics.Contains(clinic))
            {
                AssignedClinics.Add(clinic);
            }

            if (!clinic.AvailableAppointments.ContainsKey(this))
            {
                clinic.AvailableAppointments.Add(this, new List<Appointment>());
            }
            Console.WriteLine($"Doctor {Name} is assigned to the {clinic.ClinicName} for {day.ToString("ddd ~ dd MMM, yyyy")} from {9} to {9+period.Hours}\nAvailable time slots: ");
            clinic.AddAvailableAppointment(this, day, period);
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
            if(!PatientsList.Contains(patient))
            {
                PatientsList.Add(patient);
            }
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
