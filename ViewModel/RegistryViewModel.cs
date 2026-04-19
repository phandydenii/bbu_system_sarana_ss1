using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;

namespace BBU_SYSTEM.ViewModel;

public class RegistryViewModel
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

    public IEnumerable<Province>? Provinces { get; set; }
    public IEnumerable<Nationality>? Nationalities { get; set; }
    public IEnumerable<Race>? Races { get; set; }
    public IEnumerable<Disability>? Disabilities { get; set; }
    public IEnumerable<StudentJob>? StudentJobs { get; set; }
    public IEnumerable<HighSchool>? HightSchools { get; set; }
    public IEnumerable<Sponsor>? Sponsors { get; set; }
    public IEnumerable<Certificate>? Certificates { get; set; }
    public IEnumerable<Room>? Rooms { get; set; }

    public StudentDto? Student { get; set; }
    public RegistryDto? Registry { get; set; }
    public ContactPersonDto? ContactPerson { get; set; }
    public List<StudentScholarshipDto>? Schoolarships { get; set; }
    public List<StudentCertificateDto>? StudentCertificates { get; set; }
    public ExtendDto? Extend { get; set; }
    public ResumeDto? Resume { get; set; }
    public bool AssToBach { get; set; }
    public bool BachToMas { get; set; }
}