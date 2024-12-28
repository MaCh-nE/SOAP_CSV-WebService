using System.Runtime.Serialization;
using CoreWCF;

[DataContract]
public class CsvFileResponse
{
    [DataMember]
    public string Content { get; set; }

    [DataMember]
    public string Filename { get; set; }

    [DataMember]
    public DateTime Timestamp { get; set; }
}