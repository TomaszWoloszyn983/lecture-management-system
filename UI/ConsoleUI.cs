using LectureManagementSystem.Models;

class ConsoleUI()
{
    // private LectureManager _lectureManager = lectureManager;

    // public void DisplayAllLectures()
    // {
    //     var lectures = _lectureManager.DisplayAllLectures();
    //     if (lectures.Count == 0)
    //     {
    //         Console.WriteLine("\nNo lectures available.");
    //         return;
    //     }

    //     Console.WriteLine("\nList of Lectures:");
    //     for (int i = 0; i < lectures.Count; i++)
    //     {
    //         Console.WriteLine($"{i + 1}. {lectures[i].GetDetails()}");
    //     }
    // }

    public Lecture getUserInput()
    {   Object[] inputParts = new Object[5];
        Console.WriteLine("\n\nEnter lecture code");
        string lectureCode = Console.ReadLine();
        switch (lectureCode)
        {
            case "PR":
                inputParts[0] = lectureCode;
                Console.WriteLine("Enter subject name");
                inputParts[1] = Console.ReadLine();
                Console.WriteLine("Enter lecturer name");
                inputParts[2] = Console.ReadLine();
                Console.WriteLine("Enter lecture hours");
                inputParts[3] = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter programming language");
                inputParts[4] = Console.ReadLine();
                return new Programming(inputParts[1].ToString(), inputParts[2].ToString(), (int)inputParts[3], inputParts[4].ToString());
            case "MY":
                inputParts[0] = lectureCode;
                Console.WriteLine("Enter subject name");
                inputParts[1] = Console.ReadLine();
                Console.WriteLine("Enter lecturer name");
                inputParts[2] = Console.ReadLine();
                Console.WriteLine("Enter lecture hours");
                inputParts[3] = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter database type");
                inputParts[4] = Console.ReadLine();
                return new Mycology(inputParts[1].ToString(), inputParts[2].ToString(), (int)inputParts[3], inputParts[4].ToString());
            case "AS":
                inputParts[0] = lectureCode;
                Console.WriteLine("Enter subject name");
                inputParts[1] = Console.ReadLine();
                Console.WriteLine("Enter lecturer name");
                inputParts[2] = Console.ReadLine();
                Console.WriteLine("Enter lecture hours");
                inputParts[3] = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter network type");
                inputParts[4] = Console.ReadLine();
                return new Astronomy(inputParts[1].ToString(), inputParts[2].ToString(), (int)inputParts[3], inputParts[4].ToString());
            default:
                throw new ArgumentException($"Invalid lecture code: {lectureCode}");
        }

    }
}