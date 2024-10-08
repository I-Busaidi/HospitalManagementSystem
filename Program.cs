using System.Text;
using System.Text.RegularExpressions;
using static HospitalManagementSystem.Clinic;
using static HospitalManagementSystem.Doctor;
using static HospitalManagementSystem.Nurse;
using static HospitalManagementSystem.Person;
using static HospitalManagementSystem.Room;

namespace HospitalManagementSystem
{
    internal class Program
    {
        public static Hospital hospital = new Hospital();

        static string NameRegexFormat = @"^[A-Za-z]+(?: [A-Za-z]+)*$";

        static void Main(string[] args)
        {
            // Create doctors
            Doctor doctor1 = new Doctor(1, "Dr. John Smith", 45, Gender.Male, DocSpecialization.Cardiology);
            Doctor doctor2 = new Doctor(2, "Dr. Alice Brown", 38, Gender.Female, DocSpecialization.Neurology);


            // Create clinics
            Clinic cardiologyClinic = new Clinic(1, "Cardiology Clinic", Specialization.Cardiology);
            Clinic neurologyClinic = new Clinic(2, "Neurology Clinic", Specialization.Neurology);


            // Assign doctors to clinics and generate appointment slots (9 AM - 12 PM)
            Console.WriteLine("Doctor Assignments:\n");
            doctor1.AssignToClinic(cardiologyClinic, new DateTime(2024, 10, 5), TimeSpan.FromHours(3)); // Expected: Appointments generated for 9 AM, 10 AM, 11 AM
            Console.WriteLine("\n");                                                                    
            doctor2.AssignToClinic(neurologyClinic, new DateTime(2024, 10, 6), TimeSpan.FromHours(3));  // Expected: Appointments generated for 9 AM, 10 AM, 11 AM

            Console.WriteLine("\n-----------------------------------------------------------------");
            // Create rooms for clinics
            Room room1 = new Room(101, RoomType.IPR);  // Room for in-patients
            Room room2 = new Room(102, RoomType.OPR);  // Room for out-patients

            Console.WriteLine("Room Assignments:\n");
            cardiologyClinic.AddRoom(room1); // Expected: Room 101 added to Cardiology Clinic
            neurologyClinic.AddRoom(room2);  // Expected: Room 102 added to Neurology Clinic
            Console.WriteLine("\n-----------------------------------------------------------------");

            // Create patients
            InPatient inpatient1 = new InPatient("Jane Doe", 30, Gender.Female, 101, "Cardiac Arrest", doctor1, DateTime.Now);
            OutPatient outpatient1 = new OutPatient("Mark Doe", 28, Gender.Male, 102, "Migraine", neurologyClinic);


            // Assign room to in-patient
            Console.WriteLine("In-Patient room assignment:\n");
            inpatient1.AssignRoom(room1); // Expected: Room 101 becomes occupied
            Console.WriteLine("\n-----------------------------------------------------------------");

            // Book an appointment for out-patient in Cardiology Clinic
            Console.WriteLine("Out-Patient clinic assignment and booking appointment:\n");
            outpatient1.BookAppointment(cardiologyClinic, new DateTime(2024, 10, 5), TimeSpan.FromHours(10)); // Expected: Appointment at 10 AM booked
            Console.WriteLine("\n-----------------------------------------------------------------");

            // View doctor's assigned clinics
            Console.WriteLine("Displaying clinics that doctors are assigned to:\n");
            doctor1.DisplayAssignedClinics(); // Expected: Cardiology Clinic is displayed
            Console.WriteLine("\n");
            doctor2.DisplayAssignedClinics();
            Console.WriteLine("\n-----------------------------------------------------------------");

            Console.WriteLine("Displaying available appointments in clinics:\n");
            // View available appointments in Cardiology Clinic
            cardiologyClinic.DisplayAvailableAppointments();
            Console.WriteLine("\n");
            neurologyClinic.DisplayAvailableAppointments();
            // Expected: Show available slots for Dr. John Smith at 9 AM, 11 AM (10 AM is booked)
            Console.WriteLine("\n");


            // Discharge in-patient
            Console.WriteLine("Discharging a patient:\n");
            inpatient1.Discharge(); // Expected: Room 101 becomes available, patient discharged
            Console.WriteLine("\n-----------------------------------------------------------------");

            // Book another appointment for the same out-patient in Cardiology Clinic
            Console.WriteLine("Booking another appointment for the same out-patient:\n");
            outpatient1.BookAppointment(cardiologyClinic, new DateTime(2024, 10, 5), TimeSpan.FromHours(11)); // Expected: Appointment at 11 AM booked
            Console.WriteLine("\n");
            cardiologyClinic.DisplayAvailableAppointments();
            Console.WriteLine("\n");
            neurologyClinic.DisplayAvailableAppointments();
            Console.WriteLine("\n");

            // Try booking a time outside available slots
            Console.WriteLine("Trying to book an appointment outside available slots:\n");
            outpatient1.BookAppointment(cardiologyClinic, new DateTime(2024, 10, 5), TimeSpan.FromHours(12));
            // Expected: No available appointments for this time
            Console.WriteLine("\n-----------------------------------------------------------------");

            // Cancel an appointment
            Console.WriteLine("Cancelling an appointment:\n");
            cardiologyClinic.CancelAppointment(outpatient1, new DateTime(2024, 10, 5), TimeSpan.FromHours(10));
            // Expected: Appointment cancellation message for 10 AM
            Console.WriteLine("\n-----------------------------------------------------------------");

            // View available appointments after booking and cancellation
            Console.WriteLine("Displaying available appointments in clinics:\n");
            cardiologyClinic.DisplayAvailableAppointments();
            Console.WriteLine("\n");
            neurologyClinic.DisplayAvailableAppointments();
            // Expected: 10 AM slot available again, 9 AM and 11 AM booked
        }


