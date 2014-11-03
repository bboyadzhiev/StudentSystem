using CodeFirst.Data;
using CodeFirst.Data.Migrations;
using CodeFirst.Models;
using System;
using System.Linq;

namespace CodeFirst.ConsoleClient
{
    public class Program
    {
        private static bool demo = false;
        private static Random rnd = new Random();

        private static void Main(string[] args)
        {
            //var sqlServerPath = @".";
            //var sqlServerPath = @"(localdb)\v11.0";
            var sqlServerPath = @".\SQLEXPRESS";

            Config.SetConnectionString("StudentSystem", sqlServerPath);
            var contextFactory = new MigrationsContextFactory();
            var db = new StudentSystemData(contextFactory.Create());

            Menu(db);
        }

        private static void Menu(IStudentSystemData db)
        {
            bool loop = true;
            int choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose option:");
                Console.WriteLine("0. EXIT");
                Console.WriteLine("1. Students menu");
                Console.WriteLine("2. Lecturers menu");
                Console.WriteLine("3. Courses menu");
                if (!demo) Console.WriteLine("9. ADD DEMO DATA");
                Console.Write("Choice: ");

                var ans = Console.ReadLine();
                if (int.TryParse(ans, out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            loop = false;
                            Console.Write("Exiting...");
                            break;

                        case 1:
                            StudentsMenu.ShowMenu(db);
                            break;

                        case 2:
                            LecturersMenu.ShowMenu(db);
                            break;

                        case 3:
                            CoursesMenu.ShowMenu(db);
                            break;

                        case 9:
                            if (!demo) SeedSomeData(db);
                            break;

                        default:
                            Console.Write("Wrong choice, exiting...");
                            loop = false;
                            break;
                    }
                }
            } while (loop);

            Course course = db.Courses
                .All()
                .Where(c => c.Name == "PIK")
                .FirstOrDefault();
            var lecturer = course.Lecturers;
        }

        private static void SeedSomeData(IStudentSystemData db)
        {
            var studentTodor = new Student { Age = 10, FirstName = "Todor", LastName = "Todorov", SSN = "1111111111", StudentNumber = 1000000 };
            var studentAngel = new Student { Age = 10, FirstName = "Angel", LastName = "Angelov", SSN = "2222222222", StudentNumber = 1000001 };
            db.Students.Add(studentTodor);
            db.Students.Add(studentAngel);

            var lecturer1 = new Lecturer { Age = 45, FirstName = "Georgi", LastName = "Georgiev", Title = Title.Proffessor, SSN = "3333333333" };
            var lecturer2 = new Lecturer { Age = 35, FirstName = "Petar", LastName = "Petrov", Title = Title.PhD, SSN = "4444444444" };
            db.Lecturers.Add(lecturer1);
            db.Lecturers.Add(lecturer2);
            db.SaveChanges();

            var pikCourse = new Course { Name = "PIK", Materials = "C, C++", Description = "A course of C-programming" };
            var databaseCourse = new Course { Name = "Databases", Materials = "ORM, Entity Framework, Microsoft SQL", Description = "A course for databases in Visual Studio" };
            db.Courses.Add(pikCourse);
            db.Courses.Add(databaseCourse);
            db.SaveChanges();

            lecturer1.Courses.Add(pikCourse);
            lecturer1.Courses.Add(databaseCourse);
            lecturer2.Courses.Add(databaseCourse);
            db.SaveChanges();

            var pikEx1 = new Exercise { Course = pikCourse, ExerciseName = "Hello World", ExerciseContent = "Write a Hello World application" };
            var pikEx2 = new Exercise { Course = pikCourse, ExerciseName = "Console read", ExerciseContent = "Reading from the console" };
            var dbEx1 = new Exercise { Course = databaseCourse, ExerciseName = "SQL Querise", ExerciseContent = "Creating simple SQL queries" };
            db.Exercises.Add(pikEx1);
            db.Exercises.Add(pikEx2);
            db.Exercises.Add(dbEx1);
            db.SaveChanges();

            StudentsMenu.AddStudentToCourse(db, "Todor", "PIK");
            StudentsMenu.AddStudentToCourse(db, "Todor", "Databases");
            StudentsMenu.AddStudentToCourse(db, "Angel", "Databases");

            studentTodor.Homeworks.Add(new Homework { Course = pikCourse, HomeworkContent = "Todor's homework for PIK", TimeSent = DateTime.Now });
            studentTodor.Homeworks.Add(new Homework { Course = databaseCourse, HomeworkContent = "Todor's homework for Databases", TimeSent = DateTime.Now });
            db.SaveChanges();

            studentTodor.Exercises.Add(pikEx1);
            studentTodor.Exercises.Add(pikEx2);
            studentTodor.Exercises.Add(dbEx1);
            studentAngel.Exercises.Add(dbEx1);
            db.SaveChanges();
            Console.WriteLine("DEMO DATA ADDED - Press any key to continue");
            demo = true;
            Console.ReadKey();
        }
    }
}