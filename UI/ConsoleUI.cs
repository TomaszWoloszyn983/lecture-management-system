using LectureManagementSystem.Models;
// using LectureManagementSystem.Services;

class ConsoleUI()
{
    private LectureManager _lectureManager = new LectureManager();

    /**
    * The main loop of the console application.
    * It displays the menu, takes user input, 
    * and calls the appropriate methods based on the user's choice.
    */
    public void run()
    {
        bool running = true;
        Console.Clear();
        while (running)
        {
            PrintMenu();
            String? userChoice = Console.ReadKey(intercept: true).KeyChar.ToString();

            switch (userChoice)
            {
                case "1":
                    // Display all lectures
                    DisplayAllLecturesOption();
                    break;

                case "2":
                    // Add lecture
                    _lectureManager.AddLecture(getNewLecture());
                    break;

                case "3":
                    // Update lecture
                    UpdateLectureOption();
                    break;

                case "4":
                    // Delete lecture
                    DeleteLectureOption();
                    break;

                case "0":
                    // Exit application
                    running = false;
                    _lectureManager.SaveToFile();
                    Console.WriteLine("\nSaving changes and exiting application.");
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
                    break;
            }
        }
    }
 

    /**
    * Prints the main menu options to the console.
    */
    private static void PrintMenu()
    {
        Console.WriteLine("\n-------------------------------------------------------------------"
                        +"\n==================== LECTURE MANAGEMENT SYSTEM ===================="
                        +"\n-------------------------------------------------------------------"
                        +"\n\n\t\t\tSELECT AN OPTION:"
                        +"\n\n1 to Display All Lectures"
                        +"\n2 to Add Lecture"
                        +"\n3 to Update Lecture"
                        +"\n4 to Delete Lecture"
                        +"\n0 to Exit");
        Console.Write("\n> ");
    }


    /**
    * Displays all lectures in the lecture manager.
    */
    private void DisplayAllLecturesOption()
    {
        Console.WriteLine("\n==================== ALL LECTURES "
                            +$"({_lectureManager.GetTotalLectures()}) ====================");
        if (_lectureManager.GetTotalLectures() > 0)
        {
            Console.WriteLine($"{"CODE",-8} | {"TYPE",-11} | {"HOURS",5} | {"SUBJECT",-28} | {"LECTURER",-22} | {"DETAIL"}");
            Console.WriteLine(new string('-', 108));

            foreach (var lecture in _lectureManager.Lectures)
            {
                Console.WriteLine($"{lecture.SubjectCode,-8} | {lecture.GetType().Name,-11} | "
                                 +$"{lecture.LectureHours,5} | {lecture.SubjectName,-28} | "
                                 +$"{lecture.LecturerName,-22} | {lecture switch
                                    {
                                        Programming p => p.ProgrammingLanguage,
                                        Astronomy a => a.CelestialBody,
                                        Mycology m => m.FungiType,
                                        _ => ""
                                    }}"
                                );
            }
        }
        else
        {
            Console.WriteLine("No lectures available yet.");
        }
    }
    
    
    

    /**
    * Prompts the user to enter details for a new lecture.  
    * Switches based on the lecture code 
    * to create the appropriate lecture type (Programming, Mycology, or Astronomy).
    * The function returns the newly created lecture object or null if the lecture code is invalid.
    */
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


    /*
    * The update process is interactive. It prompts the user for each detail.
    * If the user presses Enter without typing anything, the current value is retained.
    * This allows for selective updating of lecture details.
    */      
    public void UpdateLectureOption()
    {
        string lectureCode = ReadStringInput("Enter the lecture code to update:");
        Lecture lectureToUpdate = _lectureManager.GetLectureByCode(lectureCode);
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
            
            // Update the lecture with the new information, 
            _lectureManager.UpdateLecture(lectureCode, 
                                            string.IsNullOrWhiteSpace(newSubjectName) ? null : newSubjectName,
                                            string.IsNullOrWhiteSpace(newLecturerName) ? null : newLecturerName,
                                            newLectureHours,
                                            string.IsNullOrWhiteSpace(newSpecificDetail) ? null : newSpecificDetail);
        }else
        {
            Console.WriteLine("\nLecture not found.");
        }
    }


    /**
    * Deletes a lecture based on the lecture code provided by the user. 
    * It first checks if the lecture exists,
    * then asks for confirmation before deletion.
    */
    public void DeleteLectureOption()
    {
     string deleteCode = ReadStringInput("Enter the lecture code to delete:");
        Lecture lectureToDelete = _lectureManager.GetLectureByCode(deleteCode);
        if (lectureToDelete != null)
        {
            Console.WriteLine($"\nAre you sure you want to delete the lecture: \n{lectureToDelete.GetDetails()}?");
            string confirmation = ReadStringInput("\n\nType 'yes' to confirm deletion or 'no' to cancel:");
            if (confirmation.ToUpper() == "YES")
            {
                _lectureManager.DeleteLecture(lectureToDelete);
            }else if (confirmation.ToUpper() == "NO")
            {
                Console.WriteLine("\nDeletion cancelled.");
            }else
            {
                Console.WriteLine("\nIncorrect confirmation. Deletion cancelled.");
            }
        }
        else
        {
            Console.WriteLine("\nLecture not found.");
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


    /**
     * A method to get a positive integer input from the user, allowing for a default value.
     * If the user enters an empty string, the current value is returned.
     */
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