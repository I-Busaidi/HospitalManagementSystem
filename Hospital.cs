using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HospitalManagementSystem
{
    public class Hospital
    {
        private static List<Doctor>? doctorsList;
        private static List<(Patient Patient, bool IsInPatient)>? patientsList;
        private static List<Room>? roomsList;
        private static List<Clinic>? clinicsList;
        private static List<InPatient>? inPatients;
        private static List<OutPatient>? outPatients;
        private static List<Nurse>? nursesList;


        static Hospital()
        {
            doctorsList = new List<Doctor>();
            patientsList = new List<(Patient Patient, bool IsInPatient)>();
            roomsList = new List<Room>();
            clinicsList = new List<Clinic>();
            inPatients = new List<InPatient>();
            outPatients = new List<OutPatient>();
            nursesList = new List<Nurse>();
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

        public void AddNurse(Nurse nurse)
        {
            if (!nursesList.Contains(nurse))
            {
                nursesList.Add(nurse);
                Console.WriteLine($"Nurse {nurse.Name} added to the hospital.");
            }
            else
            {
                Console.WriteLine($"Nurse {nurse.Name} is already added to the hospital.");
            }
        }

        public void AssignNurseToClinic(Nurse nurse, Clinic clinic)
        {
            if (!clinic.ClinicNursesList.Contains(nurse))
            {
                clinic.ClinicNursesList.Add(nurse);
                nurse.AssignToClinic(clinic);
                Console.WriteLine($"Nurse {nurse.Name} has been added to clinic: {clinic.ClinicName}");
            }
            else
            {
                Console.WriteLine($"Nurse {nurse.Name} is already assigned to this clinic.");
            }
        }

        public void AddPatient(Patient patient, bool IsInPatient, Doctor? doctor = null, string? admissionDate = null, Clinic? clinic = null)
        {
            if (patientsList.Contains((patient, true)) || patientsList.Contains((patient, false)))
            {
                Console.WriteLine($"Patient {patient.Name} is already added to the hospital.");
            }
            else
            {
                patientsList.Add((patient, IsInPatient));
                if(IsInPatient)
                {
                    InPatient inPatient = new InPatient(patient.Name, patient.Age, patient.gender, patient.PatientID, patient.Ailment, doctor, DateTime.Parse(admissionDate));
                    inPatients.Add(inPatient);
                    Console.WriteLine($"Patient {inPatient.Name} has been added as an in-patient.");
                }
                else
                {
                    OutPatient outPatient = new OutPatient(patient.Name, patient.Age, patient.gender, patient.PatientID, patient.Ailment, clinic);
                    outPatients.Add(outPatient);
                    Console.WriteLine($"Patient {outPatient.Name} has been added as an out-patient.");
                }
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

        public void AddClinic(Clinic clinic)
        {
            clinicsList.Add(clinic);
            Console.WriteLine($"Clinic \"{clinic.ClinicName}\" has been added to the hospital.");
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

        public void DischargePatient(InPatient inPatient)
        {
            inPatient.Discharge();
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

        public List<Nurse> GetNurses()
        {
            return nursesList;
        }

        public List<(Patient, bool)> GetPatients()
        {
            return patientsList;
        }

        public List<InPatient> GetInPatients()
        {
            return inPatients;
        }

        public List<OutPatient> GetOutPatients()
        {
            return outPatients;
        }

        public List<Room> GetRooms()
        {
            return roomsList;
        }

        public List<Clinic> GetClinics()
        {
            return clinicsList;
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
