using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtabaseFirstApproach
{
    class Program
    {
        static void Main(string[] args)
        {
            AddRecords();
            FetchRecords();

            Console.WriteLine("Press Any Key");
            Console.Read();

        }
        public static void AddRecords()
        {
            using (var db = new HospetalDBFirstEntities())
            {
                //Adding new Doctor  ---------------------------------------------
                Doctor doctor1 = new Doctor()
                {
                    DoctorFirstName = "Mahesh123",
                    DoctorLastName = "Pendker",
                    EmailAddress = "mahesh0919@gmail.com"
                };

                Doctor doctor2 = new Doctor()
                {
                    DoctorFirstName = "Rakesh123",
                    DoctorLastName = "Pendker",
                    EmailAddress = "rakesh.p@gmail.com"
                };

                //Adding Patents ---------------------------------------------
                Patient patient1 = new Patient()
                {
                    Address = "Hyderabad",
                    FirstName = "Vikram",
                    PatientLastName = "Voma"
                };

                Patient patient2 = new Patient()
                {
                    Address = "Hyderabad",
                    FirstName = "Vikram",
                    PatientLastName = "Voma"
                };

                db.Patients.Add(patient1);
                db.Patients.Add(patient2);

                //Adding Appointments ------------------------------------------
                Appointment appointment1 = new Appointment() { AppointmentTime = DateTime.Now.Date, Doctor = doctor1, Patient = patient1 };
                Appointment appointment2 = new Appointment() { AppointmentTime = DateTime.Now.Date, Doctor = doctor2, Patient = patient2 };
                db.Appointments.Add(appointment1);
                db.Appointments.Add(appointment2);

                db.Doctors.Add(doctor1);
                db.Doctors.Add(doctor2);

                db.SaveChanges();
            }

        }

         public static void FetchRecords()
         {
             using (var db = new HospetalDBFirstEntities())
             {
                 var query = from d in db.Doctors
                             select d;

                 Console.WriteLine("Fetching all Doctors");

                 foreach (Doctor d in db.Doctors)
                 {
                     Console.WriteLine("Doctor Name: " + d.DoctorFirstName);
                 }
             }
         }
    }
}
