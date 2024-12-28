using CoreWCF;

[ServiceContract]
public interface ICSVService
{
    [OperationContract]
    CsvFileResponse GetCSVFile();
}