        // ------------------------------------- User - Input Section -------------------------------------|
        // ----------------------------------------- NOT READY YET ----------------------------------------|

        public static void AddDoctor()
        {
            int DId;
            int DrCount = hospital.GetDoctors().Count;
            if (DrCount == 0)
            {
                DId = 1;
            }
            else
            {
                DId = hospital.GetDoctors()[DrCount - 1].DoctorID + 1;
            }

            string DName;
            Console.Clear();
            Console.WriteLine("Enter the new doctor's name:\n");
            while (string.IsNullOrEmpty(DName = Console.ReadLine()) || !Regex.IsMatch(NameRegexFormat, DName))
            {
                Console.Clear();
                Console.WriteLine("Enter the new doctor's name:\n");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }

            int DAge;
            Console.Clear();
            Console.WriteLine("Enter the new doctor's age:\n");
            while(!int.TryParse(Console.ReadLine(), out DAge) || DAge < 20)
            {
                Console.Clear();
                Console.WriteLine("Enter the new doctor's age:\n");
                if (DAge < 20)
                {
                    Console.WriteLine("\nAge cannot be lower than 20, please try again.\n");
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please try again.");
                }
            }

            int GenderChoice;
            Console.Clear();
            Console.WriteLine("Choose the appropriate gender:\n\n1. Male\n\n2. Female\n\n3. Other");
            while(!int.TryParse(Console.ReadLine(), out GenderChoice) || GenderChoice > 3 || GenderChoice < 1)
            {
                Console.Clear();
                Console.WriteLine("Choose the appropriate gender:\n\n1. Male\n\n2. Female\n\n3. Other\n");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }
            Gender DGender;
            if (GenderChoice == 1)
            {
                DGender = Gender.Male;
            }
            else if(GenderChoice == 2)
            {
                DGender = Gender.Female;
            }
            else
            {
                DGender = Gender.Other;
            }

            int SpecChoice;
            Console.Clear();
            Console.WriteLine("Choose the appropriate Specialization:\n\n1. Cardiology\n\n2. Neurology\n\n3. Dermatology");
            while (!int.TryParse(Console.ReadLine(), out SpecChoice) || SpecChoice > 3 || SpecChoice < 1)
            {
                Console.Clear();
                Console.WriteLine("Choose the appropriate Specialization:\n\n1. Cardiology\n\n2. Neurology\n\n3. Dermatology");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }

            DocSpecialization DSpec;
            if (SpecChoice == 1)
            {
                DSpec = DocSpecialization.Cardiology;
            }
            else if (SpecChoice == 2)
            {
                DSpec = DocSpecialization.Neurology;
            }
            else
            {
                DSpec = DocSpecialization.Dermatology;
            }
            hospital.AddDoctor(new Doctor(DId, DName, DAge, DGender, DSpec));
        }

