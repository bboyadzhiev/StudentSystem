using CodeFirst.Data;
using CodeFirst.Models;
using System;
using System.Linq;

namespace CodeFirst.ConsoleClient
{
    public static class LecturersMenu
    {
        private static Random rnd = new Random();

        public static void ShowMenu(IStudentSystemData db)
        {
            int choice = 0;
            var lecturerName = "";
            var lecturerLastName = "";
            var courseName = "";
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose option:");
                Console.WriteLine("0. Back to main menu");
                Console.WriteLine("1. Add lecturer");
                Console.WriteLine("2. Delete lecturer");
                Console.WriteLine("3. Add lecturer to course");
                Console.WriteLine("4. Print lecturer's courses");
                Console.WriteLine("9. Print all lecturers's info");
                Console.Write("Choice: ");

                var ans = Console.ReadLine();

                if (int.TryParse(ans, out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;

                        case 1:
                            Console.Write("Enter new lecturer first name: ");
                            lecturerName = Console.ReadLine();
                            Console.Write("Enter new lecturer last name: ");
                            lecturerLastName = Console.ReadLine();
                            Console.Write("Enter lecturer ssn: ");
                            CreateLecturer(db, lecturerName, lecturerLastName, int.Parse(Console.ReadLine()));
                            break;

                        case 2:
                            Console.Write("Enter existing lecturer last name: ");
                            DeleteLecturer(db, Console.ReadLine());
                            break;

                        case 3:
                            Console.WriteLine("Enter lecturer's last name:");
                            lecturerLastName = Console.ReadLine();
                            Console.Write("Enter course's name: ");
                            courseName = Console.ReadLine();
                            AddLecturerToCourse(db, lecturerLastName, courseName);
                            break;

                        case 4:
                            Console.Write("Enter lecturer's last name: ");
                            lecturerLastName = Console.ReadLine();
                            PrintLecturersCourses(db, lecturerLastName);
                            break;

                        case 9:
                            PrintAllLecturers(db);
                            break;

                        default:
                            Console.Write("Wrong choice, back to main menu.");
                            loop = false;
                            break;
                    }
                }
            } while (loop);
        }

        private static void PrintAllLecturers(IStudentSystemData db)
        {
            try
            {
                var lecturers = db.Lecturers.All().ToList();
                foreach (var lecturer in lecturers)
                {
                    Console.WriteLine("Ful Name: " + lecturer.Title.ToString() + " " + lecturer.FirstName + " " + lecturer.LastName);
                    Console.WriteLine("Age: " + lecturer.Age + " SSN: " + lecturer.SSN);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No enrties found...");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void PrintLecturersCourses(IStudentSystemData db, string lecturerLastName)
        {
            try
            {
                var lecturer = db.Lecturers.SearchFor(s => s.LastName == lecturerLastName).FirstOrDefault();
                if (lecturer != null)
                {
                    Console.WriteLine(lecturer.Title.ToString() + " " + lecturer.FirstName + " " + lecturer.LastName + " is lecturing:");
                    Console.WriteLine(String.Format("{0, -15}", "Course name".ToUpper()) + " | " + "Course description".ToUpper());
                    foreach (var course in lecturer.Courses)
                    {
                        Console.WriteLine(String.Format("{0, -15}", course.Name) + " | " + course.Description);
                    }
                }
                else
                {
                    Console.WriteLine("Lecturer not found!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No enrties found...");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void AddLecturerToCourse(IStudentSystemData db, string lecturerLastName, string courseName)
        {
            var lecturer = db.Lecturers.SearchFor(s => s.LastName == lecturerLastName).FirstOrDefault();
            var course = db.Courses.SearchFor(c => c.Name == courseName).FirstOrDefault();
            course.Lecturers.Add(lecturer);
            db.SaveChanges();
        }

        private static void DeleteLecturer(IStudentSystemData db, string lecturerLastName)
        {
            try
            {
                var lecturer = db.Lecturers.SearchFor(s => s.LastName == lecturerLastName).FirstOrDefault();
                if (lecturer != null)
                {
                    db.Lecturers.Delete(lecturer);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Lecturer not found!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ", e.Message);
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void CreateLecturer(IStudentSystemData db, string lecturerName, string lecturerLastName, int ssn)
        {
            int age = rnd.Next(30, 60);

            var lecturer = new Lecturer { Age = age, FirstName = lecturerName, LastName = lecturerLastName, SSN = "1" + ssn.ToString(), Title = Title.Proffessor };
            db.Lecturers.Add(lecturer);
            db.SaveChanges();
        }
    }
}