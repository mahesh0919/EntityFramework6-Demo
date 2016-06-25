﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EntityFramework
{
    public class Doctor
    {
        public Doctor() { }

        public int DoctorID { get; set; }

        [MaxLength(20)]
        public string DoctorFirstName { get; set; }
        [StringLength(20)]
        public string DoctorLastName{ get; set; }
        [ConcurrencyCheck]
        public string EmailAddress{ get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }

    public class Patient
    {
        public Patient() { }

        [Key]
        public int PID { get; set; }
        [MaxLength(20)]
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        [ConcurrencyCheck]
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
        public int PID { get; set; }
        public DateTime? AppointmentTime { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }

    public class HospetalContext: DbContext
    {
        public HospetalContext()
            : base("name=HospetalDB")   // if connection string HospetalDB not found in app.config,it will throw exception
        {

            Database.SetInitializer<HospetalContext>(new HospetalDBInitializer<HospetalContext>());
            //Database.SetInitializer<HospetalContext>(new DropCreateDatabaseAlways<HospetalContext>());
            //Database.SetInitializer<HospetalContext>(new CreateDatabaseIfNotExists<HospetalContext>());
            //Database.SetInitializer<HospetalContext>(new HospetalContext());
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
    }

    public class HospetalDBInitializer<T> : DropCreateDatabaseAlways<HospetalContext>
    {
        protected override void Seed(HospetalContext context)
        {
            //Adding new Doctor  ---------------------------------------------
            Doctor doctor1 = new Doctor()
            {
                DoctorFirstName = "Mahesh",
                DoctorLastName = "Pendker",
                EmailAddress = "mahesh0919@gmail.com"
            };

            Doctor doctor2 = new Doctor()
            {
                DoctorFirstName = "Rakesh",
                DoctorLastName = "Pendker",
                EmailAddress = "rakesh.p@gmail.com"
            };

            //Adding Patents ---------------------------------------------
            Patient patient1 = new Patient()
            {
                Address = "Hyderabad",
                PatientFirstName = "Vikram",
                PatientLastName = "Voma"
            };

            Patient patient2 = new Patient()
            {
                Address = "Hyderabad",
                PatientFirstName = "Vikram",
                PatientLastName = "Voma"
            };

            context.Patients.Add(patient1);
            context.Patients.Add(patient2);

            //Adding Appointments ------------------------------------------
            Appointment appointment1 = new Appointment() { AppointmentTime= DateTime.Now.Date, Doctor = doctor1, Patient = patient1  };
            Appointment appointment2 = new Appointment() { AppointmentTime = DateTime.Now.Date, Doctor = doctor2, Patient = patient2 };
            context.Appointments.Add(appointment1);
            context.Appointments.Add(appointment2);

            context.Doctors.Add(doctor1);
            context.Doctors.Add(doctor2);

            base.Seed(context);
        }
    }

}
