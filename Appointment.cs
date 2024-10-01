﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Appointment
    {
        public Patient? Patient;
        public DateTime AppointmentDate;
        public TimeSpan AppointmentTime;
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
            IsBooked = false;
        }

        public void GetAppointmentDetails()
        {
            if (IsBooked && Patient != null)
            {
                Console.WriteLine($"Appointment Scheduled for {Patient.Name} on {AppointmentDate.ToString("ddd MMM, yyyy a\\t h:mm:sstt")}");
            }
            else
            {
                Console.WriteLine($"No Appointment Scheduled on {AppointmentDate.ToString("ddd MMM, yyyy a\\t h:mm:sstt")}");
            }
        }
    }
}
