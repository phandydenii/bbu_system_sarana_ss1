using BBU_SYSTEM.Models;

namespace BBU_SYSTEM.ViewModel;

public class AcademicViewModel
{
    public IEnumerable<Degree>? Degrees { get; set; }
    public IEnumerable<School>? Schools { get; set; }
    public IEnumerable<Field>? Fields { get; set; }
    public IEnumerable<Promotion>? Promotions { get; set; }
    public IEnumerable<Stage>? Stages { get; set; }
    public IEnumerable<Term>? Terms { get; set; }
    public IEnumerable<Group>? Groups { get; set; }
    public IEnumerable<GroupRoom>? GroupRooms { get; set; }
}