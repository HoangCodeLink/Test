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

    var nodes = CsvUtils.GetList<Node>("../../../air-routes-latest-nodes.csv");
    var nodeCount = 0;
    Console.WriteLine(nodes.Count);
    foreach (var node in nodes)
    {
        nodeCount++;
        Console.Write($"\r{(nodeCount / nodes.Count):0.##}%");
        g.AddV(node.Label)
            .Property("id", node.Id)
            .Property(nameof(node.Author), node.Author)
            .Property(nameof(node.Code), node.Code)
            .Property(nameof(node.Country), node.Country)
            .Iterate();
    }

    var edges = CsvUtils.GetList<Edge>("../../../air-routes-latest-edges.csv");
    foreach (var edge in edges)
    {
        g.AddE(edge.Label)
            .From(edge.From + "")
            .To(edge.To + "")
            .Property(nameof(edge.Id), edge.Id)
            .Property(nameof(edge.Distance), edge.Distance)
            .Iterate();
    }

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