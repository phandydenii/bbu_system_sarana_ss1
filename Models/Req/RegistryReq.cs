using BBU_SYSTEM.DTOs;

namespace BBU_SYSTEM.Models.Req;

public class RegistryReq
{
    public StudentDto? Student { get; set; }
    public RegistryDto? Registry { get; set; }
    public ContactPersonDto? ContactPerson { get; set; }
    public List<StudentScholarshipDto>? Scholarships { get; set; }
    public List<StudentCertificateDto>? StudentCertificates { get; set; }
    public ExtendDto? Extend { get; set; }
    public ResumeDto? Resume { get; set; }
    public bool IsContinue { get; set; }
    public bool AssToBach { get; set; }
    public bool BachToMas { get; set; }
    public int? ProvinceId { get; set; }
    public int? PlaceOfBirthId { get; set; }
    public int? ProgramId { get; set; }
}