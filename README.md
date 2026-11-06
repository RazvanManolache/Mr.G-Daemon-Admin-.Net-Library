# Mr.G-Daemon-Admin-.Net-Library
## GitHub builds

Open **Actions → Admin library package → Run workflow**, then download
`MrG-Daemon-Admin-NuGet` from the completed run. Pushes and pull requests also
build. Packages use `1.0.0-ci.<run number>`, include a README and have a SHA-256
checksum. They are downloadable artifacts; this workflow does not publish to NuGet.org.

Offline checks cover property-change notifications, derived UI bindings, JSON
wire names, response dispatch and a disconnected client. Compilation warnings
fail the build. The remote client targets .NET 8 and is portable;
`DaemonServiceManager` is explicitly Windows-only.

The obsolete MVVM Light dependency has been replaced by CommunityToolkit.Mvvm.
Model classes now inherit `ObservableObject`; consumers using MVVM Light-specific
base-class APIs must migrate, while property names and notifications are preserved.
