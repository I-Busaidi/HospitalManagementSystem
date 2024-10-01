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
        public Room? Room;
        public Doctor AssignedDoctor;
        public DateTime AdmissionDate;
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
            Room.OccupyRoom();
        }

        public void Discharge()
        {
            if (Room != null)
            {
                Room.VacateRoom();
                Room = null;
            }
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Patient ID: {PatientID}, Name: {Name}, Age: {Age}\n" +
                $"Gender: {gender}, Ailment: {Ailment}, Assigned Doctor: {AssignedDoctor.Name}\n" +
                $"Admission Date: {AdmissionDate.ToString("ddd MMM, yyyy a\\t hh:mm:ss")}");

            if (Room != null)
            {
                Console.WriteLine($"Room Number: {Room.RoomNumber}");
            }
        }
    }
}
