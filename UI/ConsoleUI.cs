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
                // Display all lectures
                case "1":
                if (_lectureManager.GetTotalLectures() > 0)
                    {
                        Console.WriteLine("\nLectures in the list:");
                        _lectureManager.DisplayAllLectures();
                    }
                    else
                    {
                        Console.WriteLine("\nNo lectures available yet.");
                    }
                    break;

                // Add lecture
                case "2":
                    Lecture newLecture = getNewLecture();
                    if (newLecture != null)
                    {
                        _lectureManager.AddLecture(newLecture);
                    }
                    break;

                // Update lecture
                case "3":
                    /*
                    * The update process is interactive. It prompts the user for each detail.
                    * If the user presses Enter without typing anything, the current value is retained.
                    * This allows for selective updating of lecture details.
                    */      
                    string lectureCode = ReadStringInput("Enter the lecture code to update:");
                    Lecture lectureToUpdate = lectureManager.GetLectureByCode(lectureCode);
                    if (lectureToUpdate != null)
                    {
                        Console.WriteLine($"\nCurrent details: {lectureToUpdate.GetDetails()}");
                        string newSubjectName = ReadStringOrDefault(
                                                "Enter new subject name (or press Enter to keep current):", 
                                                lectureToUpdate.SubjectName);
                        string newLecturerName = ReadStringOrDefault(
                                                "Enter new lecturer name (or press Enter to keep current):", 
                                                lectureToUpdate.LecturerName);
                        int newLectureHours = ReadPositiveIntOrDefault(
                                                "Enter new lecture hours (or press Enter to keep current):",
                                                lectureToUpdate.LectureHours);
                        string newSpecificDetail = ReadStringOrDefault(
                                                "Enter new specific detail (or press Enter to keep current):", 
                                                lectureToUpdate switch
                                                {
                                                    Programming p => p.ProgrammingLanguage,
                                                    Astronomy a => a.CelestialBody,
                                                    Mycology m => m.FungiType,
                                                    _ => ""
                                                });
                        
                        // Update the lecture with the new details
                        _lectureManager.UpdateLecture(lectureCode, 
                                                      string.IsNullOrWhiteSpace(newSubjectName) ? null : newSubjectName,
                                                      string.IsNullOrWhiteSpace(newLecturerName) ? null : newLecturerName,
                                                      newLectureHours,
                                                      string.IsNullOrWhiteSpace(newSpecificDetail) ? null : newSpecificDetail);
                    }
                    else
                    {
                        Console.WriteLine("\nLecture not found.");
                    }
                    // Lecture updatedLecture = getNewLecture();
                    // if (updatedLecture != null)
                    // {
                    //     _lectureManager.UpdateLecture(updateCode, updatedLecture);
                    // }
                    break;
                // Delete lecture
                case "4":
                    // Console.WriteLine("Enter the lecture code to delete:");
                    string deleteCode = ReadStringInput("Enter the lecture code to delete:");
                    Lecture lectureToDelete = lectureManager.GetLectureByCode(deleteCode);
                    if (lectureToDelete != null)
                    {
                        Console.WriteLine($"\nAre you sure you want to delete the lecture: {lectureToDelete.GetDetails()}?");
                        string confirmation = ReadStringInput("Type 'yes' to confirm deletion or 'no' to cancel:");
                        if (confirmation.ToUpper() == "YES")
                        {
                            _lectureManager.DeleteLecture(lectureToDelete);
                        }
                        else if (confirmation.ToUpper() == "NO")
                        {
                            Console.WriteLine("\nDeletion cancelled.");
                        }
                        else
                        {
                            Console.WriteLine("\nIncorrect confirmation. Deletion cancelled.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nLecture not found.");
                    }
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
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
                inputParts[1] = ReadStringInput("Enter subject name:");
                inputParts[2] = ReadStringInput("Enter lecturer name:");
                inputParts[3] = ReadNumericInput("Enter lecture hours:");
                inputParts[4] = ReadStringInput("Enter programming language:");
                return new Programming(inputParts[1].ToString(), inputParts[2].ToString(), (int)inputParts[3], inputParts[4].ToString());
            case "MY":
                inputParts[0] = lectureCode;
                inputParts[1] = ReadStringInput("Enter subject name:");
                inputParts[2] = ReadStringInput("Enter lecturer name:");
                inputParts[3] = ReadNumericInput("Enter lecture hours:");
                inputParts[4] = ReadStringInput("Enter database type:");
                return new Mycology(inputParts[1].ToString(), inputParts[2].ToString(), (int)inputParts[3], inputParts[4].ToString());
            case "AS":
                inputParts[0] = lectureCode;
                inputParts[1] = ReadStringInput("Enter subject name:");
                inputParts[2] = ReadStringInput("Enter lecturer name:");
                inputParts[3] = ReadNumericInput("Enter lecture hours:");
                inputParts[4] = ReadStringInput("Enter network type:");
                return new Astronomy(inputParts[1].ToString(), inputParts[2].ToString(), (int)inputParts[3], inputParts[4].ToString());
            default:
                Console.WriteLine($"Invalid lecture code: {lectureCode}");
                return null;
        }
    }


    /**
    * A method to get a non-empty string input from the user.
    * It prompts the user until a valid non-empty string is entered.
    */
    private static string ReadStringInput(string prompt)
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

    /**
    * A method to get a string input from the user, allowing for a default value.
    * If the user enters an empty string, the current value is returned.
    */
    private static string ReadStringOrDefault(string prompt, string currentValue)
    {
        Console.Write($"{prompt} [{currentValue}]: ");
        string? input = Console.ReadLine();

        return string.IsNullOrWhiteSpace(input) ? currentValue : input.Trim();
    }


    /**
     * A method to get a positive integer input from the user.
     * It prompts the user until a valid positive integer is entered.
     */
    private static int ReadNumericInput(string prompt)
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

    private static int ReadPositiveIntOrDefault(string prompt, int currentValue)
{
    while (true)
    {
        Console.Write($"{prompt} [{currentValue}]: ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
            return currentValue;

        if (int.TryParse(input.Trim(), out int parsedValue) && parsedValue > 0)
            return parsedValue;

        Console.WriteLine("Enter a valid positive integer or press Enter to keep the current value.");
    }
}
}