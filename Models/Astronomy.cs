using LectureManagementSystem.Models;

public class Astronomy : Lecture
{
    public string CelestialBody { get; set; } // A unique subject code for astronomy lectures
    private static int _totalAstronomyLecturesCreated = 0; // Consider if you really need this static counter for Astronomy lectures

    public Astronomy(string subjectName, string lecturerName, int lectureHours, string CelestialBody) 
        : base("AS", subjectName, lecturerName, lectureHours)
    {
        this.CelestialBody = CelestialBody;
        Console.WriteLine($"Astronomy lecture created: {SubjectCode} - {SubjectName} on {CelestialBody}");
        _totalAstronomyLecturesCreated++;
    }

    // Overriding the GetDetails method
    public override string GetDetails()
    {
        return base.GetDetails() + $" | Celestial Body: {CelestialBody} (Type: Astronomy)";
    }
}