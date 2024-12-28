using System.ServiceModel;
using CoreWCF;

public class CSVService : ICSVService
{
    private readonly IWebHostEnvironment _environment;

    public CSVService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public CsvFileResponse GetCSVFile()
    {
        // Path to your static CSV file in wwwroot folder
        string filepath = Path.Combine(_environment.WebRootPath, "testCSV.csv");
        
        if (!File.Exists(filepath))
        {
            throw new FaultException("CSV file not found");
        }

        string content = File.ReadAllText(filepath);

        return new CsvFileResponse
        {
            Content = content,
            Filename = "testCSV.csv",
            Timestamp = DateTime.UtcNow
        };
    }
}