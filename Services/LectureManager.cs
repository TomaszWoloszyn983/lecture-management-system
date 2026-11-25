using LectureManagementSystem.Models;
using System.Text.Json;

class LectureManager
{

    // Create a list to hold lectures
    private List<Lecture> _lectures;
    private readonly string _filePath = "lectures.json";
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true 
    };

    // Constructor
    public LectureManager()
    {
        _lectures = new List<Lecture>();
    }


    // Method to add a lecture
    public void AddLecture(Lecture lecture)
    {
        _lectures.Add(lecture);
        Console.WriteLine("Lecture added successfully.");
    }

    internal void AddLecture(Programming programming)
    {
        _lectures.Add(programming);
        Console.WriteLine("Lecture added successfully.");
    }


    public void DeleteLecture(Lecture lecture)
    {
        if (_lectures.Remove(lecture))
        {
            Console.WriteLine("Lecture deleted");
        }
        else
        {
            Console.WriteLine("\nLecture not found!");
        }
    }


    public bool DeleteLectureByIndex(int index)
    {
        if (index >= 0 && index < _lectures.Count)
        {
            _lectures.RemoveAt(index);
            Console.WriteLine("Lecture deleted");
            return true;
        }
        else
        {
            Console.WriteLine("\nInvalid index. No lecture removed!");
        }
        return false;
    }


    /**
    * Method to update a lecture's details based on its subject code
    * An empty string or null value for any of the parameters means
    * that particular detail will not be updated.
    */
    public bool UpdateLecture(string subjectCode, string newSubjectName,
                              string newLecturerName, int newLectureHours,
                              string newSpecificDetail)
    {
        if (string.IsNullOrWhiteSpace(subjectCode))
        {
            Console.WriteLine("\nSubject Code not found!");
            return false;
        }

        Lecture? lecture = GetLectureByCode(subjectCode.Trim().ToUpper());

        if (!string.IsNullOrWhiteSpace(newSubjectName))
            lecture.SubjectName = newSubjectName.Trim();

        if (!string.IsNullOrWhiteSpace(newLecturerName))
            lecture.LecturerName = newLecturerName.Trim();

        if (newLectureHours > 0)
        {
            lecture.LectureHours = newLectureHours;
        }

        if (!string.IsNullOrWhiteSpace(newSpecificDetail))
        {
            string trimmedSpecific = newSpecificDetail.Trim();
            switch (lecture)
            {
                case Programming programming:
                    programming.ProgrammingLanguage = trimmedSpecific;
                    break;
                case Astronomy astronomy:
                    astronomy.CelestialBody = trimmedSpecific;
                    break;
                case Mycology mycology:
                    mycology.FungiType = trimmedSpecific;
                    break;
            }
        }
        // SaveToFile(_lectures);
        return true;
    }


    /**
    * Method to display all lectures in the list
    */
    public void DisplayAllLectures()
    {
        Console.WriteLine("\nAll Lectures:");
        foreach (var lecture in _lectures)
        {
            Console.WriteLine(lecture.GetDetails());
        }
    }


    /**
    * Method to get the total number of lectures in the list
    */
    public int GetTotalLectures()
    {
        return _lectures.Count;
    }


    public Lecture? GetLectureByCode(string subjectCode)
    {
        if (string.IsNullOrWhiteSpace(subjectCode))
            return null;

        string normalizedCode = subjectCode.Trim().ToUpper();

        return _lectures.FirstOrDefault(lecture =>
            lecture.SubjectCode.Equals(normalizedCode, StringComparison.OrdinalIgnoreCase));
    }

    public void SaveToFile(List<Lecture> lectures)
    {
        string json = JsonSerializer.Serialize(lectures, JsonOptions);
        File.WriteAllText(_filePath, json);
    }

        public void SaveToFile()
    {
        string json = JsonSerializer.Serialize( _lectures, JsonOptions);
        File.WriteAllText(_filePath, json);
    }


}