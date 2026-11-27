using LectureManagementSystem.Models;

public class Programming : Lecture
{
    public string ProgrammingLanguage { get; set; } //  string subljectCode = "PR"; // A unique subject code for programming lectures

    public Programming(string subjectName, string lecturerName, int lectureHours, string programmingLanguage) 
        : base("PR", subjectName, lecturerName, lectureHours)
    {
        ProgrammingLanguage = programmingLanguage;
        Console.WriteLine($"Programming lecture created: {SubjectCode} - {SubjectName} in {ProgrammingLanguage}");
    }

    // Overriding the GetDetails method
    public override string GetDetails()
    {
        return base.GetDetails() + $" | Language: {ProgrammingLanguage} (Type: Programming)";
    }
}