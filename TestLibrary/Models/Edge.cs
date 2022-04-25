namespace TestLibrary.Models;

public class Edge
{
    public int Id { get; set; }

    public int From { get; set; }

    public int To { get; set; }

    public string Label { get; set; }

    public int? Distance { get; set; }
}