        public static void AddNurse()
        {
            int NId;
            int NursesCount = hospital.GetNurses().Count;
            if (NursesCount == 0)
            {
                NId = 1;
            }
            else
            {
                NId = hospital.GetNurses()[NursesCount - 1].NurseID + 1;
            }

            string NurseName;
            Console.Clear();
            Console.WriteLine("Enter the new nurse's name:\n");
            while (string.IsNullOrEmpty(NurseName = Console.ReadLine()) || !Regex.IsMatch(NameRegexFormat, NurseName))
            {
                Console.Clear();
                Console.WriteLine("Enter the new nurse's name:\n");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }

            int NurseAge;
            Console.Clear();
            Console.WriteLine("Enter the new nurse's age:\n");
            while (!int.TryParse(Console.ReadLine(), out NurseAge) || NurseAge < 20)
            {
                Console.Clear();
                Console.WriteLine("Enter the new nurse's age:\n");
                if (NurseAge < 20)
                {
                    Console.WriteLine("\nAge cannot be lower than 20, please try again.\n");
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please try again.");
                }
            }

            int GenderChoice;
            Console.Clear();
            Console.WriteLine("Choose the appropriate gender:\n\n1. Male\n\n2. Female\n\n3. Other");
            while (!int.TryParse(Console.ReadLine(), out GenderChoice) || GenderChoice > 3 || GenderChoice < 1)
            {
                Console.Clear();
                Console.WriteLine("Choose the appropriate gender:\n\n1. Male\n\n2. Female\n\n3. Other\n");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }
            Gender NurseGender;
            if (GenderChoice == 1)
            {
                NurseGender = Gender.Male;
            }
            else if (GenderChoice == 2)
            {
                NurseGender = Gender.Female;
            }
            else
            {
                NurseGender = Gender.Other;
            }

            int SpecChoice;
            Console.Clear();
            Console.WriteLine("Choose the appropriate Specialization:\n\n1. General\n\n2. Pediatrics\n\n3. Surgery");
            while (!int.TryParse(Console.ReadLine(), out SpecChoice) || SpecChoice > 3 || SpecChoice < 1)
            {
                Console.Clear();
                Console.WriteLine("Choose the appropriate Specialization:\n\n1. General\n\n2. Pediatrics\n\n3. Surgery");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }

            NurseSpecialization NurseSpec;
            if (SpecChoice == 1)
            {
                NurseSpec = NurseSpecialization.General;
            }
            else if (SpecChoice == 2)
            {
                NurseSpec = NurseSpecialization.Pediatrics;
            }
            else
            {
                NurseSpec = NurseSpecialization.Surgery;
            }
            hospital.AddNurse(new Nurse(NId, NurseName, NurseAge, NurseGender, NurseSpec));
        }

