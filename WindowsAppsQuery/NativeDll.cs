using System.Runtime.InteropServices;

public static class NativeDll
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pszSource">资源标识符</param>
    /// <param name="pszOutBuf">输出的缓冲区</param>
    /// <param name="cchOutBuf">缓冲区大小</param>
    /// <param name="ppvReserved">保留, 固定0</param>
    /// <returns></returns>
    [DllImport("shlwapi.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, ThrowOnUnmappableChar = true)]
    public static extern unsafe uint SHLoadIndirectString(string pszSource, ref char pszOutBuf, int cchOutBuf, IntPtr ppvReserved);

    [DllImport("FirewallAPI.dll")]
    public static extern uint NetworkIsolationEnumAppContainers(uint Flags, out uint pdwCntPublicACs, out IntPtr ppPublicACs);

    [DllImport("FirewallAPI.dll")]
    public static extern void NetworkIsolationFreeAppContainers(IntPtr pACs);

    public static string? GetIndirectString(string str)
    {
        Span<char> buffer = stackalloc char[4096];
        if (SHLoadIndirectString(str, ref buffer[0], buffer.Length, 0) != 0)
        {
            return null;
        }
        return new string(buffer.TrimEnd('\0'));
    }
}