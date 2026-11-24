using LectureManagementSystem.Models;

public class Astronomy : Lecture
{
    public string Galaxy { get; set; } // A unique subject code for astronomy lectures
    private static int _totalAstronomyLecturesCreated = 0; // Consider if you really need this static counter for Astronomy lectures

    public Astronomy(string subjectName, string lecturerName, int lectureHours, string Galaxy) 
        : base("AS", subjectName, lecturerName, lectureHours)
    {
        this.Galaxy = Galaxy;
        Console.WriteLine($"Astronomy lecture created: {SubjectCode} - {SubjectName} on {Galaxy}");
        _totalAstronomyLecturesCreated++;
    }

    // Overriding the GetDetails method
    public override string GetDetails()
    {
        return base.GetDetails() + $" | Celestial Body: {Galaxy} (Type: Astronomy)";
    }
}