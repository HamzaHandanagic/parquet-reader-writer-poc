using System;

public sealed record ParquetRecord
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime CreatedAtUtc { get; init; }
    public double Score { get; init; }

    public ParquetRecord() { }

    public ParquetRecord(int id, string name, DateTime createdAtUtc, double score)
    {
        Id = id;
        Name = name;
        CreatedAtUtc = createdAtUtc;
        Score = score;
    }
}
