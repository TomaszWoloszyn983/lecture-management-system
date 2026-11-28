using LectureManagementSystem.Models;

public class Astronomy : Lecture
{
    public string CelestialBody { get; set; }

    public Astronomy(string subjectName, string lecturerName, int lectureHours, string CelestialBody) 
        : base("AS", subjectName, lecturerName, lectureHours)
    {
        this.CelestialBody = CelestialBody;
        Console.WriteLine($"Astronomy lecture created: {SubjectCode} - {SubjectName} on {CelestialBody}");
    }


    // Overriding the GetDetails method
    public override string GetDetails()
    {
        return base.GetDetails() + $" | Celestial Body: {CelestialBody} (Type: Astronomy)";
    }
}