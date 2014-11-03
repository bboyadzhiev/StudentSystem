using CodeFirst.Data;
using CodeFirst.Models;
using System;
using System.Linq;

namespace CodeFirst.ConsoleClient
{
    public static class StudentsMenu
    {
        private static Random rnd = new Random();

        public static void ShowMenu(IStudentSystemData db)
        {
            int choice = 0;
            var studentName = "";
            var studentLastName = "";
            var courseName = "";
            var hwConent = "";
            var exerciseName = "";
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose option:");
                Console.WriteLine("0. Back to main menu");
                Console.WriteLine("1. Add student");
                Console.WriteLine("2. Delete student ");
                Console.WriteLine("3. Add student to course");
                Console.WriteLine("4. Add homework for student and course");
                Console.WriteLine("5. Delete homework with content");
                Console.WriteLine("6. Print all students enrolled in courses");
                Console.WriteLine("7. Add student to exercise");
                Console.WriteLine("8. Print all homeworks");
                Console.WriteLine("9. Print all students");
                Console.Write("Choice: ");

                var ans = Console.ReadLine();

                if (int.TryParse(ans, out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            return;

                        case 1:
                            Console.Write("Enter new student first name: ");
                            studentName = Console.ReadLine();
                            Console.Write("Enter new student last name: ");
                            studentLastName = Console.ReadLine();
                            CreateStudent(db, studentName, studentLastName);
                            break;

                        case 2:
                            Console.Write("Enter existing student first name: ");
                            DeleteStudent(db, Console.ReadLine());
                            break;

                        case 3:
                            Console.Write("Enter course name: ");
                            courseName = Console.ReadLine();
                            Console.Write("Enter student name: ");
                            studentName = Console.ReadLine();
                            AddStudentToCourse(db, studentName, courseName);
                            break;

                        case 4:
                            Console.Write("Enter course name: ");
                            courseName = Console.ReadLine();
                            Console.Write("Enter student name: ");
                            studentName = Console.ReadLine();
                            Console.Write("Enter new homework's content: ");
                            hwConent = Console.ReadLine();
                            AddHomeworkToStudentWithCourse(db, studentName, courseName, hwConent);
                            break;

                        case 5:
                            Console.WriteLine("Enter homework content: ");
                            hwConent = Console.ReadLine();
                            DeleteHomeworkWithContent(db, hwConent);
                            break;

                        case 6:
                            PrintStudentsInCourses(db);
                            break;

                        case 7:
                            Console.Write("Enter existing student name: ");
                            studentName = Console.ReadLine();
                            Console.Write("Enter existing exercise name: ");
                            exerciseName = Console.ReadLine();
                            AddStudentToExercise(db, studentName, exerciseName);
                            break;

                        case 9:
                            PrintAllStudents(db);
                            break;

                        case 8:
                            PrintAllHomeworks(db);
                            break;

                        default:
                            Console.Write("Wrong choice, back to main menu.");
                            loop = false;
                            break;
                    }
                }
            } while (loop);
        }

