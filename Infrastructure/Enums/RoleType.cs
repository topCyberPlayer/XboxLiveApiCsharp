using System.ComponentModel;

namespace Infrastructure.Enums
{
    public enum RoleType
    {
        [Description("Администратор")]
        Admin = 1,

        [Description("Модератор")]
        Moderator = 2,

        [Description("Пользователь")]
        Gamer = 3
    }
}
