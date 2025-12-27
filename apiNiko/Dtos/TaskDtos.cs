using System.ComponentModel.DataAnnotations;
namespace apiNiko.Dtos
{
    public class TaskDtos
    {
        public record CreateTaskDto(
        [Required, MinLength(2)] string Title
        );

        public record TaskDto(int Id, string Title, bool IsDone);
    }
}
