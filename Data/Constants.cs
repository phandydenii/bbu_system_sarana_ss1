namespace BBU_SYSTEM.Data;

public static class TermStatusConstant
{
    public const string Pass = "PASSED";
    public const string Active = "ACTIVE";
}
public static class StageStatusConstant
{ 
    public const string Active = "ACTIVE";
}

public static class AssignGroupCampusConstant
{
    public static readonly Dictionary<string, string> Campuses = new()
    {
        { "associate", "ក្រុមបរិញ្ញាបត្ររង" }, //degree, school,field, promotion, stage, study-time
        { "foundation", "ក្រុមបរិញ្ញាបត្រមូលដ្ឋាន" }, //degree, school,field, promotion, stage, study-time
        { "specialize", "ក្រុមបរិញ្ញាបត្រជំនាញ" }, //degree, school,academic-year, promotion, stage, study-time
        { "master", "ក្រុមអនុបណ្ឌិត" }, //degree, school, promotion, stage, study-time
        { "doctor", "ក្រុមបណ្ឌិត" }, //degree, school, promotion, stage, study-time
        { "diploma", "ក្រុមបាក់ឌុប" }, //degree, school, promotion, stage, study-time
        { "other", "សាខាផ្សេង/សាកលផ្សេង/បរិញ្ញាបត្ររងទៅបរិញ្ញាបត្រ" },
        { "unpromoted", "មិនទាន់ឡើងថ្នាក់" }, //degree, school, promotion, stage, study-time
    };
}

public static class ScoreTypeConstant
{
    public const string FinalAndStateProjectPaper = "FINAL_FINAL_AND_STATE_PROJECT_PAPER";
    public const string Final = "FINAL";
    public const string FinalAndState = "FINAL_AND_STATE";
    public const string ProjectPaper = "PROJECT_PAPER";
    public const string StateExam = "STATE_EXAM";
    public const string Practicum = "PRACTICUM";
}