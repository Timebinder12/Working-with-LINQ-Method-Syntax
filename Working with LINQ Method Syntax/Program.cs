using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{

    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double Tuition { get; set; }
    }
    public class StudentClubs
    {
        public int StudentID { get; set; }
        public string ClubName { get; set; }
    }
    public class StudentGPA
    {
        public int StudentID { get; set; }
        public double GPA { get; set; }
    }

    public static void Main()
    {

        // Student collection
        IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 1, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 2, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 3, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 5, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
        };
        // Student GPA Collection
        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
        // Club collection
        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };
        //Grouping by GPA
        var allStudents = studentGPAList.GroupBy(s => s.GPA);

        Console.WriteLine("Grouping by GPA and displaying the student's ID:");
        foreach (var student in allStudents)
            foreach (StudentGPA students in student)
                Console.WriteLine($"Student ID: {students.StudentID}");
        
        Console.WriteLine();

        //Ordering and grouping by clubname and displaying student ID
        var clubStudents = studentClubList.OrderBy(s => s.ClubName).GroupBy(s => s.ClubName);

        Console.WriteLine("Sorting by Club and then grouping by Club and displaying student's IDs");
        foreach(var student in clubStudents)
            foreach (StudentClubs students in student)
                Console.WriteLine($"Student ID: {students.StudentID}");
        
        Console.WriteLine();

        //Displaying the number of students who have gpa between 2.5 and 4.0
        var countGPA = studentGPAList.Where(s => s.GPA >= 2.5).Where(s => s.GPA <= 4.0).Count();
        Console.WriteLine($"The number of students who have a GPA between 2.5 and 4.0 are {countGPA}");
        Console.WriteLine();

        //Displaying an average of all student's tuition
        var avgTuition = studentList.Average(s => s.Tuition);
        Console.WriteLine($"The average of all student's tuition is {avgTuition}");
        Console.WriteLine();

        //Displaying the information from the person with the highest tuition
        var highestTution = studentList.Max(s => s.Tuition);

        foreach (var student in studentList)
            if (student.Tuition == highestTution)
                Console.WriteLine($"The person with the highest tuition:\nName: {student.StudentName} Major: {student.Major} Tuition: {student.Tuition}");
        Console.WriteLine();

        //Performing a join on two lists and displaying name, major, and gpa
        var innerJoin = studentList.Join(studentGPAList,
                                         student => student.StudentID,
                                         GPA => GPA.StudentID,
                                         (student, GPA) => new
                                         {
                                             StudentName = student.StudentName,
                                             Major = student.Major,
                                             GPA = GPA.GPA,
                                         });
        Console.WriteLine("Students names, major, and GPA:");
        Console.WriteLine();

        foreach (var s in innerJoin)
            Console.WriteLine($"Name: {s.StudentName} Major: {s.Major} GPA: {s.GPA}");
        Console.WriteLine();

        //Peforming a join on two lists and displaying students who are in the game club
        var notanotherinnerjoin = studentList.Join(studentClubList.Where(s => s.ClubName == "Game"),
                                                   student => student.StudentID,
                                                   club => club.StudentID,
                                                   (student, club) => new
                                                   {
                                                       studentName = student.StudentName,
                                                   });
        Console.WriteLine("Students who are in the game club:");
        Console.WriteLine();

        foreach (var s in notanotherinnerjoin)
            Console.WriteLine($"Name: {s.studentName}");


    }
}