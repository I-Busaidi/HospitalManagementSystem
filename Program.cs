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
    }
}
