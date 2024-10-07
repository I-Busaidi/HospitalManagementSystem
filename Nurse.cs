using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HospitalManagementSystem.Doctor;

namespace HospitalManagementSystem
{
    public class Nurse : Person, IPatientCare
    {
        public int NurseID { get; private set; }
        public Clinic AssignedClinic { get; private set; }
        public enum NurseSpecialization
        {
            General, Pediatrics, Surgery
        }
        public NurseSpecialization NurseSpec { get; private set; }
        public List<Patient> AssignedPatients { get; private set; }

        public Doctor assistDoctor { get; private set; }

        public Nurse(int NurseID, string Name, int Age, Gender gender, Clinic AssignedClinic, NurseSpecialization specialization) : base(Name, Age, gender)
        {
            this.NurseID = NurseID;
            this.AssignedClinic = AssignedClinic;
            NurseSpec = specialization;
            AssignedPatients = new List<Patient>() { };
        }

        public void AssistDoctor(Doctor doctor, Patient patient)
        {
            doctor.SetAssistingNurse(this);
            AssignedPatients.Add(patient);
            assistDoctor = doctor;
        }

        public void CheckVitals(Patient patient)
        {
            string RecordedBP;
            float RecordedHR;
            float RecordedTemp;
            
            Console.Clear();
            Console.WriteLine($"Enter the blood pressure for patient {patient.Name}\n");
            while(string.IsNullOrEmpty(RecordedBP = Console.ReadLine()))
            {
                Console.Clear();
                Console.WriteLine($"Enter the blood pressure for patient {patient.Name}\n");
                Console.WriteLine("\nInvalid input, please try again:\n");
            }

            Console.Clear();
            Console.WriteLine($"Enter the heartrate for patient {patient.Name}\n");
            while (!float.TryParse(Console.ReadLine(), out RecordedHR) || RecordedHR < 0)
            {
                Console.Clear();
                Console.WriteLine($"Enter the heartrate for patient {patient.Name}\n");
                Console.WriteLine("\nInvalid input, please try again:\n");
            }

            Console.Clear();
            Console.WriteLine($"Enter the temperature for patient {patient.Name}\n");
            while (!float.TryParse(Console.ReadLine(), out RecordedTemp) || RecordedTemp < 0)
            {
                Console.Clear();
                Console.WriteLine($"Enter the temperature for patient {patient.Name}\n");
                Console.WriteLine("\nInvalid input, please try again:\n");
            }

            patient.RecordVitalsCheck(RecordedBP, RecordedHR, RecordedTemp);
        }

        public void AdministerMedication(Patient patient, string medication)
        {
            patient.AddMedication(medication);
        }

        public override void DisplayInfo()
        {
            StringBuilder sb = new StringBuilder();
            string border = new string('-', 60);
            sb.AppendLine($"Nurse Name: {Name}");
            sb.AppendLine($"Nurse ID: {NurseID}");
            sb.AppendLine($"Specialization: {NurseSpec}");
            sb.AppendLine($"Clinis: {AssignedClinic.ClinicName}");
            sb.AppendLine($"Doctor Assisting: {assistDoctor.Name}");
            sb.AppendLine("Assigned Patients:");
            sb.AppendLine($"{"Name", -20} | {"ID", -10} | {"Ailment", -20}");
            sb.AppendLine(border);

            for (int i = 0; i < AssignedPatients.Count; i++)
            {
                sb.AppendLine($"{AssignedPatients[i].Name,-20} | {AssignedPatients[i].PatientID,-10} | {AssignedPatients[i].Ailment,-20}");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
