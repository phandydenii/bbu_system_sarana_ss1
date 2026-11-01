namespace BBU_SYSTEM.Helper;

public static class FunctionHelper
{
    public static DateTime? ParseDate(string? value) =>
        DateTime.TryParse(value, out var date) ? date : null;
}

 