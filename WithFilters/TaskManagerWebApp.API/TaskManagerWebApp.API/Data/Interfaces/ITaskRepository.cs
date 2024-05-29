using TaskManagerWebApp.API.Models;

namespace TaskManagerWebApp.API.Data.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> GetAllTasks();      
        Task<int> AddTask  (Tasks tasks);
        Task<int> EditTask(Tasks tasks);
        Task<bool> DeleteTask(int taskId);
        Task<Tasks> GetTaskDetailsById(int taskId);
    }
}
