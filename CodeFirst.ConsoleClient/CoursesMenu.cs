using CodeFirst.Data;
using CodeFirst.Models;
using System;
using System.Linq;

namespace CodeFirst.ConsoleClient
{
    public static class CoursesMenu
    {
        private static Random rnd = new Random();

        public static void ShowMenu(IStudentSystemData db)
        {
            int choice = 0;
            var courseName = "";
            var description = "";
            var exerciseName = "";
            var exerciseContent = "";
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose option:");
                Console.WriteLine("0. Back to main menu");
                Console.WriteLine("1. Add course");
                Console.WriteLine("2. Delete course");
                Console.WriteLine("3. Print all courses info");
                Console.WriteLine("4. Print all courses lecturers");
                Console.WriteLine("5. Add new exercise to course");
                Console.WriteLine("6. Print all courses exercises");
                Console.Write("Choice: ");

                var ans = Console.ReadLine();

                if (int.TryParse(ans, out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;

                        case 1:
                            Console.Write("Enter new course name: ");
                            courseName = Console.ReadLine();
                            Console.Write("Enter course description: ");
                            description = Console.ReadLine();
                            AddNewCourse(db, courseName, description);
                            break;

                        case 2:
                            Console.Write("Enter existing course name: ");
                            courseName = Console.ReadLine();
                            DeleteCourse(db, courseName);
                            break;

                        case 3:
                            PrintAllCourses(db);
                            break;

                        case 4:
                            PrintLecturersInCourses(db);
                            break;

                        case 5:
                            Console.Write("Enter existing course name: ");
                            courseName = Console.ReadLine();
                            Console.Write("Enter new exercise name: ");
                            exerciseName = Console.ReadLine();
                            Console.Write("Enter exercise content: ");
                            exerciseContent = Console.ReadLine();
                            AddExerciseToCourse(db, courseName, exerciseName, exerciseContent);
                            break;

                        case 6:
                            PrintCoursesExercises(db);
                            break;

                        default:
                            Console.Write("Wrong choice, back to main menu.");
                            loop = false;
                            break;
                    }
                }
            } while (loop);
        }

        private static void PrintAllCourses(IStudentSystemData db)
        {
            try
            {
                var courses = db.Courses.All().ToList();
                Console.WriteLine(String.Format("{0, -15}", "Course name".ToUpper()) + " | " + "Course description".ToUpper());
                foreach (var course in courses)
                {
                    Console.WriteLine(String.Format("{0, -15}", course.Name) + " | " + course.Description);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No enrties found...");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void DeleteCourse(IStudentSystemData db, string courseName)
        {
            var course = db.Courses.SearchFor(c => c.Name == courseName).FirstOrDefault();

            foreach (StudentCourse sc in course.StudentsCourses.ToList()) db.StudentCourses.Delete(sc);
            foreach (Exercise ex in course.Exercises.ToList()) db.Exercises.Delete(ex);
            foreach (Homework hw in course.Homeworks.ToList()) db.Homeworks.Delete(hw);

            db.Courses.Delete(course);
            db.SaveChanges();
        }

        private static void AddNewCourse(IStudentSystemData db, string courseName, string description)
        {
            var course = new Course { Name = courseName, Description = description, Materials = "" };
            db.Courses.Add(course);
            db.SaveChanges();
        }

        private static void PrintLecturersInCourses(IStudentSystemData db)
        {
            try
            {
                var courses = db.Courses.All().ToList();
                foreach (var c in courses)
                {
                    Console.WriteLine("Course " + c.Name + " is lectured by:");
                    foreach (Lecturer lecturer in c.Lecturers)
                    {
                        Console.WriteLine(lecturer.Title.ToString() + " " + lecturer.FirstName + " " + lecturer.LastName);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No enrties found...");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void PrintCoursesExercises(IStudentSystemData db)
        {
            try
            {
                var courses = db.Courses.All().ToList();
                foreach (var c in courses)
                {
                    Console.WriteLine("Course " + c.Name + " has the following exercises:");
                    foreach (Exercise exercise in c.Exercises)
                    {
                        Console.WriteLine(exercise.ExerciseName + " content: " + exercise.ExerciseContent);
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No enrties found...");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void AddExerciseToCourse(IStudentSystemData db, string courseName, string exerciseName, string exerciseContent)
        {
            var course = db.Courses.SearchFor(c => c.Name == courseName).FirstOrDefault();
            var exercise = new Exercise { ExerciseName = exerciseName, ExerciseContent = exerciseContent };
            db.Exercises.Add(exercise);
            course.Exercises.Add(exercise);
            db.SaveChanges();
        }
    }
}