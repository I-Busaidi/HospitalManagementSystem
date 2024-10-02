using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class OutPatient : Patient
    {
        public Clinic ClinicAssigned { get; private set; }
        public OutPatient(string Name, int Age, Gender gender, int PatientID, string Ailment, Clinic AssignedClinic) 
            : base (PatientID, Name, Age, gender, Ailment)
        {
            ClinicAssigned = AssignedClinic;
        }

        public void BookAppointment(Clinic clinic, DateTime appointmentDay, TimeSpan appointmentTime)
        {
            clinic.BookAppointment(this, appointmentDay, appointmentTime);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Patient ID: {PatientID}, Name: {Name}, Age: {Age}\n" +
                $"Gender: {gender}, Ailment: {Ailment}, Assigned Clinic: {ClinicAssigned.ClinicName}\n");
            if (ClinicAssigned != null && ClinicAssigned.AvailableAppointments != null)
            {
                foreach (var Kvp in ClinicAssigned.AvailableAppointments)
                {
                    for (int i = 0; i < Kvp.Value.Count; i++)
                    {
                        if (Kvp.Value[i].Patient.Name == Name)
                        {
                            Console.WriteLine($"Upcoming appointment: {Kvp.Value[i].AppointmentDate.ToString("ddd MMM, yyyy")} " +
                                $"Time: {Kvp.Value[i].AppointmentTime.ToString()}");
                            return;
                        }
                    }
                }
            }
        }
    }
}
