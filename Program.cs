using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            AddRecords();
            
        }

        public static void AddRecords()
        {
            using (HospetalContext hContext = new HospetalContext())
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

                hContext.Doctors.Add(doctor1);
                hContext.Doctors.Add(doctor2);

                //Adding Patents ---------------------------------------------
                Patient patient1 = new Patient()
                { 
                    Address ="Hyderabad",
                    PatientFirstName = "Vikram",
                    PatientLastName = "Voma"
                };

                Patient patient2 = new Patient()
                {
                    Address = "Hyderabad",
                    PatientFirstName = "Vikram",
                    PatientLastName = "Voma"
                };

                hContext.Patients.Add(patient1);
                hContext.Patients.Add(patient2);

                Appointment appointment1 = new Appointment() { AppointmentTime= DateTime.Now.Date, Doctor = doctor1, Patient = patient1  };

                hContext.Appointments.Add(appointment1);

                hContext.SaveChanges();

                //Display Doctors List
                var doctors = (from d in hContext.Doctors
                               orderby d.DoctorFirstName
                               select d).ToList();

                Console.WriteLine("-------List of doctors------");
                foreach (Doctor d in doctors)
                {
                    Console.WriteLine("Doctor FirstName: " + d.DoctorFirstName);
                }

                Console.WriteLine("Press any Key");
                Console.Read();
            }
        }
    
    }
}
