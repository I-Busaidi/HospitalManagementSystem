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

        public Patient(int PatientID, string Name, int Age, Gender gender, string Ailment, Doctor doctor) : base(Name, Age, gender)
        {
            this.PatientID = PatientID;
            this.Ailment = Ailment;
            AssignedDoctor = doctor;
            doctor.AddPatient(this);
        }

        public void AssignRoom(Room room)
        {
            Room = room;
        }

        public void Discharge()
        {
            Room.VacateRoom();
            Room = null;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Gender: {gender}");
            Console.WriteLine($"PatientID: {PatientID}, Ailment: {Ailment}, Doctor: {AssignedDoctor.Name}");
        }

        public int GetID()
        {
            return PatientID;
        }
        public string GetAilment()
        {
            return Ailment;
        }
    }
}
