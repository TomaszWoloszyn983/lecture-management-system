using LectureManagementSystem.Models;

class Program{
    static void Main(string[] args){

        Console.WriteLine("\n\n\nWelcome to the Lecture Management System!\n\n\t\t!!!\n\n");
        // think if you want to load lectures from a file here, if you implement file handling
        LectureManager lectureManager = new();

               Console.WriteLine($"\n\nLectures in the list: {lectureManager.GetTotalLectures()}"
                         +$"\nTotal lectures created: {Lecture.TotalLecturesCreated}");
       
        new ConsoleUI(lectureManager).run();
        
        Console.WriteLine("Exiting the program...");

        
    }
}

// LectureManagementSystem/
// ├── Models/
// │   ├── Lecture.cs          (Klasa bazowa - abstrakcyjna)
// │   ├── Programming.cs      (Klasa pochodna)
// │   ├── Database.cs         (Klasa pochodna)
// │   └── Networking.cs       (Klasa pochodna)
// ├── Services/
// │   ├── LectureManager.cs   (Logika biznesowa, kolekcje, statystyki i pliki)
// │   └── FileHandler.cs      (Opcjonalnie: obsługa zapisu/odczytu pliku)
// ├── UI/
// │   └── ConsoleUI.cs        (Menu użytkownika i obsługa konsoli)
// └── Program.cs              (Punkt startowy)