using Dev.Naamloos.Ducker.Database.Entities;

namespace Dev.Naamloos.Ducker.Dto
{
    public record SafeUserDto(string Id, string Username)
    {
        public static SafeUserDto FromUser(User user)
        {
            return new SafeUserDto(user.Id, user.UserName!);
        }
    }
}
