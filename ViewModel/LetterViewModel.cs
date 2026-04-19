using BBU_SYSTEM.Models;

namespace BBU_SYSTEM.ViewModel;

public class LetterViewModel
{
    public ListData? ListDatas { get; set; }
    public IEnumerable<Letter>? Letters { get; set; }
    public IEnumerable<LetterCategory>? LetterCategories { get; set; }
}