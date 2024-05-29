using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApp.API.Models
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duedate { get; set; }

    }
}