        private static void PrintAllHomeworks(IStudentSystemData db)
        {
            try
            {
                var students = db.Students.All().ToList();
                foreach (var student in students)
                {
                    Console.WriteLine("Homeworks of " + student.FirstName + " " + student.LastName + " :");
                    foreach (var hw in student.Homeworks)
                    {
                        Console.WriteLine("Course: " + hw.Course.Name + ", homework: " + hw.HomeworkContent);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No enrties found..." + e.ToString());
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void CreateStudent(IStudentSystemData db, string studentName, string studentLastName)
        {
            int age = rnd.Next(6, 18);

            var student = new Student { Age = age, FirstName = studentName, LastName = studentLastName, SSN = "8" + rnd.Next(000000000, 999999999).ToString(), StudentNumber = rnd.Next(100000000, 199999999) };
            db.Students.Add(student);
            db.SaveChanges();
        }

        private static void DeleteStudent(IStudentSystemData db, string studentName)
        {
            var student = db.Students.SearchFor(s => s.FirstName == studentName).FirstOrDefault();
            if (student != null)
            {
                foreach (StudentCourse sc in student.StudentsCourses.ToList()) db.StudentCourses.Delete(sc);

                foreach (Homework h in student.Homeworks.ToList()) db.Homeworks.Delete(h);

                db.Students.Delete(student);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Student not found!");
            }
        }

        private static void AddNewCourseToStudent(IStudentSystemData db, string studentName, string courseName)
        {
            var student = db.Students.SearchFor(s => s.FirstName == studentName).FirstOrDefault();
            Console.WriteLine("Found studnt with id: " + student.PersonId);
            var course = new Course { Description = "Generated", Name = courseName, Materials = "Some materials" };
            db.Courses.Add(course);
            db.SaveChanges();
            // var studentCourse = new StudentCourse { Course = course, Student = student };
            Random rnd = new Random();
            var studentCourse = new StudentCourse { CourseId = course.CourseID, Course = course, StudentId = student.PersonId, Student = student, Result = rnd.Next(0, 40) };
            db.StudentCourses.Add(studentCourse);
            db.SaveChanges();
        }

        public static void AddStudentToCourse(IStudentSystemData db, string studentName, string courseName)
        {
            var student = db.Students.SearchFor(s => s.FirstName == studentName).FirstOrDefault();
            var course = db.Courses.SearchFor(c => c.Name == courseName).FirstOrDefault();

            var studentCourse = new StudentCourse { CourseId = course.CourseID, Course = course, StudentId = student.PersonId, Student = student, Result = 0 };
            db.StudentCourses.Add(studentCourse);
            db.SaveChanges();
            Console.WriteLine("Student " + studentName + " added to course " + courseName);
        }

        private static void AddHomeworkToStudentWithCourse(IStudentSystemData db, string studentName, string courseName, string homeworkContent)
        {
            var student = db.Students.SearchFor(s => s.FirstName == studentName).FirstOrDefault();
            var course = db.Courses.SearchFor(c => c.Name == courseName).FirstOrDefault();
            var homework = new Homework
            {
                Course = course,
                Student = student,
                HomeworkContent = homeworkContent,
                TimeSent = DateTime.Now
            };
            student.Homeworks.Add(homework);
            db.SaveChanges();
        }

        private static void DeleteHomeworkWithContent(IStudentSystemData db, string homeworkContent)
        {
            var homework = db.Homeworks.SearchFor(h => h.HomeworkContent == homeworkContent).FirstOrDefault();
            db.Homeworks.Delete(homework);
            db.SaveChanges();
        }

        private static void PrintStudentsInCourses(IStudentSystemData db)
        {
            try
            {
                var sc = db.StudentCourses.All().ToList();
                foreach (var s in sc)
                {
                    Console.WriteLine("Student " + s.Student.FirstName + " is enrolled in course " + s.Course.Name + " with result " + s.Result);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No enrties found...");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void AddNewExerciseToCourse(IStudentSystemData db, string courseName, string exerciseName)
        {
            var course = db.Courses.SearchFor(c => c.Name == courseName).FirstOrDefault();
            var exercise = new Exercise { Course = course, ExerciseName = exerciseName, ExerciseContent = "Exercise on " + course.Name };
            db.Exercises.Add(exercise);
            db.SaveChanges();
        }

        private static void AddStudentToExercise(IStudentSystemData db, string studentName, string exerciseName)
        {
            var student = db.Students.SearchFor(s => s.FirstName == studentName).FirstOrDefault();
            var exercise = db.Exercises.SearchFor(e => e.ExerciseName == exerciseName).FirstOrDefault();
            //exercise.Students.Add(student);
            student.Exercises.Add(exercise);
            db.SaveChanges();
        }

        private static void PrintAllStudents(IStudentSystemData db)
        {
            try
            {
                var students = db.Students.All();
                foreach (var student in students)
                {
                    Console.WriteLine("Full name: " + student.FirstName + " " + student.LastName + " student number: " + student.StudentNumber);
                    Console.WriteLine("Age: " + student.Age + " SSN: " + student.SSN);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No enrties found...");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}