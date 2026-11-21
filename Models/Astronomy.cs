using LectureManagementSystem.Models;

public class Astronomy : Lecture
{
    public string CelestialBody { get; set; } // Unikalna cecha dla astronomii
    private static int _totalAstronomyLecturesCreated = 0; // Consider if you really need this static counter for Astronomy lectures

    public Astronomy(string subjectName, string lecturerName, int lectureHours, string celestialBody) 
        : base("AS", subjectName, lecturerName, lectureHours)
    {
        CelestialBody = celestialBody;
        Console.WriteLine($"Astronomy lecture created: {SubjectCode} - {SubjectName} on {CelestialBody}");
        _totalAstronomyLecturesCreated++;
    }

    // Nadpisanie metody (Polimorfizm)
    public override string GetDetails()
    {
        return base.GetDetails() + $" | Celestial Body: {CelestialBody} (Type: Astronomy)";
    }
}