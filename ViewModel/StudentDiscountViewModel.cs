namespace BBU_SYSTEM.ViewModel;

public class StudentDiscountViewModel
{
    public int StudentDiscountId { get; set; }
    public string? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentNameInKhmer { get; set; }
    public string? Sex { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? PlaceOfBirthId { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public int Discount { get; set; }
    public string? Note { get; set; }
}