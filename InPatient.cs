using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HospitalManagementSystem.Person;

namespace HospitalManagementSystem
{
    public class InPatient : Patient
    {
        public Room? Room { get; private set; }
        public Doctor? AssignedDoctor { get; private set; }
        public Nurse? AssignedNurse { get; private set; }
        public DateTime AdmissionDate { get; private set; }
        public InPatient(string Name, int Age, Gender gender, int PatientID, string Ailment, Doctor doctor, DateTime admissionDate) 
            : base(PatientID, Name, Age, gender, Ailment)
        {
            AssignedDoctor = doctor;
            AdmissionDate = admissionDate;
            Room = null;
        }

        public void AssignRoom(Room room)
        {
            Room = room;
            Console.WriteLine($"Room {room.RoomNumber} has been assigned to patient {Name} on {AdmissionDate.ToString("ddd ~ dd MMM, yyyy")}");
            Room.OccupyRoom();
        }

        public void AssignDoctor(Doctor doctor)
        {
            AssignedDoctor = doctor;
        }

        public void AssignNurse(Nurse nurse)
        {
            AssignedNurse = nurse;
        }

        public void Discharge()
        {
            if (Room != null)
            {
                Room.VacateRoom();
                AssignedNurse = null;
                AssignedDoctor = null;
                Console.WriteLine($"Patient {Name} admitted on {AdmissionDate.ToString("ddd ~ dd MMM, yyyy")} has been discharged on {DateTime.Now.ToString("ddd ~ dd MMM, yyyy")}");
                Room = null;
            }
            else
            {
                Console.WriteLine("Room is already vacated.");
            }
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Patient ID: {PatientID}, Name: {Name}, Age: {Age}\n" +
                $"Gender: {gender}, Ailment: {Ailment}\n" +
                $"Admission Date: {AdmissionDate.ToString("ddd MMM, yyyy a\\t hh:mm:ss")}");

            if (Room != null)
            {
                Console.WriteLine($"Room Number: {Room.RoomNumber}");
            }

            if (AssignedDoctor != null)
            {
                Console.WriteLine($"Assigned Doctor: {AssignedDoctor.Name}");
            }
        }
    }
}
