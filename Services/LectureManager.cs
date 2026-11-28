using LectureManagementSystem.Models;
using System.Text.Json;

class LectureManager
{

    // Create a list to hold lectures
    public List<Lecture> Lectures { get; private set;}
    private readonly string _filePath = "lecturesdata.json";
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true 
    };

    // Constructor
    public LectureManager()
    {
        Lectures = new List<Lecture>();
        LoadFromFile();
    }


    // Method to add a lecture
    public void AddLecture(Lecture lecture)
    {
        if (lecture != null)
        {
            Lectures.Add(lecture);
            Console.WriteLine("Lecture added successfully.");
            SaveToFile();
        }
    }


    /**
    * Method to delete a lecture from the list  
    */   
    public void DeleteLecture(Lecture lecture)
    {
        if (Lectures.Remove(lecture))
        {
            Console.WriteLine("Lecture deleted");
            SaveToFile();
        }
        else
        {
            Console.WriteLine("\nLecture not found!");
        }
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
            SaveToFile();
            Console.WriteLine("Lecture updated successfully.");
            return true;
        }else
        {
            Console.WriteLine("\nLecture not found!");
            return false;
        }
    }


    /**
    * Method to get the total number of lectures in the list
    */
    public int GetTotalLectures()
    {
        return Lectures.Count;
    }


    /**
    * Method to get a lecture by its subject code.  
    * Returns null if the lecture is not found or if the input is invalid.
    */
    public Lecture? GetLectureByCode(string subjectCode)
    {
        if (string.IsNullOrWhiteSpace(subjectCode))
            return null;

        string normalizedCode = subjectCode.Trim().ToUpper();

        return Lectures.FirstOrDefault(lecture =>
            lecture.SubjectCode.Equals(normalizedCode, StringComparison.OrdinalIgnoreCase));
    }


    /**
    * Saves the current state of lectures to a JSON file.
    * It serializes the list of lectures and the total number of lectures created.  
    */
        public void SaveToFile()
    {
        var storageData = new LectureStorageData
        {
            TotalLecturesCreated = Lecture.TotalLecturesCreated,
            Lectures = Lectures
        };
        string json = JsonSerializer.Serialize(storageData, JsonOptions);
        File.WriteAllText(_filePath, json);
    }

    /**
     * Loads lectures from a JSON file. If the file doesn't exist or is empty, it resets the lecture counter.
     * It also ensures that only valid lectures (with non-empty subject codes and positive lecture hours) are loaded into the list.
     */
    public void LoadFromFile()
    {
        if (!File.Exists(_filePath))
            return;

        string json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            Lecture.SetLectureCounter(0); // Reset the counter if the file is empty
            return;
        }

        LectureStorageData? storageData = JsonSerializer.Deserialize<LectureStorageData>(json, JsonOptions);
        if (storageData == null)
            return;

        Lecture.SetLectureCounter(storageData.TotalLecturesCreated);
        Lectures.Clear();
        foreach (Lecture lecture in storageData.Lectures)
        {
            if (string.IsNullOrWhiteSpace(lecture.SubjectCode) || lecture.LectureHours <= 0)
                continue;

            bool codeExists = Lectures.Any(l =>
                l.SubjectCode.Equals(lecture.SubjectCode, StringComparison.OrdinalIgnoreCase));

            if (!codeExists)
                Lectures.Add(lecture);
        }
    }
}