        public static void AddPatient()
        {
            int PId;
            int PCount = hospital.GetPatients().Count;
            if (PCount == 0)
            {
                PId = 1;
            }
            else
            {
                PId = hospital.GetPatients()[PCount - 1].Item1.PatientID + 1;
            }

            string PName;
            Console.Clear();
            Console.WriteLine("Enter the new patient's name:\n");
            while (string.IsNullOrEmpty(PName = Console.ReadLine()) || !Regex.IsMatch(NameRegexFormat, PName))
            {
                Console.Clear();
                Console.WriteLine("Enter the new patient's name:\n");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }

            int PAge;
            Console.Clear();
            Console.WriteLine("Enter the new patient's age:\n");
            while (!int.TryParse(Console.ReadLine(), out PAge) || PAge < 1)
            {
                Console.Clear();
                Console.WriteLine("Enter the new patient's age:\n");
                if (PAge < 1)
                {
                    Console.WriteLine("\nAge cannot be lower than 1, please try again.\n");
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please try again.");
                }
            }

            int GenderChoice;
            Console.Clear();
            Console.WriteLine("Choose the appropriate gender:\n\n1. Male\n\n2. Female\n\n3. Other");
            while (!int.TryParse(Console.ReadLine(), out GenderChoice) || GenderChoice > 3 || GenderChoice < 1)
            {
                Console.Clear();
                Console.WriteLine("Choose the appropriate gender:\n\n1. Male\n\n2. Female\n\n3. Other\n");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }
            Gender PGender;
            if (GenderChoice == 1)
            {
                PGender = Gender.Male;
            }
            else if (GenderChoice == 2)
            {
                PGender = Gender.Female;
            }
            else
            {
                PGender = Gender.Other;
            }

            string Illness;
            Console.Clear();
            Console.WriteLine("Enter the ailment of the patient:\n");
            while (string.IsNullOrEmpty(Illness = Console.ReadLine()) || int.TryParse(Illness, out _))
            {
                Console.Clear();
                Console.WriteLine("Enter the ailment of the patient:\n");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }

            int PatientTypeChoice;
            Console.Clear();
            Console.WriteLine("Register as In-Patient(1) OR Out-Patient(2):\n");
            while (!int.TryParse(Console.ReadLine(), out PatientTypeChoice) || PatientTypeChoice > 2 || PatientTypeChoice < 1)
            {
                Console.Clear();
                Console.WriteLine("Register as In-Patient(1) OR Out-Patient(2):\n");
                Console.WriteLine("\nInvalid input, please try again.\n");
            }
            Console.Clear();
            if (PatientTypeChoice == 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                string border = new string('-', 50);
                sb.AppendLine($"{"Doctor Name", -20} | {"Specialization", -20}");
                sb.AppendLine(border);
                var DocList = hospital.GetDoctors();
                for (int i = 0; i < DocList.Count; i++)
                {
                    sb.AppendLine($"{DocList[i].Name,-20} | {DocList[i].specialization.ToString(),-20}");
                }
                Console.WriteLine(sb.ToString());
                Console.WriteLine("\nChoose a doctor from the list:\n");

                int AssignDocChoice;
                while (!int.TryParse(Console.ReadLine(), out AssignDocChoice) || AssignDocChoice > DocList.Count || AssignDocChoice < 1)
                {
                    Console.Clear();
                    Console.WriteLine(sb.ToString());
                    Console.WriteLine("\nChoose a doctor from the list:\n");
                    Console.WriteLine("\nInvalid input, please try again.\n");
                }
                Patient patient = new Patient(PId, PName, PAge, PGender, Illness);
                hospital.AddPatient(patient, true, DocList[AssignDocChoice - 1], DateTime.Now.ToString(), null);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                string border = new string('-', 50);
                sb.AppendLine($"{"Clinic Name",-20} | {"Specialization",-20}");
                sb.AppendLine(border);
                var ClinicList = hospital.GetClinics();
                for (int i = 0; i < ClinicList.Count; i++)
                {
                    sb.AppendLine($"{ClinicList[i].ClinicName,-20} | {ClinicList[i].specialization.ToString(),-20}");
                }
                Console.WriteLine(sb.ToString());
                Console.WriteLine("\nChoose a clinic from the list:\n");

                int AssignClinicChoice;
                while (!int.TryParse(Console.ReadLine(), out AssignClinicChoice) || AssignClinicChoice > ClinicList.Count || AssignClinicChoice < 1)
                {
                    Console.Clear();
                    Console.WriteLine(sb.ToString());
                    Console.WriteLine("\nChoose a clinic from the list:\n");
                    Console.WriteLine("\nInvalid input, please try again.\n");
                }
                Patient patient = new Patient(PId, PName, PAge, PGender, Illness);
                hospital.AddPatient(patient, false, null, null, ClinicList[AssignClinicChoice-1]);
            }
        }

        public static void AddClinic()
        {
            int CID;
            var ClinicList = hospital.GetClinics();
            if (ClinicList.Count == 0)
            {
                CID = 1;
            }
            else
            {
                CID = ClinicList[ClinicList.Count - 1].ClinicID +1;
            }

            string CName;
            Console.Clear();
            Console.WriteLine("Enter the clinic name:\n");
            while (string.IsNullOrEmpty(CName = Console.ReadLine()) || int.TryParse(CName, out _))
            {
                Console.Clear();
                Console.WriteLine("Enter the clinic name:\n");
                Console.WriteLine("\nInvalid input, please try again:\n");
            }

            Specialization ClinicSpec;
            int SpecChoice;
            Console.Clear();
            Console.WriteLine("Choose a specialization for the clinic:\n1. Cardiology.\n2. Neurology.\n3. Dermatology.\n");
            while (!int.TryParse(Console.ReadLine(), out SpecChoice) || SpecChoice > 3 || SpecChoice < 1)
            {
                Console.Clear();
                Console.WriteLine("Choose a specialization for the clinic:\n1. Cardiology.\n2. Neurology.\n3. Dermatology.\n");
                Console.WriteLine("\nInvalid input, please try again:\n");
            }

            if (SpecChoice == 1)
            {
                ClinicSpec = Specialization.Cardiology;
            }
            else if (SpecChoice == 2)
            {
                ClinicSpec = Specialization.Neurology;
            }
            else
            {
                ClinicSpec = Specialization.Dermatology;
            }

            hospital.AddClinic(new Clinic(CID, CName, ClinicSpec));
        }

