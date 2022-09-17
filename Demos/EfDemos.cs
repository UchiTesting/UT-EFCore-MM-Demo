using EF_MM.Models;
using EF_MM.Persistence;

using Microsoft.EntityFrameworkCore;

namespace EF_MM.Demos;

public class EfDemos
{
    private Course cSharp;
    private Course java;
    private Course ef;
    private Course hibernate;
    private Course projectManagement;

    private Student peter;
    private Student annie;
    private Student paul;
    private Student suzie;
    private Student jack;

    MyDbContext db;

    public EfDemos()
    {
        db = new MyDbContext();

        cSharp = db.Courses
            .Include(s => s.Students)
            .Single(c => c.Title.Equals("C#"));

        java = db.Courses
            .Include(s => s.Students)
            .Single(c => c.Title.Equals("Java"));
        ef = db.Courses.Include(s => s.Students).Single(c => c.Title.Equals("Entity Framework"));
        hibernate = db.Courses.Include(s => s.Students).Single(c => c.Title.Equals("Hibernate"));
        projectManagement = InitProjectManagementcourse(db);

        peter = db.Students
            .Include(c => c.Courses)
            .Single(s => s.Name.Equals("Peter"));

        annie = db.Students
            .Include(c => c.Courses)
            .Single(s => s.Name.Equals("Annie"));

        paul = db.Students
            .Include(c => c.Courses)
            .Single(s => s.Name.Equals("Paul"));

        suzie = db.Students
            .Include(c => c.Courses)
            .Single(s => s.Name.Equals("Suzie"));

        jack = db.Students
            .Include(c => c.Courses)
            .Single(s => s.Name.Equals("Jack"));
    }

    ~EfDemos()
    {
        db.Dispose();
    }

    // This one is modified in the expected scenario and needs extra checks upon start.
    private Course InitProjectManagementcourse(DbContext dbContext)
    {
        Course? temporaryCourse;
        temporaryCourse = db.Courses.Include(s => s.Students)
            .SingleOrDefault(c => c.Title.Equals("Project Management 101"));

        if (temporaryCourse is null)
        {
            temporaryCourse = db.Courses.Include(s => s.Students)
                .SingleOrDefault(c => c.Title.Equals("Project Management 201"));

            temporaryCourse.Title = "Project Management 101";
            db.SaveChanges();
        }

        if (temporaryCourse is null)
            throw new Exception("Project Management course data is somewhat broken. Check it and try again.");

        return temporaryCourse;
    }

    public void AddDotNetCoursesToAnnie()
    {
        Student annie = db.Students.Single(s => s.Name == "Annie");

        annie.Courses.Add(cSharp);
        annie.Courses.Add(projectManagement);
        annie.Courses.Add(ef);

        db.SaveChanges();

        Console.WriteLine($"Added .NET courses to {annie.Name} aged {annie.Age}");
    }

    internal void DisplayAnniesCourses()
    {
        DisplayStudentCourses(annie);
    }

    internal void DisplayStudentCourses(Student s)
    {
        Console.WriteLine($"\n=== {s.Name} Courses ===\n");

        // Uses in-memory data
        //foreach (Course c in s.Courses)
        //    Console.WriteLine(c.Title);

        // Let's interact with the DB Instead
        List<Course> courses = db.Courses
            .Where(c => c.Students.Contains(s))
            .ToList();

        courses.ForEach(c => Console.WriteLine(c.Title));

        Console.WriteLine($"\n=== END ===\n");
    }

    internal void AddHibernateThenRemoveIt()
    {
        // One impactful action on the DB : Adding data
        annie.Courses.Add(hibernate);
        db.SaveChanges();

        // Intermediary display of the courses. Hibernate is there.
        DisplayAnniesCourses();

        // Second impactful action on the DB : Removing data
        annie.Courses.Remove(hibernate);
        db.SaveChanges();
    }

    internal void UpdatePmCourseTo201()
    {
        projectManagement.Title = "Project Management 201";
        db.SaveChanges();
    }

    internal void UpdatePmCourseTo101()
    {
        projectManagement.Title = "Project Management 101";
        db.SaveChanges();
    }

    internal void CreateDevOpsCourseThenAddToAnnieThenDeleteDevOpsCourse()
    {
        // Add a new DevOps course.
        string courseTitle = "DevOps: From Hero to absolute Zero (as it should be)";
        Course DevOpsCourse = new Course { Title = courseTitle };

        db.Courses.Add(DevOpsCourse);
        db.SaveChanges();

        var devOpsCourseFromDb = db.Courses.Single(c => c.Title.Equals(courseTitle));

        Console.WriteLine($"Course from DB => {devOpsCourseFromDb.Title}");

        if (devOpsCourseFromDb is null)
            throw new Exception("Could not load DevOps course. Deal with it.");

        // Add DevOps to Annie.
        annie.Courses.Add(devOpsCourseFromDb);
        db.SaveChanges();

        DisplayAnniesCourses();

        // Delete the newly created DevOps course. Cascading will remove the link with Annie.
        db.Courses.Remove(devOpsCourseFromDb);
        db.SaveChanges();
    }
}
