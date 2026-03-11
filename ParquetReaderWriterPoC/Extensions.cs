public static class Extensions
{
    public static List<ParquetRecord> CreateDefaultRecords() =>
    [
        new(1, "Alpha", DateTime.UtcNow, 95.2),
        new(2, "Beta", DateTime.UtcNow.AddMinutes(-10), 88.7),
        new(3, "Gamma", DateTime.UtcNow.AddMinutes(-20), 91.4)
    ];
}