        public static void AddRoom()
        {
            int RoomNumber;
            Console.Clear();
            Console.WriteLine("Enter the new room number:\n");
            while (!int.TryParse(Console.ReadLine(), out RoomNumber) || RoomNumber < 1 || VerifyRoom(RoomNumber))
            {
                Console.Clear();
                Console.WriteLine("Enter the new room number:\n");
                if (RoomNumber < 1)
                {
                    Console.WriteLine("\nRoom number must be greater than 0:\n");
                }
                else if (VerifyRoom(RoomNumber))
                {
                    Console.WriteLine("\nRoom number is already used, please try again:\n");
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please try again:\n");
                }
            }

            int roomTypeChoice;
            Console.Clear();
            Console.WriteLine("Choose a room type for the new room:\n\n1. In-Patient Room.\n2. Out-Patient Room\n");
            while (!int.TryParse(Console.ReadLine(),out roomTypeChoice) || roomTypeChoice > 3 || roomTypeChoice < 1)
            {
                Console.Clear();
                Console.WriteLine("Choose a room type for the new room:\n\n1. In-Patient Room.\n2. Out-Patient Room\n");
                Console.WriteLine("\nInvalid choice, please try again:\n");
            }

            RoomType roomType;
            if (roomTypeChoice == 1)
            {
                roomType = RoomType.IPR;
            }
            else
            {
                roomType = RoomType.OPR;
            }

            hospital.AddRoom(new Room(RoomNumber, roomType));
        }

        public static bool VerifyRoom(int roomNumber)
        {
            bool RoomExists = false;
            var RoomList = hospital.GetRooms();

            for (int i = 0; i < RoomList.Count; i++)
            {
                if (RoomList[i].RoomNumber == roomNumber)
                {
                    RoomExists = true;
                    break;
                }
            }

            return RoomExists;
        }

        public static void AssignRoomToInPatient()
        {
            var PatientList = hospital.GetInPatients();
            var RoomsList = hospital.GetRooms();
            StringBuilder sb = new StringBuilder();
            string border = new string('-', 50);

            sb.AppendLine("Unassigned In-Patients:");
            sb.AppendLine();
            sb.AppendLine($"{"No.", -5} | {"Name", -20} | {"ID", -20} | {"Ailment", -10}");
            sb.AppendLine(border);

            int count = 1;
            for (int i = 0; i < PatientList.Count; i++)
            {
                if (PatientList[i].Room == null)
                {
                    sb.AppendLine($"{count} | {PatientList[i].Name,-20} | {PatientList[i].PatientID,-20} | {PatientList[i].Ailment,-10}");
                    count++;
                }
            }

            sb.AppendLine();
            sb.AppendLine("Choose a patient from the listing number:");
            sb.AppendLine();
            Console.Clear();
            Console.WriteLine( sb.ToString());

            int InPatientNumber;
            while(!int.TryParse(Console.ReadLine(), out InPatientNumber) || InPatientNumber > count || InPatientNumber < 1)
            {
                Console.Clear();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("Invalid input, please try again:\n");
            }

            sb.Clear();
            Console.Clear();

            sb.AppendLine("Available Rooms:");
            sb.AppendLine();
            sb.AppendLine($"{"No.", -5} | {"Number",-10} | {"Type",-20}");
            sb.AppendLine(border);

            count = 1;
            for (int i = 0; i < RoomsList.Count; i++)
            {
                if (RoomsList[i].IsOccupied != true)
                {
                    sb.AppendLine($"{count} | {RoomsList[i].RoomNumber,-20} | {RoomsList[i].roomType,-20}");
                    count++;
                }
            }

            sb.AppendLine();
            sb.AppendLine($"Choose a room to assign to patient {PatientList[InPatientNumber - 1]}:");
            sb.AppendLine();
            Console.WriteLine(sb.ToString());

            int AssignedRoom;
            while (!int.TryParse(Console.ReadLine(), out AssignedRoom) || AssignedRoom > count || AssignedRoom < 1)
            {
                Console.Clear();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("Invalid input, please try again:\n");
            }

            sb.Clear();
            Console.Clear();
            hospital.AssignRoomToPatient(PatientList[InPatientNumber - 1], RoomsList[AssignedRoom - 1]);
        }

