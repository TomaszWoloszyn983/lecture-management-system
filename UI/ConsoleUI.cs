using LectureManagementSystem.Models;

class ConsoleUI(LectureManager lectureManager)
{
    private LectureManager _lectureManager = lectureManager;

    public void run()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== LECTURE MANAGEMENT SYSTEM ==="
                            +"\nSELECT AN OPTION:"
                            +"\n1 to Display All Lectures"
                            +"\n2 to Add Lecture"
                            +"\n3 to Update Lecture"
                            +"\n4 to Delete Lecture"
                            +"\n0 to Exit");
            String? userChoice = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("You selected option: " + userChoice); // Move to the next line after user input
            if (userChoice == "0")
            {
                running = false;
            }
        }
    }
    
    public Lecture getNewLecture()
    {   Object[] inputParts = new Object[5];
        Console.WriteLine("\n\nEnter lecture code");
        string lectureCode = Console.ReadLine().ToUpper();
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
                Console.WriteLine($"Invalid lecture code: {lectureCode}");
                return null;
        }

    }
}