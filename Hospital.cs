using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Hospital
    {
        private List<Doctor> DoctorsList;
        private List<Patient> PatientsList;
        private List<Room> RoomsList;

        public Hospital() 
        { 
            DoctorsList = new List<Doctor>();
            PatientsList = new List<Patient>();
            RoomsList = new List<Room>();
        }

        public void AddDoctor(Doctor doctor)
        {
            DoctorsList.Add(doctor);
        }

        public void AddPatient(Patient patient)
        {
            PatientsList.Add(patient);
        }

        public void AssignRoomToPatient(Patient patient, Room room)
        {

        }

        public void GetDoctorPatients(Doctor doctor)
        {

        }
    }
}
