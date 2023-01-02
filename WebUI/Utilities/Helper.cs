using static NuGet.Packaging.PackagingConstants;

namespace WebUI.Utilities;

public static class Helper
{

    public static string Combine(string wwwroot, params string[] folders)
    {
        string path = Path.Combine(wwwroot);
        foreach (var folder in folders)
        {
            path = Path.Combine(path, folder);
        }

        return path;
    }
    public static bool DeleteFile(string wwwroot, params string[] folders)
    {

        string path = Combine(wwwroot, folders);
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }
        return false;
    }

    public enum RoleType:byte
    {
        Admin,
        Moderator,
        Member
    }
}
