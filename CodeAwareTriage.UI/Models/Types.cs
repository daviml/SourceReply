namespace CodeAwareTriage.UI.Models;

public class DynamicRow : Dictionary<string, object>
{
    public string _id { get; set; } = Guid.NewGuid().ToString();
    public IaVerdict IaVerdict { get; set; } = new IaVerdict();
}

public class IaVerdict
{
    public string Status { get; set; } = "idle"; // idle, analyzing, completed
    public string Text { get; set; } = "";
    public string Type { get; set; } = ""; // Bug, Infra, Dúvida
}

public class ColumnMapping
{
    public string IdColumn { get; set; } = "";
    public string DescriptionColumn { get; set; } = "";
    public string StatusColumn { get; set; } = "";
    public string PriorityColumn { get; set; } = "";
    public string DateColumn { get; set; } = "";
}

public class CodeFile
{
    public string Name { get; set; } = "";
    public string Path { get; set; } = "";
    public string Content { get; set; } = "";
}

public class DashboardStats
{
    public int Total { get; set; }
    public int Critical { get; set; }
    public int SlaExceeded { get; set; }
}
