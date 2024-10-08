using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Doctor : Person, IInPatientCare, IOutPatientCare
    {
        public int DoctorID { get; private set; }
        public enum DocSpecialization
        {
            Cardiology, Neurology, Dermatology
        }

        public DocSpecialization specialization;

        public List<Clinic> AssignedClinics = new List<Clinic>();
        public List<Patient> PatientsList = new List<Patient>();
        public Nurse AssistingNurse { get; private set; }

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
            string border = new string('-', 50);
            sb.AppendLine($"{Name} Assigned to:");
            sb.AppendLine();
            sb.AppendLine($"{"Clinic ID", -10} | {"Clinic Name", -20} | {"Clinic Type", -20}");
            sb.AppendLine(border);
            for (int i = 0; i < AssignedClinics.Count; i++)
            {
                sb.AppendLine($"{AssignedClinics[i].ClinicID, -10} | {AssignedClinics[i].ClinicName, -20} | {AssignedClinics[i].specialization, -20}");
            }
            sb.AppendLine(border);
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

        public void SetAssistingNurse(Nurse nurse)
        {
            AssistingNurse = nurse;
        }

        public void AssignRoomToPatient(InPatient inPatient, Room room)
        {
            if (room.IsOccupied)
            {
                Console.WriteLine($"Room {room.RoomNumber} is already occupied.");
            }
            else
            {
                room.OccupyRoom();
                inPatient.AssignRoom(room);
                Console.WriteLine($"Room {room.RoomNumber} assigned to patient {inPatient.Name}.");
            }
        }

        public void DischargePatient(InPatient inPatient)
        {
            if (inPatient.Room != null)
            {
                inPatient.Room.VacateRoom();
                Console.WriteLine($"Patient {inPatient.Name} admitted on {inPatient.AdmissionDate.ToString("ddd ~ dd MMM, yyyy")} has been discharged on {DateTime.Now.ToString("ddd ~ dd MMM, yyyy")}");
                inPatient.VacateRoom();
            }
            else
            {
                Console.WriteLine("Room is already vacated.");
            }
        }

        public void ScheduleFollowUpAppointment(OutPatient outPatient, Clinic clinic, DateTime appointmentDay, TimeSpan appointmentTime)
        {
            outPatient.BookAppointment(clinic, appointmentDay, appointmentTime);
        }

        public void CheckVitals(Patient patient)
        {
            string RecordedBP;
            float RecordedHR;
            float RecordedTemp;

            Console.Clear();
            Console.WriteLine($"Enter the blood pressure for patient {patient.Name}\n");
            while (string.IsNullOrEmpty(RecordedBP = Console.ReadLine()))
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

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Gender: {gender}");
            Console.WriteLine($"DoctorID: {DoctorID}, Specialization: {specialization}");
        }
    }
}
