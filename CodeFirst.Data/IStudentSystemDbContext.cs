namespace CodeFirst.Data
{
    using CodeFirst.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IStudentSystemDbContext
    {
        IDbSet<Person> People { get; set; }
        IDbSet<Student> Students { get; set; }
        IDbSet<Lecturer> Lecturers { get; set; }
        IDbSet<Course> Courses { get; set; }
        IDbSet<Homework> Homeworks { get; set; }
        IDbSet<Exercise> Exercises { get; set; }
        IDbSet<StudentCourse> StudentCourses { get; set; }
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}