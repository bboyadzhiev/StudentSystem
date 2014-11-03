namespace CodeFirst.Data
{
    using CodeFirst.Models;
    using StudentSystem.Data.Repositories;
    using System;
    using System.Collections.Generic;

    public class StudentSystemData : IStudentSystemData
    {
        private readonly IStudentSystemDbContext context;
        private readonly IDictionary<Type, object> repositories;

        //public StudentSystemData()
        //    : this(new StudentSystemDbContext())
        //{
        //}
        //
        //public StudentSystemData(string connectionString)
        //    : this(new StudentSystemDbContext(connectionString))
        //{
        //}

        public StudentSystemData(IStudentSystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Person> People
        {
            get { return this.GetRepository<Person>(); }
        }

        public IRepository<Student> Students
        {
            get { return this.GetRepository<Student>(); }
        }

        public IRepository<Lecturer> Lecturers
        {
            get { return this.GetRepository<Lecturer>(); }
        }

        public IRepository<Course> Courses
        {
            get { return this.GetRepository<Course>(); }
        }

        public IRepository<Homework> Homeworks
        {
            get { return this.GetRepository<Homework>(); }
        }

        public IRepository<Exercise> Exercises
        {
            get { return this.GetRepository<Exercise>(); }
        }

        public IRepository<StudentCourse> StudentCourses
        {
            get { return this.GetRepository<StudentCourse>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EFRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}