namespace Net8StarterAuthApi.Auth.Models;

public static class AdminRoleOptions
{
    public static string SysAdmin = "SysAdmin";
    public static string Support = "Support";
}

public class AdminRole
{
    public string Name { get; set; }

    public string Description { get; set; }
}
