using System.Runtime.InteropServices;

internal class AppContainer
{
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string PackageFullName { get; set; }
    public string[] Binaries { get; set; } = [];
    public string WorkingDirectory { get; set; }

    public AppContainer(INET_FIREWALL_APP_CONTAINER info)
    {
        PackageFullName = info.packageFullName;
        WorkingDirectory = info.workingDirectory;
        uint HRESULT = 0;

        // @{Microsoft.WindowsCamera_2022.2201.4.0_x64__8wekyb3d8bbwe?ms-resource://Microsoft.WindowsCamera/LensSDK/Resources/AppStoreName}
        // @{Microsoft.WindowsCamera_2022.2201.4.0_x64__8wekyb3d8bbwe?ms-resource://Microsoft.WindowsCalculator/AppStoreName} manifest里的


        Span<char> buffer = stackalloc char[4096];
        HRESULT = NativeDll.SHLoadIndirectString(info.displayName, ref buffer[0], buffer.Length, 0);
        if (HRESULT == 0)
            DisplayName = new string(buffer.TrimEnd('\0'));
        else
            DisplayName = info.displayName;
        buffer.Clear();

        if (NativeDll.SHLoadIndirectString(info.description, ref buffer[0], buffer.Length, 0) == 0)
            Description = new string(buffer.TrimEnd('\0'));
        else
            Description = info.description;
        buffer.Clear();

        INET_FIREWALL_AC_BINARIES inet_FIREWALL_AC_BINARIES = info.binaries;
        if (inet_FIREWALL_AC_BINARIES.count > 0 && inet_FIREWALL_AC_BINARIES.binaries != 0)
        {
            Binaries = new string[inet_FIREWALL_AC_BINARIES.count];
            for (int i = 0; i < inet_FIREWALL_AC_BINARIES.count; i++)
            {
                var str = Marshal.PtrToStringUni(Marshal.ReadIntPtr(inet_FIREWALL_AC_BINARIES.binaries + nint.Size * i));
                ;
                if (str?.StartsWith(@"\\?\") == true)
                {
                    str = str[4..];
                }

                if (str != null)
                {
                    Binaries[i] = str;
                }
            }
        }

        if (DisplayName.Contains("Photo", StringComparison.OrdinalIgnoreCase))
        {

        }

        if (Binaries?.Any(v => v.Contains("Photo", StringComparison.OrdinalIgnoreCase)) == true)
        {

        }

        //Console.WriteLine($"{DisplayName}\t{Binaries.Length}");
        //Console.WriteLine(Description);
        //Console.WriteLine();



    }

}
