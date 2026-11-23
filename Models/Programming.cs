using LectureManagementSystem.Models;

public class Programming : Lecture
{
    public string ProgrammingLanguage { get; set; } //  string subljectCode = "PR"; // A unique subject code for programming lectures
    private static int _totalProgrammingLecturesCreated = 0; //Conmsider if you really need this static counter for Programming lectures
    string uniqueSubjectCode = "PR"; // A unique subject code for programming lectures

    public Programming(string subjectName, string lecturerName, int lectureHours, string programmingLanguage) 
        : base("PR", subjectName, lecturerName, lectureHours)
    {
        ProgrammingLanguage = programmingLanguage;
        Console.WriteLine($"Programming lecture created: {SubjectCode} - {SubjectName} in {ProgrammingLanguage}");
        _totalProgrammingLecturesCreated++;
    }

    // Overriding the GetDetails method
    public override string GetDetails()
    {
        return base.GetDetails() + $" | Language: {ProgrammingLanguage} (Type: Programming)";
    }
}