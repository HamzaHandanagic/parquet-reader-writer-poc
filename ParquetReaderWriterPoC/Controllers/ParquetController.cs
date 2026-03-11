using Microsoft.AspNetCore.Mvc;
using Parquet.Serialization;

[ApiController]
[Route("parquet")]
public sealed class ParquetController : ControllerBase
{
    private static readonly string DataDirectory = Path.Combine(AppContext.BaseDirectory, "data");
    private static readonly string ParquetFilePath = Path.Combine(DataDirectory, "sample.parquet");

    [HttpPost("write")]
    [ProducesResponseType(typeof(WriteParquetResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<WriteParquetResponse>> Write([FromBody] WriteParquetRequest? request)
    {
        Directory.CreateDirectory(DataDirectory);

        var data = request?.Records is { Count: > 0 } records
            ? records
            : Extensions.CreateDefaultRecords();

        await using var stream = System.IO.File.Create(ParquetFilePath);
        await ParquetSerializer.SerializeAsync(data, stream);

        return Ok(new WriteParquetResponse(
            "Parquet file written successfully.",
            ParquetFilePath,
            data.Count));
    }

    [HttpGet("read")]
    [ProducesResponseType(typeof(List<ParquetRecord>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ParquetRecord>>> Read()
    {
        if (!System.IO.File.Exists(ParquetFilePath))
        {
            return NotFound(new ErrorResponse(
                "Parquet file not found. Write data first.",
                ParquetFilePath));
        }

        await using var stream = System.IO.File.OpenRead(ParquetFilePath);
        var data = (await ParquetSerializer.DeserializeAsync<ParquetRecord>(stream)).ToList();

        return Ok(data);
    }
}
