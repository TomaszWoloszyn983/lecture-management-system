using LectureManagementSystem.Models;

class LectureManager
{

    // Create a list to hold lectures
    private List<Lecture> _lectures;


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
    public bool UpdateLecture(string subjectCode, string? newSubjectName,
                              string? newLecturerName, int? newLectureHours,
                              string? newSpecificDetail = null)
    {
        if (string.IsNullOrWhiteSpace(subjectCode))
            return false;

        Lecture? lecture = GetLectureByCode(subjectCode.Trim().ToUpper());
        if (lecture == null)
        {
            Console.WriteLine("\nLecture not found.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(newSubjectName))
            lecture.SubjectName = newSubjectName.Trim();

        if (!string.IsNullOrWhiteSpace(newLecturerName))
            lecture.LecturerName = newLecturerName.Trim();

        if (newLectureHours.HasValue)
        {
            if (newLectureHours.Value <= 0)
                Console.WriteLine("Lecture hours must be a positive number.");

            lecture.LectureHours = newLectureHours.Value;
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
                    astronomy.Galaxy = trimmedSpecific;
                    break;
                case Mycology mycology:
                    mycology.FungiType = trimmedSpecific;
                    break;
            }
        }
        // SaveToFile();
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


}