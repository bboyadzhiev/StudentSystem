namespace CodeFirst.Data
{
    using CodeFirst.Models;
    using StudentSystem.Data.Repositories;

    // Unit of Work Interface
    public interface IStudentSystemData
    {
        IRepository<Person> People { get; }
        IRepository<Student> Students { get; }
        IRepository<Lecturer> Lecturers { get; }
        IRepository<Course> Courses { get; }
        IRepository<Homework> Homeworks { get; }
        IRepository<Exercise> Exercises { get; }
        IRepository<StudentCourse> StudentCourses { get; }
        int SaveChanges();
    }
}