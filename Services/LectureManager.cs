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
        // Console.WriteLine($"Lecture added: {lecture.GetDetails()}");
        Console.WriteLine("Lecture added successfully.");
    }

    internal void AddLecture(Programming programming)
    {
        _lectures.Add(programming);
        // Console.WriteLine($"Lecture added: {programming.GetDetails()}");
        Console.WriteLine("Lecture added successfully.");
    }

    public void DeleteLecture(Lecture lecture)
    {
        if (_lectures.Remove(lecture))
        {
            Console.WriteLine($"Lecture removed: {lecture.GetDetails()}");
        }
        else
        {
            Console.WriteLine("\nLecture not found!");
        }
    }

    public void DeleteLectureByIndex(int index)
    {
        if (index >= 0 && index < _lectures.Count)
        {
            var lecture = _lectures[index];
            _lectures.RemoveAt(index);
            Console.WriteLine($"Lecture removed: {lecture.GetDetails()}");
        }
        else
        {
            Console.WriteLine("\nInvalid index. No lecture removed!");
        }
    }


    public bool UpdateLecture(string subjectCode, string? newSubjectName,
                              string? newLecturerName, int? newLectureHours,
                              string? newSpecificDetail = null)
    {
        if (string.IsNullOrWhiteSpace(subjectCode))
            return false;

        string normalizedCode = subjectCode.Trim().ToUpper();
        Lecture? lecture = _lectures.FirstOrDefault(l => l.SubjectCode.Equals(normalizedCode, StringComparison.OrdinalIgnoreCase));

        if (lecture == null)
            return false;

        if (!string.IsNullOrWhiteSpace(newSubjectName))
            lecture.SubjectName = newSubjectName.Trim();

        if (!string.IsNullOrWhiteSpace(newLecturerName))
            lecture.LecturerName = newLecturerName.Trim();

        if (newLectureHours.HasValue)
        {
            if (newLectureHours.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(newLectureHours), "Liczba godzin musi być większa od zera.");

            lecture.LectureHours = newLectureHours.Value;
        }

        if (lecture is Programming programmingLecture && !string.IsNullOrWhiteSpace(newSpecificDetail))
        {
            programmingLecture.ProgrammingLanguage = newSpecificDetail.Trim();
        }

        // SaveToFile();
        return true;
    }

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


}