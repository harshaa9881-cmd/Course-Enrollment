using System;
using System.Collections.Generic;
using System.Text;
using Course_Enrollment.Models;

namespace Cserv.Services
{
        public class CourseServ
        {
            // Course creation
            public Courses[] Precourse;

            public CourseServ()
            {
                //coursecode is the index of the course in the array
                Courses c1 = new Courses(" ", "AIML", 40000, 30, 50, 3);
                Courses c2 = new Courses("co2", "AIDS", 50000, 40, 50, 2);
                Courses c3 = new Courses("co3", "CYBER", 30000, 30, 50, 1);
                Courses c4 = new Courses("co4", "DATA-ANALYST", 20000, 40, 50, 0);
                Courses c5 = new Courses("co5", "CLOUD-COMPUTING", 10000, 50, 50, 5);

                Precourse = new Courses[] { c1, c2, c3, c4, c5 };
            }

            // Display all courses
            public void DisplayCourses()
            {
                Console.WriteLine("All Courses: ");

                for (int i = 0; i < Precourse.Length; i++)
                {
                    Console.WriteLine(Precourse[i].Title);
                }
            }

            // Search by course code
            public void SearchByCode()
            {
                Console.WriteLine("Enter Coursecode to Search :");
                string? code = Console.ReadLine();
                int flag1 = 0;

                foreach (Courses i in Precourse)
                {
                    if (i.CourseCode == code)
                    {
                        Console.WriteLine("Course Found : " + i.Title);
                        flag1 = 1;
                        break;
                    }
                }

                if (flag1 == 0)
                {
                    Console.WriteLine("CourseNotFound");
                }
            }

            // Search by title
            public void SearchByTitle()
            {
                Console.WriteLine("Enter CourseTitle to Search :");
                string? ctitle = Console.ReadLine();
                int flag2 = 0;

                foreach (Courses i in Precourse)
                {
                    if (i.Title.ToLower() == ctitle.ToLower())
                    {
                        Console.WriteLine("Course Found : " + i.CourseCode);
                        flag2 = 1;
                        break;
                    }
                }

                if (flag2 == 0)
                {
                    Console.WriteLine("CourseNotFound");
                }
            }

            // Sort array based on fees
            public void SortByFee()
            {
                Courses[] sortedarray = (Courses[])Precourse.Clone();

                Array.Sort(sortedarray, (a, b) => a.Fee.CompareTo(b.Fee));

                foreach (Courses i in sortedarray)
                {
                    Console.WriteLine(i.Title + " - Fee: " + i.Fee);
                }
            }

            // Display available seats for every course
            public void DisplayAvailableSeats()
            {
                Console.WriteLine("Enter CourseId to Search :");
                string? cid = Console.ReadLine();
                foreach (Courses i in Precourse)
                {
                if (cid == i.CourseCode)
                {
                    Console.WriteLine("Course : " + i.CourseCode +
                        "  Available seats: " +
                        (i.TotalSeats - i.EnrolledCount));
                }
                }
            }
            public void countchange(int a,string code)
            {
            
                foreach (Courses i in Precourse)
                {
                    if (i.CourseCode == code)
                    {
                        if (a == 0)//decrement
                        {
                            i.EnrolledCount--;
                        }
                        else if (a == 1)//increament
                        {
                            i.EnrolledCount++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("count inc/dec failed");
                    }
                    
                }
            }
        }
    }

