using Sklad_project_app.Models;

namespace Sklad_project_app
{
    public static class CurrentUser
    {
        public static User User { get; set; }
        public static string RoleName { get; set; }

        public static bool IsAdmin => RoleName =="Администратор";
        public static bool IsStorekeeper => RoleName =="Кладовщик";
    }
}