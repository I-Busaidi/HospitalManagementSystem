using System.Text;
using static HospitalManagementSystem.Clinic;
using static HospitalManagementSystem.Doctor;
using static HospitalManagementSystem.Person;
using static HospitalManagementSystem.Room;

namespace HospitalManagementSystem
{
    internal class Program
    {
        public static Hospital hospital = new Hospital();

        static void Main(string[] args)
        {
            // Create doctors
            Doctor doctor1 = new Doctor(1, "Dr. John Smith", 45, Gender.Male, DocSpecialization.Cardiology);
            Doctor doctor2 = new Doctor(2, "Dr. Alice Brown", 38, Gender.Female, DocSpecialization.Neurology);


            // Create clinics
            Clinic cardiologyClinic = new Clinic(1, "Cardiology Clinic", Specialization.Cardiology);
            Clinic neurologyClinic = new Clinic(2, "Neurology Clinic", Specialization.Neurology);


            // Assign doctors to clinics and generate appointment slots (9 AM - 12 PM)
            doctor1.AssignToClinic(cardiologyClinic, new DateTime(2024, 10, 5), TimeSpan.FromHours(3)); // Expected: Appointments generated for 9 AM, 10 AM, 11 AM
            doctor2.AssignToClinic(neurologyClinic, new DateTime(2024, 10, 6), TimeSpan.FromHours(3));  // Expected: Appointments generated for 9 AM, 10 AM, 11 AM


            // Create rooms for clinics
            Room room1 = new Room(101, RoomType.IPR);  // Room for in-patients
            Room room2 = new Room(102, RoomType.OPR);  // Room for out-patients
            cardiologyClinic.AddRoom(room1); // Expected: Room 101 added to Cardiology Clinic
            neurologyClinic.AddRoom(room2);  // Expected: Room 102 added to Neurology Clinic


            // Create patients
            InPatient inpatient1 = new InPatient("Jane Doe", 30, Gender.Female, 101, "Cardiac Arrest", doctor1, DateTime.Now);
            OutPatient outpatient1 = new OutPatient("Mark Doe", 28, Gender.Male, 102, "Migraine", neurologyClinic);


            // Assign room to in-patient
            inpatient1.AssignRoom(room1); // Expected: Room 101 becomes occupied


            // Book an appointment for out-patient in Cardiology Clinic
            outpatient1.BookAppointment(cardiologyClinic, new DateTime(2024, 10, 5), TimeSpan.FromHours(10)); // Expected: Appointment at 10 AM booked


            // View doctor's assigned clinics
            doctor1.DisplayAssignedClinics(); // Expected: Cardiology Clinic is displayed


            // View available appointments in Cardiology Clinic
            cardiologyClinic.DisplayAvailableAppointments();
            // Expected: Show available slots for Dr. John Smith at 9 AM, 11 AM (10 AM is booked)


            // Discharge in-patient
            inpatient1.Discharge(); // Expected: Room 101 becomes available, patient discharged


            // Book another appointment for the same out-patient in Cardiology Clinic
            outpatient1.BookAppointment(cardiologyClinic, new DateTime(2024, 10, 5), TimeSpan.FromHours(11)); // Expected: Appointment at 11 AM booked


            // Try booking a time outside available slots
            outpatient1.BookAppointment(cardiologyClinic, new DateTime(2024, 10, 5), TimeSpan.FromHours(12));
            // Expected: No available appointments for this time


            // Cancel an appointment
            cardiologyClinic.CancelAppointment(outpatient1, new DateTime(2024, 10, 5), TimeSpan.FromHours(10));
            // Expected: Appointment cancellation message for 10 AM


            // View available appointments after booking and cancellation
            cardiologyClinic.DisplayAvailableAppointments();
            // Expected: 10 AM slot available again, 9 AM and 11 AM booked
        }



        //USER INPUT SECTION

        //static void AddNewDoctor()
        //{
        //    int DocID;
        //    var DocList = hospital.GetDoctors();

        //    if (DocList.Count > 0)
        //    {
        //        DocID = DocList[DocList.Count - 1].GetID() + 1;
        //    }
        //    else
        //    {
        //        DocID = 1;
        //    }

        //    string DocName;
        //    Console.WriteLine("Enter the doctor's name: \n");
        //    while (string.IsNullOrEmpty(DocName = Console.ReadLine()) || int.TryParse(DocName, out _))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Enter the doctor's name: \n");
        //        Console.WriteLine("Invalid input, please try again:\n");
        //    }

        //    int DocAge;
        //    Console.Clear();
        //    Console.WriteLine($"Enter the age of Dr. {DocName}:\n");
        //    while (!int.TryParse(Console.ReadLine(), out DocAge) || DocAge < 20)
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"Enter the age of Dr. {DocName}:\n");
        //        Console.WriteLine("Invalid input, please try again.\n");
        //    }

