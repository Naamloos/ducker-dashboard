using System.Text.Json.Serialization;

namespace Dev.Naamloos.Ducker.Dto
{
    public record RegisterUserDto(string Username, string Email, string Password);

    public record LoginUserDto(string Email, string Password);
}
