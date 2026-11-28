
using LectureManagementSystem.Models;


public class Mycology : Lecture
{
    public string FungiType { get; set; } 

    public Mycology(string subjectName, string lecturerName, int lectureHours, string fungiType) 
        : base("MY", subjectName, lecturerName, lectureHours)
    {
        FungiType = fungiType;
        Console.WriteLine($"Mycology lecture created: {SubjectCode} - {SubjectName} on {FungiType}");
    }

    // Overriding the GetDetails method
    public override string GetDetails()
    {
        return base.GetDetails() + $" | Fungi Type: {FungiType} (Type: Mycology)";
    }
}