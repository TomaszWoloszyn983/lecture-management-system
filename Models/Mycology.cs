
using LectureManagementSystem.Models;

// Intruduction to Mycology: The study of fungi, including their genetic and biochemical properties, their taxonomy, and their use to humans as a source for tinder, medicine, food, and entheogens, as well as their dangers, such as toxicity or infection.
// Edible and inedible mushrooms
// Symbiotic relationships:

public class Mycology : Lecture
{
    public string FungiType { get; set; } // Unikalna cecha dla mykologii
    private static int _totalMycologyLecturesCreated = 0; // Consider if you really need this static counter for Mycology lectures

    public Mycology(string subjectName, string lecturerName, int lectureHours, string fungiType) 
        : base("MY", subjectName, lecturerName, lectureHours)
    {
        FungiType = fungiType;
        Console.WriteLine($"Mycology lecture created: {SubjectCode} - {SubjectName} on {FungiType}");
        _totalMycologyLecturesCreated++;
    }

    // Nadpisanie metody (Polimorfizm)
    public override string GetDetails()
    {
        return base.GetDetails() + $" | Fungi Type: {FungiType} (Type: Mycology)";
    }
}