        //    int DocGender;
        //    Console.Clear();
        //    Console.WriteLine($"Choose gender for Dr. {DocName}: \n1. Male\n2. Female\n3. Other\n");
        //    while (!int.TryParse(Console.ReadLine(), out DocGender) || DocGender < 1 || DocGender > 3)
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"Choose gender for Dr. {DocName}: \n1. Male\n2. Female\n3. Other\n");
        //        Console.WriteLine("Invalid input, please try again.\n");
        //    }
        //    Gender gender = Gender.Male;
        //    switch (DocGender)
        //    {
        //        case 1:
        //            gender = Gender.Male;
        //            break;

        //        case 2:
        //            gender = Gender.Female;
        //            break;

        //        case 3:
        //            gender = Gender.Other;
        //            break;
        //    }

        //    Console.Clear();
        //    Console.WriteLine($"Enter the specialization of Dr. {DocName}:\n");
        //    string Spec;
        //    while (string.IsNullOrEmpty(Spec = Console.ReadLine()) || int.TryParse(Spec, out _))
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"Enter the specialization of Dr. {DocName}:\n");
        //        Console.WriteLine("Invalid input, please try again.\n");
        //    }

        //    hospital.AddDoctor(new Doctor(DocID, DocName, DocAge, gender, Spec));
        //    Console.Clear();
        //    Console.WriteLine($"Dr. {DocName} has been added successfully.");
        //}

        //static void AddNewPatient()
        //{
        //    int PatID;
        //    var PatList = hospital.GetPatients();

        //    if (PatList.Count > 0)
        //    {
        //        PatID = PatList[PatList.Count - 1].GetID() + 1;
        //    }
        //    else
        //    {
        //        PatID = 1;
        //    }

        //    string PatName;
        //    Console.WriteLine("Enter the patient's name: \n");
        //    while (string.IsNullOrEmpty(PatName = Console.ReadLine()) || int.TryParse(PatName, out _))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Enter the patient's name: \n");
        //        Console.WriteLine("Invalid input, please try again:\n");
        //    }

        //    int PatAge;
        //    Console.Clear();
        //    Console.WriteLine($"Enter the age of {PatName}:\n");
        //    while (!int.TryParse(Console.ReadLine(), out PatAge) || PatAge < 0)
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"Enter the age of {PatName}:\n");
        //        Console.WriteLine("Invalid input, please try again.\n");
        //    }

        //    int PatGender;
        //    Console.Clear();
        //    Console.WriteLine($"Choose gender for {PatName}: \n1. Male\n2. Female\n3. Other\n");
        //    while (!int.TryParse(Console.ReadLine(), out PatGender) || PatGender < 1 || PatGender > 3)
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"Choose gender for {PatName}: \n1. Male\n2. Female\n3. Other\n");
        //        Console.WriteLine("Invalid input, please try again.\n");
        //    }
        //    Gender gender = Gender.Male;
        //    switch (PatGender)
        //    {
        //        case 1:
        //            gender = Gender.Male;
        //            break;

        //        case 2:
        //            gender = Gender.Female;
        //            break;

        //        case 3:
        //            gender = Gender.Other;
        //            break;
        //    }

        //    Console.Clear();
        //    Console.WriteLine($"Enter the ailment of {PatName}:\n");
        //    string Ailment;
        //    while (string.IsNullOrEmpty(Ailment = Console.ReadLine()) || int.TryParse(Ailment, out _))
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"Enter the ailment of {PatName}:\n");
        //        Console.WriteLine("Invalid input, please try again.\n");
        //    }

        //    Console.Clear();
        //    var DocList = hospital.GetDoctors();
        //    int DocIndex = -1;
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < DocList.Count; i++)
        //    {
        //        sb.AppendLine($"{(i + 1)}. Dr. {DocList[i].Name}, Specialization: {DocList[i].GetSpec()}");
        //        sb.AppendLine();
        //    }
        //    Console.WriteLine(sb.ToString());
        //    Console.WriteLine("\nChoose a doctor to assign to patient:\n");

        //    while (!int.TryParse(Console.ReadLine(), out DocIndex) || DocIndex < 1 || DocIndex > DocList.Count)
        //    {
        //        Console.Clear();
        //        Console.WriteLine(sb.ToString());
        //        Console.WriteLine("\nChoose a doctor to assign to patient:\n");
        //        Console.WriteLine("Invalid input, please try again:\n");
        //    }

        //    hospital.AddPatient(new Patient(PatID, PatName, PatAge, gender, Ailment, DocList[DocIndex - 1]));
        //    Console.Clear();
        //    Console.WriteLine($"Patient {PatName} has been added successfully.");
        //}

        //static void ShowDocInfo()
        //{
        //    var DocList = hospital.GetDoctors();
        //    int DocIndex = -1;
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < DocList.Count; i++)
        //    {
        //        sb.AppendLine($"{(i + 1)}. Dr. {DocList[i].Name}");
        //    }
        //    Console.WriteLine(sb.ToString());
        //    Console.WriteLine("\nChoose a doctor to view details:\n");

        //    while (!int.TryParse(Console.ReadLine(), out DocIndex) || DocIndex < 1 || DocIndex > DocList.Count)
        //    {
        //        Console.Clear();
        //        Console.WriteLine(sb.ToString());
        //        Console.WriteLine("\nChoose a doctor to view details:\n");
        //        Console.WriteLine("Invalid input, please try again:\n");
        //    }

        //    DocList[DocIndex - 1].DisplayInfo();
        //    hospital.GetDoctorPatients(DocList[DocIndex - 1]);
        //}

        //static void ShowPatientInfo()
        //{
        //    var PatList = hospital.GetDoctors();
        //    int PatIndex = -1;
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < PatList.Count; i++)
        //    {
        //        sb.AppendLine($"{(i + 1)}. Name: {PatList[i].Name}");
        //    }
        //    Console.WriteLine(sb.ToString());
        //    Console.WriteLine("\nChoose a patient to view details:\n");

        //    while (!int.TryParse(Console.ReadLine(), out PatIndex) || PatIndex < 1 || PatIndex > PatList.Count)
        //    {
        //        Console.Clear();
        //        Console.WriteLine(sb.ToString());
        //        Console.WriteLine("\nChoose a patient to view details:\n");
        //        Console.WriteLine("Invalid input, please try again:\n");
        //    }

        //    Console.Clear();
        //    PatList[PatIndex - 1].DisplayInfo();
        //}

        //static void CreateAppointment()
        //{

        //}

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
