using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Appointment
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime? AppointmentDate { get; set; }

        public Appointment(Patient patient, Doctor doctor, DateTime AppointmentDate)
        {
            Patient = patient;
            Doctor = doctor;
            this.AppointmentDate = AppointmentDate;
        }

        public void ScheduleAppointment(DateTime date)
        {
            AppointmentDate = date;
        }

        public void CancelAppointment()
        {
            AppointmentDate = null;
        }

        public void GetAppointmentDetails()
        {
            Console.WriteLine($"Appointment Scheduled for {Patient.Name} with {Doctor.Name} on {AppointmentDate.Value.ToString("MMMM dd, yyyy a\\t h:mm:sstt")}");
        }
    }
}
