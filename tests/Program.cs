using MrG.Daemon.Control.Data;
using MrG.Daemon.Control.Events;
using MrG.Daemon.Control.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

static void Check(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}

var changes = new List<string?>();
var config = new RemoteServerConfig();
config.PropertyChanged += (_, e) => changes.Add(e.PropertyName);
config.IP = "192.0.2.1";
config.IP = "192.0.2.1";
Check(changes.SequenceEqual(new[] { "IP" }), "Config changes must notify exactly once.");

var disk = new DiskInfo { TotalSpace = 1024 };
changes.Clear();
disk.PropertyChanged += (_, e) => changes.Add(e.PropertyName);
disk.FreeSpace = 512;
Check(disk.SpacePercentage == 50, "Disk percentage changed.");
Check(changes.SequenceEqual(new[] { "FreeSpace", "SpacePercentage", "FreeSpaceString" }), "Disk dependent bindings must update.");
var json = JObject.Parse(JsonConvert.SerializeObject(disk));
Check((double?)json["free"] == 512 && (double?)json["total"] == 1024, "Wire field names changed.");
Check(json["SpacePercentage"] is null, "Computed disk values must stay off the wire.");

var app = new SubApplication();
changes.Clear();
app.PropertyChanged += (_, e) => changes.Add(e.PropertyName);
app.Running = true;
Check(!app.StartEnabled && app.StopEnabled && app.RestartEnabled, "Application command availability changed.");
Check(changes.SequenceEqual(new[] { "Running", "StartEnabled", "StopEnabled", "RestartEnabled" }), "Application dependent bindings must update.");
var restored = JsonConvert.DeserializeObject<SubApplication>("{\"id\":\"sample\",\"name\":\"Example\",\"running\":true}");
Check(restored is { Id: "sample", Name: "Example", Running: true }, "Daemon JSON no longer deserializes.");

// Constructing the remote client does not open a connection or start a service.
var manager = new DaemonServerManager(new RemoteServerConfig());
var events = 0;
manager.DiskInfoEvent += (_, disks) =>
{
    events++;
    Check(disks is { Count: 1 } && disks[0].Path == "C:", "Disk event payload changed.");
};
foreach (var payload in new[] { "null", "{}", "{\"type\":null}", "{\"type\":\"\"}" })
    manager.SocketClient_MessageReceived(payload);
Check(events == 0, "Incomplete responses must not raise events.");
manager.SocketClient_MessageReceived("{\"type\":\"DISKINFO\",\"data\":[{\"name\":\"C:\",\"free\":512,\"total\":1024}]}");
Check(events == 1, "Valid responses must still dispatch.");
var socket = new WebSocketClient("http://127.0.0.1:8180/ws");
socket.SendMessageAsync("not connected");
await socket.StopAsync();
Console.WriteLine("All offline MVVM, JSON, response and disconnected-client checks passed.");
