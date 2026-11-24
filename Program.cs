using LectureManagementSystem.Models;

class Program{
    static void Main(string[] args){

        Console.WriteLine("\n\n\nWelcome to the Lecture Management System!\n\n\t\t!!!\n\n");
        LectureManager lectureManager = new();

        Lecture lecture1 = new Programming("Introduction to Programming", "Dr. Octavius", 40, "C#");

        lectureManager.AddLecture(lecture1);    
        lectureManager.AddLecture(new Programming("Introduction to Programming", "Dr. Parker", 40, "C#"));
        lectureManager.AddLecture(new Mycology("Introduction to Mycology", "Dr. Banner", 30, "Edible"));
        lectureManager.AddLecture(new Astronomy("Introduction to Astronomy", "Dr. Stark", 35, "Stars"));

         Console.WriteLine($"\n\nLectures in the list: {lectureManager.GetTotalLectures()}"
                         +$"\nTotal lectures created: {Lecture.TotalLecturesCreated}");
        lectureManager.DisplayAllLectures();

        lectureManager.DeleteLecture(lecture1); // This will not find the lecture because it's a different instance
        lectureManager.DeleteLecture(lecture1); // This will not find the lecture because it's a different instance
        
        Console.WriteLine($"\n\nLectures in the list: {lectureManager.GetTotalLectures()}"
                         +$"\nTotal lectures created: {Lecture.TotalLecturesCreated}");
        lectureManager.DisplayAllLectures();

        lectureManager.DeleteLectureByIndex(1); 
        lectureManager.DeleteLectureByIndex(10); 

        Console.WriteLine($"\n\nLectures in the list: {lectureManager.GetTotalLectures()}"
                    +$"\nTotal lectures created: {Lecture.TotalLecturesCreated}");
        lectureManager.DisplayAllLectures();

        
        Console.WriteLine("Updating lecture... " + lectureManager.UpdateLecture("PR_0002", "The Hangman Game", "Dr. Doom", 45, "Sumatra")); 

        Console.WriteLine($"\n\nLectures in the list: {lectureManager.GetTotalLectures()}"
                    +$"\nTotal lectures created: {Lecture.TotalLecturesCreated}");
        lectureManager.DisplayAllLectures();

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