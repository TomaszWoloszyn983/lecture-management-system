using LectureManagementSystem.Models;
public class LectureStorageData
{
    public int TotalLecturesCreated { get; set; }
    public List<Lecture> Lectures { get; set; } = new();
}