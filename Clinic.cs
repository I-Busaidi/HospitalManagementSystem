using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Clinic
    {
        protected int ClinicID;
        protected string ClinicName;
        public enum Specialization
        {
            Cardiology, Neurology, Dermatology
        }
        public Specialization specialization;
        protected List<Room> rooms;
        protected Dictionary<Doctor, List<Appointment>> AvailableAppointments;

        public Clinic(int ClinicID, string ClinicName, Specialization specialization)
        {
            this.ClinicID = ClinicID;
            this.ClinicName = ClinicName;
            this.specialization = specialization;
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        public void AddAvailableAppointment(Patient patient, Doctor doctor, DateTime appointmentDay, TimeSpan period)
        {
            Appointment appointment = new Appointment(patient, appointmentDay, period);
            AvailableAppointments[doctor].Add(appointment);
        }

        public string DisplayAvailableAppointments()
        {
            StringBuilder sb = new StringBuilder();
            string border = new string('-', 90);
            sb.AppendLine($"{"Doctor", -20} | {"Day", -30} | {"Period", -30} | {"Patient", -20}");
            sb.AppendLine(border);

            foreach(var Kvp in AvailableAppointments)
            {
                sb.Append($"{Kvp.Key.Name, -20} | ");
                for (int i = 0;i < Kvp.Value.Count;i++)
                {
                    sb.AppendLine($"{Kvp.Value[i].AppointmentDate.Value.ToString("dddd mm, yyyy"),-30} " +
                        $"| {Kvp.Value[i].AppointmentTime.ToString(),-30} | {Kvp.Value[i].Patient.Name,-20}");
                    sb.Append($"{"",-20} | ");
                }
                sb.AppendLine(border);
            }

            return sb.ToString();
        }
    }

}
