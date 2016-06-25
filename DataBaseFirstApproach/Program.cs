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
            //AddRecords();
            //FetchRecords();
            CRUDOperation();

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

        public static void CRUDOperation()
        { 
           // Add records
         using(var db = new HospetalDBFirstEntities())
         {
             Doctor d1 = new Doctor() { DoctorFirstName ="David", Age= 40, DoctorLastName="Young", EmailAddress="test@gmail.com", Gender = "M" };
             Doctor d2 = new Doctor() { DoctorFirstName = "DavidTest", Age = 40, DoctorLastName = "Young", EmailAddress = "test@gmail.com", Gender = "M" };

             db.Doctors.Add(d1);
             db.Doctors.Add(d2);
             db.SaveChanges();
         }

        //Update Record
         using (var db = new HospetalDBFirstEntities())
         {
             var doctors = (from d in db.Doctors
                            where d.DoctorFirstName == "David"
                            select d);

             foreach (Doctor d in doctors)
             {
                 d.DoctorFirstName = "David Update";
             }
             db.SaveChanges();
         }

        //Delete Record
         using (var db = new HospetalDBFirstEntities())
         {
             var doctors = (from d in db.Doctors
                                    where d.DoctorFirstName == "DavidTest"
                                    select d);
             foreach (Doctor d in doctors)
             {
                 db.Doctors.Remove(d);
             }
             db.SaveChanges();
         }

        //Select Records
         using (var db = new HospetalDBFirstEntities())
         {
             foreach (Doctor d in db.Doctors)
             {
                 Console.WriteLine("Doctor Name: "+ d.DoctorFirstName);
             }
         }

        }
    }
}
