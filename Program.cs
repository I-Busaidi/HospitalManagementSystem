using System.Text;
using static HospitalManagementSystem.Person;
using static HospitalManagementSystem.Room;

namespace HospitalManagementSystem
{
    internal class Program
    {
        public static Hospital hospital = new Hospital();

        static void Main(string[] args)
        {
            
            // Test Case 1: Create doctors and patients
            Console.WriteLine("===== Test Case 1: Create Doctors and Patients =====");
            Doctor doctor1 = new Doctor(1, "Dr. Smith", 45, Gender.Male, "Cardiology");
            Doctor doctor2 = new Doctor(2, "Dr. Brown", 38, Gender.Female, "Neurology");
            hospital.AddDoctor(doctor1);
            hospital.AddDoctor(doctor2);
            Patient patient1 = new Patient(101, "John Doe", 30, Gender.Male, "Heart Disease", doctor1);
            hospital.AddPatient(patient1);
            Patient patient2 = new Patient(102, "Jane Roe", 28, Gender.Female, "Migraine", doctor2);
            hospital.AddPatient(patient2);
            Patient patient3 = new Patient(104, "Donald Trump", 28, Gender.Male, "Migraine", doctor2);
            hospital.AddPatient(patient3);
            Patient patient4 = new Patient(106, "Will Smith", 28, Gender.Male, "Fever", doctor1);
            hospital.AddPatient(patient4);
            Patient patient5 = new Patient(107, "Peter Roe", 28, Gender.Male, "Acid reflex", doctor2);
            hospital.AddPatient(patient5);
            Patient patient6 = new Patient(109, "Jane Hammilton", 28, Gender.Female, "Migraine", doctor1);
            hospital.AddPatient(patient6);
            // Display information
            patient1.DisplayInfo();
            patient2.DisplayInfo();
            doctor1.DisplayInfo();
            doctor2.DisplayInfo();
            // Test Case 2: Assign rooms to patients
            Console.WriteLine("\n===== Test Case 2: Room Assignment =====");
            Room room1 = new Room(202, RoomType.ICU);
            hospital.AddRoom(room1);
            Room room2 = new Room(203, RoomType.General);
            hospital.AddRoom(room2);

            hospital.AssignRoomToPatient(patient1, room1);
            hospital.AssignRoomToPatient(patient2, room2);
            //patient1.AssignRoom(room1);
            //patient2.AssignRoom(room2);


            // Display room details
            Console.WriteLine($"Room {room1.RoomNumber} is occupied: {room1.IsOccupied}");
            Console.WriteLine($"Room {room2.RoomNumber} is occupied: {room2.IsOccupied}");
            // Test Case 3: Schedule appointments
            Console.WriteLine("\n===== Test Case 3: Schedule Appointments =====");
            Appointment appointment1 = new Appointment(patient1, doctor1, new DateTime(2024, 10,
           5, 9, 30, 0));
            appointment1.ScheduleAppointment(new DateTime(2024, 10, 5, 9, 30, 0));
            appointment1.GetAppointmentDetails();
            Appointment appointment2 = new Appointment(patient2, doctor2, new DateTime(2024, 10,
           6, 11, 0, 0));
            appointment2.ScheduleAppointment(new DateTime(2024, 10, 6, 11, 0, 0));
            appointment2.GetAppointmentDetails();
            // Test Case 4: Discharge patients
            Console.WriteLine("\n===== Test Case 4: Discharge Patients =====");
            patient1.Discharge();
            Console.WriteLine($"Patient {patient1.Name} has been discharged. Room { room1.RoomNumber} is now occupied: { room1.IsOccupied}");
            // Test Case 5: Display doctor-patient details
            Console.WriteLine("\n===== Test Case 5: Display Doctor Details =====");
            doctor1.DisplayInfo();
            doctor2.DisplayInfo();

            Console.WriteLine("\n===== Test Case 6: Display Doctor-Patient Details =====");
            hospital.GetDoctorPatients(doctor1);
            Console.WriteLine("---------------------------------------------------------");
            hospital.GetDoctorPatients(doctor2);

        }

        static void AddNewDoctor()
        {
            int DocID;
            var DocList = hospital.GetDoctors();

            if (DocList.Count > 0)
            {
                DocID = DocList[DocList.Count - 1].GetID() + 1;
            }
            else
            {
                DocID = 1;
            }

            string DocName;
            Console.WriteLine("Enter the doctor's name: \n");
            while (string.IsNullOrEmpty(DocName = Console.ReadLine()) || int.TryParse(DocName, out _ ))
            {
                Console.Clear();
                Console.WriteLine("Enter the doctor's name: \n");
                Console.WriteLine("Invalid input, please try again:\n");
            }

            int DocAge;
            Console.Clear();
            Console.WriteLine($"Enter the age of Dr. {DocName}:\n");
            while (!int.TryParse(Console.ReadLine(), out DocAge) || DocAge < 20)
            {
                Console.Clear();
                Console.WriteLine($"Enter the age of Dr. {DocName}:\n");
                Console.WriteLine("Invalid input, please try again.\n");
            }

            int DocGender;
            Console.Clear();
            Console.WriteLine($"Choose gender for Dr. {DocName}: \n1. Male\n2. Female\n3. Other\n");
            while (!int.TryParse(Console.ReadLine(),out DocGender) || DocGender < 1 || DocGender > 3)
            {
                Console.Clear();
                Console.WriteLine($"Choose gender for Dr. {DocName}: \n1. Male\n2. Female\n3. Other\n");
                Console.WriteLine("Invalid input, please try again.\n");
            }
            Gender gender = Gender.Male;
            switch(DocGender)
            {
                case 1:
                    gender = Gender.Male;
                    break;

                case 2:
                    gender = Gender.Female;
                    break;

                case 3:
                    gender = Gender.Other; 
                    break;
            }

            Console.Clear() ;
            Console.WriteLine($"Enter the specialization of Dr. {DocName}:\n");
            string Spec;
            while (string.IsNullOrEmpty(Spec = Console.ReadLine()) || int.TryParse(Spec, out _))
            {
                Console.Clear();
                Console.WriteLine($"Enter the specialization of Dr. {DocName}:\n");
                Console.WriteLine("Invalid input, please try again.\n");
            }

            hospital.AddDoctor(new Doctor(DocID, DocName, DocAge, gender, Spec));
            Console.Clear();
            Console.WriteLine($"Dr. {DocName} has been added successfully.");
        }