        public static void AssignNurseToClinic()
        {
            var NurseList = hospital.GetNurses();
            var ClinicsList = hospital.GetClinics();

            StringBuilder sb = new StringBuilder();
            string border = new string('-', 60);
            sb.AppendLine("Unassigned Nurses:");
            sb.AppendLine();
            sb.AppendLine($"{"No.", -5} | {"Name", -20} | {"ID", -5} | {"Specialization", -20}");
            sb.AppendLine(border);

            int count = 1;
            for (int i = 0; i < NurseList.Count; i++)
            {
                if (NurseList[i].AssignedClinic == null)
                {
                    sb.AppendLine($"{count,-5} | {NurseList[i].Name,-20} | {NurseList[i].NurseID,-5} | {NurseList[i].NurseSpec,-20}");
                    count++;
                }
            }

            sb.AppendLine();
            sb.AppendLine($"Choose a nurse to assign:");
            sb.AppendLine();
            Console.WriteLine(sb.ToString());

            int NurseNumber;
            while (!int.TryParse(Console.ReadLine(), out NurseNumber) || NurseNumber > NurseList.Count || NurseNumber < 1)
            {
                Console.Clear();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("Invalid input, please try again:\n");
            }

            sb.Clear();
            Console.Clear();

            sb.AppendLine("Available Clinics:");
            sb.AppendLine();
            sb.AppendLine($"{"No.",-5} | {"Name",-20} | {"ID",-5} | {"Specialization",-20}");
            sb.AppendLine(border);

            count = 1;
            for (int i = 0; i < ClinicsList.Count; i++)
            {
                sb.AppendLine($"{count,-5} | {ClinicsList[i].ClinicName,-20} | {ClinicsList[i].ClinicID,-5} | {ClinicsList[i].specialization,-20}");
                count++;
            }

            sb.AppendLine();
            sb.AppendLine($"Choose a clinic to assign nurse {NurseList[NurseNumber-1].Name} to:");
            sb.AppendLine();
            Console.WriteLine(sb.ToString());

            int ClinicNumber;
            while (!int.TryParse(Console.ReadLine(), out ClinicNumber) || ClinicNumber > ClinicsList.Count || ClinicNumber < 1)
            {
                Console.Clear();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("Invalid input, please try again:\n");
            }

            hospital.AssignNurseToClinic(NurseList[NurseNumber-1], ClinicsList[ClinicNumber-1]);
        }

        public static void DischargeInPatient()
        {
            var InPatientsList = hospital.GetInPatients();
            StringBuilder sb = new StringBuilder();
            string border = new string('-', 50);
            sb.AppendLine("Admitted In-Patients:");
            sb.AppendLine();
            sb.AppendLine($"{"No.", -5} | {"Name", -20} | {"ID", -5} | {"Ailment", -20} | {"Room", -5}");
            sb.AppendLine(border);
            int count = 1;
            for (int i = 0; i < InPatientsList.Count;i++)
            {
                if (InPatientsList[i].Room != null)
                {
                    sb.AppendLine($"{count,-5} | {InPatientsList[i].Name,-20} | {InPatientsList[i].PatientID,-5} | {InPatientsList[i].Ailment,-20} | {InPatientsList[i].Room, -5}");
                    count++;
                }
            }
            sb.AppendLine();
            sb.AppendLine("Choose an In-Patient to discharge:");
            sb.AppendLine();

            Console.Clear();
            Console.WriteLine(sb.ToString());

            int InPatientChoice;
            while (!int.TryParse(Console.ReadLine(), out InPatientChoice) || InPatientChoice < 1 || InPatientChoice > count)
            {
                Console.Clear();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("\nInvalid input, please try again:\n");
            }

            hospital.DischargePatient(InPatientsList[InPatientChoice - 1]);
        }
    }
}
