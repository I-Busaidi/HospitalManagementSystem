﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public interface IPatientCare
    {
        void CheckVitals(Patient patient);
    }
}
