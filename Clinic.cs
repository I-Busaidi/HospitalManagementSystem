﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HospitalManagementSystem
{
    public class Clinic : IDisplayInfo, ISchedulable
    {
        public int ClinicID { get; private set; }
        public string ClinicName { get; private set; }
        public enum Specialization
        {
            Cardiology, Neurology, Dermatology
        }
        public Specialization specialization { get; private set; }
        public List<Room> rooms = new List<Room>();
        public Dictionary<Doctor, List<Appointment>> AvailableAppointments = new Dictionary<Doctor, List<Appointment>>();
        public List<Nurse> ClinicNursesList = new List<Nurse>();

        public Clinic(int ClinicID, string ClinicName, Specialization specialization)
        {
            this.ClinicID = ClinicID;
            this.ClinicName = ClinicName;
            this.specialization = specialization;
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);
            Console.WriteLine($"Room {room.RoomNumber} ({room.roomType}) added to {ClinicName}");
        }

        public void AddAvailableAppointment(Doctor doctor, DateTime appointmentDay, TimeSpan period)
        {
            if (!AvailableAppointments.ContainsKey(doctor))
            {
                AvailableAppointments[doctor] = new List<Appointment>();
            }
            int hrs = period.Hours;
            for(int i = 0; i < hrs; i++)
            {
                Appointment appointment = new Appointment(appointmentDay, TimeSpan.FromHours(9+i));
                AvailableAppointments[doctor].Add(appointment);
                Console.WriteLine($"{TimeSpan.FromHours(9+i).ToString()}");
                appointment.AddAvailableFreeSlots();
            }
        }

        public void BookAppointment(Patient patient, Doctor doctor, DateTime appointmentDay, TimeSpan appointmentTime)
        {
            if (AvailableAppointments.TryGetValue(doctor, out var appointments))
            {
                foreach (var appointment in appointments)
                {
                    if (appointment.AppointmentDate == appointmentDay 
                        && appointment.AppointmentTime == appointmentTime 
                        && !appointment.IsBooked)
                    {
                        appointment.ScheduleAppointment(patient, appointmentDay, appointmentTime);
                        Console.WriteLine($"Patient {patient.Name} has been assigned to {ClinicName}");
                        Console.WriteLine($"Appointment booked for {patient.Name} at {appointmentTime} with {doctor.Name}");
                        return;
                    }
                }
            }
            Console.WriteLine($"Sorry, no available appointments on {appointmentDay.ToString("ddd ~ dd MMM, yyyy")} at {appointmentTime}.");
        }

        public void ScheduleAppointment(Patient patient, DateTime appointmentDay, TimeSpan appointmentTime)
        {
            foreach (var Kvp in AvailableAppointments)
            {
                for (int i = 0; i < Kvp.Value.Count; i++)
                {
                    if (Kvp.Value[i].AppointmentDate == appointmentDay 
                        && Kvp.Value[i].AppointmentTime == appointmentTime 
                        && !Kvp.Value[i].IsBooked)
                    {
                        Kvp.Value[i].ScheduleAppointment(patient, appointmentDay, appointmentTime);
                        Console.WriteLine($"Patient {patient.Name} has been assigned to {ClinicName}");
                        Console.WriteLine($"Appointment booked for {patient.Name} with {Kvp.Key.Name} at {appointmentTime}");
                        return;
                    }
                }
            }
            Console.WriteLine($"Sorry, no available appointments on {appointmentDay.ToString("ddd ~ dd MMM, yyyy")} at {appointmentTime}.");
        }

        public void CancelAppointment(Patient patient, DateTime appointmentDay, TimeSpan appointmentTime)
        {
            foreach (var Kvp in AvailableAppointments)
            {
                for (int i = 0; i < Kvp.Value.Count; i++)
                {
                    if (Kvp.Value[i].AppointmentDate == appointmentDay 
                        && Kvp.Value[i].AppointmentTime == appointmentTime 
                        && Kvp.Value[i].IsBooked 
                        && Kvp.Value[i].Patient.Name == patient.Name)
                    {
                        Kvp.Value[i].CancelAppointment(patient, appointmentDay, appointmentTime);
                        Console.WriteLine($"Appointment at {appointmentTime} for patient {patient.Name} cancelled.");
                        return;
                    }
                }
            }
        }

        public void DisplayAvailableAppointments()
        {
            StringBuilder sb = new StringBuilder();
            string border = new string('-', 65);
            sb.AppendLine($"Available appointments in {ClinicName}:");
            sb.AppendLine();
            sb.AppendLine($"{"Doctor", -20} | {"Day", -30} | {"Period", -30}");
            sb.AppendLine(border);
            bool FirstLine = true;
            foreach (var Kvp in AvailableAppointments)
            {
                sb.Append($"{Kvp.Key.Name,-20} | ");
                for (int i = 0; i < Kvp.Value.Count; i++)
                {
                    if (!Kvp.Value[i].IsBooked)
                    {
                        if (FirstLine)
                        {
                            sb.AppendLine($"{Kvp.Value[i].AppointmentDate.ToString("ddd ~ dd MMM, yyyy"),-30} " +
                            $"| {Kvp.Value[i].AppointmentTime.ToString(),-30}");
                            FirstLine = false;
                        }
                        else
                        {
                            sb.AppendLine($"{"",-20} | {Kvp.Value[i].AppointmentDate.ToString("ddd ~ dd MMM, yyyy"),-30} " +
                            $"| {Kvp.Value[i].AppointmentTime.ToString(),-30}");
                        }

                        if (i != Kvp.Value.Count - 1)
                        {
                            sb.AppendLine($"{"",-20} | {"",-30} |");
                        }
                    }
                }
                sb.Append(border);
            }

            Console.WriteLine( sb.ToString());
        }

        public void DisplayInfo()
        {
            StringBuilder sb = new StringBuilder();
            string border = new string('-', 35);
            sb.AppendLine($"Clinic Name: {ClinicName}");
            sb.AppendLine($"Clinic ID: {ClinicID}");
            sb.AppendLine($"Specialization: {specialization}");
            sb.AppendLine("Rooms:");
            sb.AppendLine($"{"Room No.", -10} | {"Room Type", -20}");
            sb.AppendLine(border);
            for (int i = 0;i < rooms.Count;i++)
            {
                sb.AppendLine($"{rooms[i].RoomNumber,-10} | {rooms[i].roomType,-20}");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
