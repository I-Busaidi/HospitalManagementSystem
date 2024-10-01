using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HospitalManagementSystem
{
    public class Hospital
    {
        private List<Doctor> doctorsList;
        private List<Patient> patientsList;
        private List<Room> roomsList;

        public Hospital()
        {
            doctorsList = new List<Doctor>();
            patientsList = new List<Patient>();
            roomsList = new List<Room>();
        }

        public void AddDoctor(Doctor doctor)
        {
            if (!doctorsList.Contains(doctor))
            {
                doctorsList.Add(doctor);
                Console.WriteLine($"Doctor {doctor.Name} added to the hospital.");
            }
            else
            {
                Console.WriteLine($"Doctor {doctor.Name} is already in the hospital.");
            }
        }

        public void AddPatient(Patient patient)
        {
            if (!patientsList.Contains(patient))
            {
                patientsList.Add(patient);
                Console.WriteLine($"Patient {patient.Name} added to the hospital.");
            }
            else
            {
                Console.WriteLine($"Patient {patient.Name} is already in the hospital.");
            }
        }

        public void AddRoom(Room room)
        {
            if (!roomsList.Any(r => r.RoomNumber == room.RoomNumber))
            {
                roomsList.Add(room);
                Console.WriteLine($"Room {room.RoomNumber} added to the hospital.");
            }
            else
            {
                Console.WriteLine($"Room {room.RoomNumber} is already in the hospital.");
            }
        }

        public void AssignRoomToPatient(InPatient patient, Room room)
        {
            if (room.IsOccupied)
            {
                Console.WriteLine($"Room {room.RoomNumber} is already occupied.");
            }
            else
            {
                room.OccupyRoom();
                patient.AssignRoom(room);
                Console.WriteLine($"Room {room.RoomNumber} assigned to patient {patient.Name}.");
            }
        }
        public void GetDoctorPatients(Doctor doctor)
        {
            if (doctor.PatientsList.Count == 0)
            {
                Console.WriteLine($"Doctor {doctor.Name} has no patients assigned.");
            }
            else
            {
                Console.WriteLine($"Patients of Doctor {doctor.Name}:");
                foreach (var patient in doctor.PatientsList)
                {
                    Console.WriteLine($"- {patient.Name}, Ailment: {patient.Ailment}");
                }
            }
        }

        public List<Doctor> GetDoctors()
        {
            return doctorsList;
        }

        public List<Patient> GetPatients()
        {
            return patientsList;
        }

        public List<Room> GetRooms()
        {
            return roomsList;
        }

        public void DisplayRoomStatuses()
        {
            Console.WriteLine("Room Statuses in the Hospital:");
            foreach (var room in roomsList)
            {
                string status = room.IsOccupied ? "Occupied" : "Available";
                Console.WriteLine($"Room {room.RoomNumber}: {status}");
            }
        }
    }
}
