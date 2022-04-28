using System.Text.Json;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Structure;
using static Gremlin.Net.Process.Traversal.AnonymousTraversalSource;
using TestLibrary.Utilities;
using TestLibrary.Models;
using Edge = TestLibrary.Models.Edge;

try
{
    Console.WriteLine("Start setup");
    var endpoint = "localhost";
    // This uses the default Neptune and Gremlin port, 8182
    var gremlinServer = new GremlinServer(endpoint, 8182, false);
    var gremlinClient = new GremlinClient(gremlinServer);
    var remoteConnection = new DriverRemoteConnection(gremlinClient, "g");
    var g = Traversal().WithRemote(remoteConnection);
    Console.WriteLine("Connected");

    var count = 0;
    var nodes = CsvUtils.GetList<Node>("air-routes-latest-nodes.csv");
    foreach (var node in nodes)
    {
        count++;
        Console.Write($"\r{decimal.Divide(count * 100, nodes.Count):0.00}%");
        g.AddV(node.Label)
            .Property("id", node.Id)
            .Property(nameof(node.Author), node.Author)
            .Property(nameof(node.Code), node.Code)
            .Property(nameof(node.Country), node.Country)
            .Iterate();
    }
    Console.WriteLine();

    count = 0;
    var edges = CsvUtils.GetList<Edge>("air-routes-latest-edges.csv");
    foreach (var edge in edges)
    {
        count++;
        Console.Write($"\r{decimal.Divide(count * 100, edges.Count):0.00}%");
        g.AddE(edge.Label)
            .From(g.V().Has("id", edge.From).Next())
            .To(g.V().Has("id", edge.To).Next())
            .Property(nameof(edge.Id), edge.Id)
            .Property(nameof(edge.Distance), edge.Distance)
            .Iterate();
    }
    Console.WriteLine();

    // g.AddV("Person").Property("Name", "Justin").Iterate();
    // g.AddV("Custom Label").Property("Name", "Custom id vertex 1").Iterate();
    // g.AddV("Custom Label").Property("Name", "Custom id vertex 2").Iterate();
    // g.AddE()
    //
    // var output = g.V().Limit<Vertex>(3).ToList();
    // Console.WriteLine($"Length of output is {output.Count}");
    //
    // foreach (var item in output)
    // {
    //     Console.WriteLine(JsonSerializer.Serialize(g.V().HasId(item.Id).Project<object>("Name").By("Name").Next()));
    // }
}
catch (Exception e)
{
    Console.WriteLine("{0}", e);
}