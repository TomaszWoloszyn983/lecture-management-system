namespace LectureManagementSystem.Models;

using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Programming), typeDiscriminator: "programming")]
[JsonDerivedType(typeof(Astronomy), typeDiscriminator: "astronomy")]
[JsonDerivedType(typeof(Mycology), typeDiscriminator: "mycology")]
public abstract class Lecture
{
    // A static counter to keep track of the total number of lectures created
    private static int _totalLecturesCreated = 0;
    public static int TotalLecturesCreated => _totalLecturesCreated;

    // Properties common to all lectures
    public string SubjectCode { get; set; }
    public string SubjectName { get; set; }
    public string LecturerName { get; set; }
    public int LectureHours { get; set; }

    // Base constructor
    public Lecture(string subjectCode, string subjectName, 
                      string lecturerName, int lectureHours)
    {
        SubjectCode = GenerateSubjectCode(subjectCode); // Generate a unique subject code
        SubjectName = subjectName;
        LecturerName = lecturerName;
        LectureHours = lectureHours;
        
        Console.WriteLine(
                $"\nLecture created: {SubjectCode} - {SubjectName}."
                +$" Total lectures created: {_totalLecturesCreated}"
        );
    }


    /* 
        A virtual method to get lecture details,
        which can be overridden by derived classes
    */
    public virtual string GetDetails()
    {
        return $"Code: {SubjectCode} | Name: {SubjectName}"
              +$" | Lecturer: {LecturerName} | Hours: {LectureHours}h";
    }


    /**
        A private method to generate a unique subject code based 
        on the provided lecture code and the total number of lectures created.

        Increases the total lectures created counter
        and formats it as a four-digit number.
    */  
    private string GenerateSubjectCode(String LectureCode) {
        return LectureCode+$"_{++_totalLecturesCreated:D4}"; // Example: CS101-001
    }


    /**
        A method that allows the lecture counter to be set to a specific value.
        Used to initialize the counter with a value retrieved from a file.
    */
    public static void SetLectureCounter(int countValue)
    {
        _totalLecturesCreated = countValue;
    }

}