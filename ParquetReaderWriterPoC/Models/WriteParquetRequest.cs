public sealed record WriteParquetRequest
{
    public List<ParquetRecord>? Records { get; init; }
}