        static void AddNewPatient()
        {
            int PatID;
            var PatList = hospital.GetPatients();

            if (PatList.Count > 0)
            {
                PatID = PatList[PatList.Count - 1].GetID() + 1;
            }
            else
            {
                PatID = 1;
            }

            string PatName;
            Console.WriteLine("Enter the patient's name: \n");
            while (string.IsNullOrEmpty(PatName = Console.ReadLine()) || int.TryParse(PatName, out _))
            {
                Console.Clear();
                Console.WriteLine("Enter the patient's name: \n");
                Console.WriteLine("Invalid input, please try again:\n");
            }

            int PatAge;
            Console.Clear();
            Console.WriteLine($"Enter the age of {PatName}:\n");
            while (!int.TryParse(Console.ReadLine(), out PatAge) || PatAge < 0)
            {
                Console.Clear();
                Console.WriteLine($"Enter the age of {PatName}:\n");
                Console.WriteLine("Invalid input, please try again.\n");
            }

            int PatGender;
            Console.Clear();
            Console.WriteLine($"Choose gender for {PatName}: \n1. Male\n2. Female\n3. Other\n");
            while (!int.TryParse(Console.ReadLine(), out PatGender) || PatGender < 1 || PatGender > 3)
            {
                Console.Clear();
                Console.WriteLine($"Choose gender for {PatName}: \n1. Male\n2. Female\n3. Other\n");
                Console.WriteLine("Invalid input, please try again.\n");
            }
            Gender gender = Gender.Male;
            switch (PatGender)
            {
                case 1:
                    gender = Gender.Male;
                    break;

                case 2:
                    gender = Gender.Female;
                    break;

                case 3:
                    gender = Gender.Other;
                    break;
            }

            Console.Clear();
            Console.WriteLine($"Enter the ailment of {PatName}:\n");
            string Ailment;
            while (string.IsNullOrEmpty(Ailment = Console.ReadLine()) || int.TryParse(Ailment, out _))
            {
                Console.Clear();
                Console.WriteLine($"Enter the ailment of {PatName}:\n");
                Console.WriteLine("Invalid input, please try again.\n");
            }

            Console.Clear();
            var DocList = hospital.GetDoctors();
            int DocIndex = -1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < DocList.Count; i++)
            {
                sb.AppendLine($"{(i+1)}. Dr. {DocList[i].Name}, Specialization: {DocList[i].GetSpec()}");
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
            Console.WriteLine("\nChoose a doctor to assign to patient:\n");

            while(!int.TryParse(Console.ReadLine(), out DocIndex) || DocIndex < 1 || DocIndex > DocList.Count)
            {
                Console.Clear();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("\nChoose a doctor to assign to patient:\n");
                Console.WriteLine("Invalid input, please try again:\n");
            }

            hospital.AddPatient(new Patient(PatID, PatName, PatAge, gender, Ailment, DocList[DocIndex-1]));
            Console.Clear();
            Console.WriteLine($"Patient {PatName} has been added successfully.");
        }

        static void ShowDocInfo()
        {
            var DocList = hospital.GetDoctors();
            int DocIndex = -1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < DocList.Count; i++)
            {
                sb.AppendLine($"{(i + 1)}. Dr. {DocList[i].Name}");
            }
            Console.WriteLine(sb.ToString());
            Console.WriteLine("\nChoose a doctor to view details:\n");

            while (!int.TryParse(Console.ReadLine(), out DocIndex) || DocIndex < 1 || DocIndex > DocList.Count)
            {
                Console.Clear();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("\nChoose a doctor to view details:\n");
                Console.WriteLine("Invalid input, please try again:\n");
            }

            DocList[DocIndex - 1].DisplayInfo();
            hospital.GetDoctorPatients(DocList[DocIndex - 1]);
        }

        static void ShowPatientInfo()
        {
            var PatList = hospital.GetDoctors();
            int PatIndex = -1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < PatList.Count; i++)
            {
                sb.AppendLine($"{(i + 1)}. Name: {PatList[i].Name}");
            }
            Console.WriteLine(sb.ToString());
            Console.WriteLine("\nChoose a patient to view details:\n");

            while (!int.TryParse(Console.ReadLine(), out PatIndex) || PatIndex < 1 || PatIndex > PatList.Count)
            {
                Console.Clear();
                Console.WriteLine(sb.ToString());
                Console.WriteLine("\nChoose a patient to view details:\n");
                Console.WriteLine("Invalid input, please try again:\n");
            }

            Console.Clear() ;
            PatList[PatIndex - 1].DisplayInfo();
        }

        //static (bool, string?) VerifyDocName(List<Doctor> Doctors, string Name)
        //{
        //    bool NameExist = false;
        //    string? Message = null;

        //    for (int i = 0;i < Doctors.Count; i++)
        //    {
        //        if (Doctors[i].Name == Name)
        //        {
        //            NameExist = true;
        //            Message = "\nThis name already exists, please try again.\n";
        //            break;
        //        }
        //    }

        //    return (NameExist, Message);
        //}

    }
}
