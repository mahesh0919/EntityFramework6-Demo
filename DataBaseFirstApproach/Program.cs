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


            GetRecords();
            //AddRecords();
            //FetchRecords();
            //CRUDOperation();
            //Transaction();

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

        public static void GetRecords()
        {
                 // Now Fetch records
                using (var db = new HospetalDBFirstEntitiesOverride())
                {
                    var query = from p in db.Patients
                                select p;

                    Console.WriteLine("Fetching all Patints");

                    foreach (Patient p in db.Patients)
                    {
                        Console.WriteLine("Doctor Name: " + p.FirstName);
                    }
                }
        }


        public static void FetchRecords()
         {
             GetPatientsList();
             GetDoctorsList();
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

        public static void Transaction()
        {
            //using (var db = new HospetalDBFirstEntities())
            //{
            //    Patient p1 = new Patient()
            //    {
            //        Address = "Hyderabad",
            //        Age = 15,
            //        FirstName = "Masood",
            //        Gender = "M",
            //        PatientLastName = "Ali"
            //    };
            //    db.Patients.Add(p1);

            //    Patient p2 = new Patient()
            //    {
            //        Address = "Warangal",
            //        Age = 15,
            //        FirstName = "Shanmukh",
            //        Gender = "M",
            //        PatientLastName = "Sai"
            //    };
            //    db.Patients.Add(p2);
            //    db.SaveChanges();        // In a single transaction add above 2 records
            //}

            // use transaction object
            using(var db = new HospetalDBFirstEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Patient p1 = new Patient()
                        {
                            Address = "Hyderabad",
                            Age = 15,
                            FirstName = "Masood",
                            Gender = "M",
                            PatientLastName = "Ali"
                        };
                        db.Patients.Add(p1);
                        db.SaveChanges();

                        Patient p2 = new Patient()
                        {
                            Address = "Warangal",
                            Age = 15,
                            FirstName = "Shanmukh",
                            Gender = "M",
                            PatientLastName = "Sai"
                        };
                        db.Patients.Add(p2);
                        db.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            GetPatientsList();
        }

        public static void GetPatientsList()
        {
            // Now Fetch records
            using (var db = new HospetalDBFirstEntities())
            {
                var query = from p in db.Patients
                            select p;

                Console.WriteLine("Fetching all Patints");

                foreach (Patient p in db.Patients)
                {
                    Console.WriteLine("Doctor Name: " + p.FirstName);
                }
            }
        }

        public static void GetDoctorsList()
        {
            using (var db = new HospetalDBFirstEntities())
            {
                var query = from d in db.Doctors
                            select d;

                Console.WriteLine("\n\nFetching all Doctors");

                foreach (Doctor d in db.Doctors)
                {
                    Console.WriteLine("Doctor Name: " + d.DoctorFirstName);
                }
            }
        }
    
    
    }
}
