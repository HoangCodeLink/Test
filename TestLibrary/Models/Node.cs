namespace TestLibrary.Models;

public class Node
{
    public int Id { get; set; }

    public string Label { get; set; }

    public string Type { get; set; }

    public string Code { get; set; }

    public string ICAO { get; set; }

    public string Description { get; set; }

    public string Region { get; set; }

    public int? Runways { get; set; }

    public int? Longest { get; set; }

    public int? Elev { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string Author { get; set; }

    public string Date { get; set; }
}