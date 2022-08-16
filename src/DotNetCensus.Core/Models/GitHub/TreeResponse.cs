using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCensus.Core.Models.GitHub;

public class TreeResponse
{
    //"sha": "9fb037999f264ba9a7fc6274d15fa3ae2ab98312",
    //"url": "https://api.github.com/repos/octocat/Hello-World/trees/9fb037999f264ba9a7fc6274d15fa3ae2ab98312",
    //"tree": []

    public TreeResponse()
    {
        tree = Array.Empty<FileResponse>();
    }

    public string? sha { get; set; }
    public string? url { get; set; }
    public FileResponse[] tree { get; set; }
}
