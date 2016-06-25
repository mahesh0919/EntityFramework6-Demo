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
