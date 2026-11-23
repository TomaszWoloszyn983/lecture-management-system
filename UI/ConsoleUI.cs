using LectureManagementSystem.Models;

class ConsoleUI(LectureManager lectureManager)
{
    private LectureManager _lectureManager = lectureManager;

    public void run()
    {
        bool running = true;
        Console.Clear();
        while (running)
        {
            Console.WriteLine("\n-----------------------------------"
                            +"\n==== LECTURE MANAGEMENT SYSTEM ===="
                            +"\n-----------------------------------"
                            +"\n\nSELECT AN OPTION:"
                            +"\n\t1 to Display All Lectures"
                            +"\n\t2 to Add Lecture"
                            +"\n\t3 to Update Lecture"
                            +"\n\t4 to Delete Lecture"
                            +"\n\t0 to Exit");
            String? userChoice = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine("You selected option: " + userChoice); // Move to the next line after user input
            if (userChoice == "0")
            {
                running = false;
            }

            switch (userChoice)
            {
                case "1":
                    Console.WriteLine($"\nLectures in the list: {lectureManager.GetTotalLectures()}"
                                +$"\nTotal lectures created: {Lecture.TotalLecturesCreated}");
                    lectureManager.DisplayAllLectures();
                    break;
                case "2":
                    Lecture newLecture = getNewLecture();
                    if (newLecture != null)
                    {
                        _lectureManager.AddLecture(newLecture);
                    }
                    break;
                case "3":
                    Console.WriteLine("Enter the lecture code to update:");
                    // Lecture updatedLecture = getNewLecture();
                    // if (updatedLecture != null)
                    // {
                    //     _lectureManager.UpdateLecture(updateCode, updatedLecture);
                    // }
                    break;
                case "4":
                    Console.WriteLine("Enter the lecture code to delete:");
                    string deleteCode = Console.ReadLine().ToUpper();
                    Lecture lectureToDelete = _lectureManager.GetLectureByCode(deleteCode);
                    if (lectureToDelete != null)
                    {
                        _lectureManager.DeleteLecture(lectureToDelete);
                    }
                    else
                    {
                        Console.WriteLine("Lecture not found.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    
    public Lecture getNewLecture()
    {   Object[] inputParts = new Object[5];
        Console.WriteLine("\n\nEnter lecture code"
                         +"\n(PR - Programming, MY - Mycology, AS - Astronomy):");
        string lectureCode = Console.ReadLine().ToUpper();
        switch (lectureCode)
        {
            case "PR":
                inputParts[0] = lectureCode;
                // Console.WriteLine("Enter subject name");
                inputParts[1] = GetStringInput("Enter subject name:");
                // Console.WriteLine("Enter lecturer name");
                inputParts[2] = GetStringInput("Enter lecturer name:");
                // Console.WriteLine("Enter lecture hours");
                inputParts[3] = GetNumericInput("Enter lecture hours:");
                // Console.WriteLine("Enter programming language");
                inputParts[4] = GetStringInput("Enter programming language:");
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

    private static string GetStringInput(string prompt)
    {
        Console.WriteLine(prompt);
        string input;
        do
        {
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                Console.WriteLine("Input cannot be empty. Please try again.");
        } while (string.IsNullOrWhiteSpace(input));
        return input;
    }

    private static int GetNumericInput(string prompt)
    {
        Console.WriteLine(prompt);
        int input;
        bool isValid;

        do
        {
            string? rawInput = Console.ReadLine();
            isValid = int.TryParse(rawInput?.Trim(), out input) && input > 0;

            if (!isValid)
            {
                Console.WriteLine("Input must be a positive integer. Please try again.");
            }
        } while (!isValid);

        return input;
    }
}