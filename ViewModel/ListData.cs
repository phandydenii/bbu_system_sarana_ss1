using BBU_SYSTEM.Models;

namespace BBU_SYSTEM.ViewModel;

public class ListData
{
    public IEnumerable<Degree>? Degrees { get; set; }
    public IEnumerable<School>? Schools { get; set; }
    public IEnumerable<Field>? Fields { get; set; }
    public IEnumerable<Promotion>? Promotions { get; set; }
    public IEnumerable<Stage>? Stages { get; set; }
    public IEnumerable<Term>? Terms { get; set; }
    public IEnumerable<Group>? Groups { get; set; }
    public IEnumerable<GroupRoom>? GroupRooms { get; set; }
    public IEnumerable<StudyTime>? StudyTimes { get; set; }
    public IEnumerable<Room>? Rooms { get; set; }

    public IEnumerable<Province>? Provinces { get; set; }
    public IEnumerable<Race>? Races { get; set; }
    public IEnumerable<Nationality>? Nationalities { get; set; }
    public IEnumerable<HighSchool>? HightSchools { get; set; }
    public IEnumerable<StudentJob>? StudentJobs { get; set; }
    public IEnumerable<Disability>? Disabilities { get; set; }

    public IEnumerable<StudentSearch>? StudentSearches { get; set; }
    public IEnumerable<Sponsor>? Sponsors { get; set; }
    public IEnumerable<Certificate>? Certificates { get; set; }
    public IEnumerable<University>? Universities { get; set; }
}