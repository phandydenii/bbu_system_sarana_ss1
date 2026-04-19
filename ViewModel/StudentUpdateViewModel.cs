namespace BBU_SYSTEM.ViewModel;

public class StudentUpdateViewModel
{
    public string? StudentId { get; set; } = "";
    public string? StudentName { get; set; } = "";
    public string? StudentNameInKhmer { get; set; } = "";
    public string? Sex { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
    public int PlaceOfBirthId { get; set; }
    public int RaceId { get; set; }
    public int NationalityId { get; set; }
    public string? MaritalStatus { get; set; }
    public int HighSchoolGraduatedYear { get; set; }
    public int FromProvinceId { get; set; }
    public string? FromHighSchoolNameInKhmer { get; set; }
    public int JobId { get; set; }
    public string? MotherNameInKhmer { get; set; }
    public string? MotherOccupationInKhmer { get; set; }
    public string? FatherNameInKhmer { get; set; }
    public string? FatherOccupationInKhmer { get; set; }
    public string? Phone { get; set; } = "";
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? AddressInKhmer { get; set; } 
    public int FieldId { get; set; }
    public int IsPhotoReceived { get; set; }
    public string? Note { get; set; }
    public string? Status { get; set; }
}