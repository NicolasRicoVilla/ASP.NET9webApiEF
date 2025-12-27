using System.ComponentModel.DataAnnotations;
namespace apiNiko.Dtos
{
    public class UserDtos
    {
        public record CreateUserDto(
        [Required,MinLength(2)] string Name,
        [Range(0,130)] int Age
        );

        public record UserSummaryDtos(int Id, string Name, int Age, int OpenTasks);
    }
}
