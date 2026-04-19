namespace BBU_SYSTEM.Models;

public class ChartResult
{
    public List<string> Labels { get; set; } = [];
    public List<ChartDataset> Datasets { get; set; } = [];
}