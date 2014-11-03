namespace CodeFirst.Data
{
    using CodeFirst.Data.Migrations;
    using CodeFirst.Models;
    using System.Data.Entity;

    public class StudentSystemDbContext : DbContext, IStudentSystemDbContext
    {
        public StudentSystemDbContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());
            this.Database.Connection.ConnectionString = connectionString;
        }

        public IDbSet<Person> People { get; set; }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Lecturer> Lecturers { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }

        public IDbSet<Exercise> Exercises { get; set; }

        public IDbSet<StudentCourse> StudentCourses { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}