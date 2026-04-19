using BBU_SYSTEM.Models;

namespace BBU_SYSTEM.ViewModel;

public class StudentViewModel
{
    public Student? Student { get; set; }
    public Registry? Registry { get; set; }
    public Degree? Degree { get; set; }
    public School? School { get; set; }
    public Field? Field { get; set; }
    public Field? FieldGroup { get; set; }
    public Stage? Stage { get; set; }
    public Term? Term { get; set; }
    public Promotion? Promotion { get; set; }
    public Group? Group { get; set; }
    public GroupRoom? GroupRoom { get; set; }
    public ContactPerson? ContactPerson { get; set; }
    public List<StudentScholarship>? Schoolarships { get; set; }
    public List<StudentCertificate>? StudentCertificates { get; set; }
    public Resume? Resume { get; set; }
    public Extend? Extend { get; set; }
    public Quit? Quit { get; set; }
    public List<Group>? Groups { get; set; }
    public Suspend? Suspend { get; set; }
    public Suppress? Suppress { get; set; }
    public Payment? Payment { get; set; }
}

public class StudentListViewModel
{
    public StudentViewModel? StudentViewModel { get; set; }
    public ListData? ListData { get; set; }
}