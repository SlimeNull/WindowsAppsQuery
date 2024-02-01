using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Windows.Management.Deployment;

PackageManager packageManager = new PackageManager();
var packages = packageManager.FindPackagesForUser(WindowsIdentity.GetCurrent().User!.Value);


var r = NativeDll.NetworkIsolationEnumAppContainers(0, out var num, out var ptr);
if (r != 0)
{
    throw new Win32Exception((int)r, Marshal.GetLastPInvokeErrorMessage());
}

List<AppContainer> list = new((int)num);
for (int i = 0; i < num; i++)
{
    var info = Marshal.PtrToStructure<INET_FIREWALL_APP_CONTAINER>(ptr + Marshal.SizeOf<INET_FIREWALL_APP_CONTAINER>() * i);

    if (info.displayName.StartsWith("@{"))
    {
        Console.WriteLine(info.displayName);
    }
    if (info.description.StartsWith("@{"))
    {
        Console.WriteLine(info.description);
    }

    list.Add(new AppContainer(info));
}

NativeDll.NetworkIsolationFreeAppContainers(ptr);
