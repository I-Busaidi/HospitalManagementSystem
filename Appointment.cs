using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Appointment
    {
        public Patient? Patient;
        public DateTime? AppointmentDate;
        public TimeSpan? AppointmentTime;
        public bool IsBooked;

        public Appointment(Patient patient, DateTime AppointmentDate, TimeSpan period)
        {
            Patient = patient;
            this.AppointmentDate = AppointmentDate;
            AppointmentTime = period;
            IsBooked = true;
        }

        public Appointment(DateTime AppointmentDate, TimeSpan period)
        {
            this.AppointmentDate = AppointmentDate;
            AppointmentTime = period;
            IsBooked = false;
        }

        public void ScheduleAppointment(Patient patient, DateTime date, TimeSpan period)
        {
            Patient = patient;
            AppointmentDate = date;
            AppointmentTime = period;
            IsBooked = true;
        }

        public void CancelAppointment(DateTime date, TimeSpan period)
        {
            Patient = null;
            AppointmentDate = null;
            AppointmentTime = null;
            IsBooked = false;
        }

        public void GetAppointmentDetails()
        {
            Console.WriteLine($"Appointment Scheduled for {Patient.Name} on {AppointmentDate.Value.ToString("MMMM dd, yyyy a\\t h:mm:sstt")}");
        }
    }
}
