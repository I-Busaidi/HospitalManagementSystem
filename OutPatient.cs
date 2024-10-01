using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class OutPatient : Patient
    {
        public Clinic ClinicAssigned;
        public OutPatient(int PatientID, string Name, int Age, Gender gender, string Ailment, Clinic AssignedClinic) 
            : base (PatientID, Name, Age, gender, Ailment)
        {
            ClinicAssigned = AssignedClinic;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Patient ID: {PatientID}, Name: {Name}, Age: {Age}\n" +
                $"Gender: {gender}, Ailment: {Ailment}, Assigned Clinic: {ClinicAssigned.ClinicName}\n");

            foreach(var Kvp in ClinicAssigned.AvailableAppointments)
            {
                for(int i = 0; i < Kvp.Value.Count; i++)
                {
                    if (Kvp.Value[i].Patient.Name == Name)
                    {
                        Console.WriteLine($"Upcoming appointment: {Kvp.Value[i].AppointmentDate.Value.ToString("dddd mm, yyyy")} " +
                            $"Time: {Kvp.Value[i].AppointmentTime.ToString()}");
                        return;
                    }
                }
            }
        }
    }
}
