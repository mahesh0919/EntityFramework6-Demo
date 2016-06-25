using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFramework
{
    public class Doctor
    {
        public Doctor() { }

        public int DoctorID { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName{ get; set; }
        public string EmailAddress{ get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }

    public class Patient
    {
        public Patient() { }

        public int PatientID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        public Appointment() { }

        public int AppointmentID { get; set; }

        //Foreign key for Doctor
        public int DoctorID { get; set; }

        //Foreign key for Patient
        public int PatientID { get; set; }
        public DateTime? AppointmentTime { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }

    public class HospetalContext: DbContext
    {
        public HospetalContext()
            : base("HospetalDB")   // if connection string HospetalDB not found in app.config,it will throw exception
        { 
        
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }

    }

}
