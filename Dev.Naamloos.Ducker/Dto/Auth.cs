using System.Text.Json.Serialization;

namespace Dev.Naamloos.Ducker.Dto
{
    public record RegisterUserDto(string Username, string Password, string InviteCode);

    public record LoginUserDto(string Username, string Password);
}
