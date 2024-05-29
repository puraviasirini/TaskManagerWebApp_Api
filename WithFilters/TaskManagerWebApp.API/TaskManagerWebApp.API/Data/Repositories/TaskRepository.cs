using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagerWebApp.API.Data.Interfaces;
using TaskManagerWebApp.API.Models;

namespace TaskManagerWebApp.API.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public readonly TaskManagerDbContext _taskManagerDbContext;

        public TaskRepository(TaskManagerDbContext taskManagerDbContext)
        {
            _taskManagerDbContext = taskManagerDbContext;
        }
        public async Task<int> AddTask(Tasks taskRequest)
        {
            if (string.IsNullOrWhiteSpace(taskRequest.Title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }

            try
            {
                var parameters = new[]
                    {
                        new SqlParameter("@title", taskRequest.Title),
                        new SqlParameter("@description", taskRequest.Description),
                        new SqlParameter("@dueDate", taskRequest.Duedate)
                    };

                var result = await _taskManagerDbContext.Database.ExecuteSqlRawAsync("EXEC InsertTask @title, @description, @dueDate", parameters);
                ;
                await _taskManagerDbContext.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to add task to the database.", ex);
                //return 0;
            }


        }

        public async Task<bool> DeleteTask(int taskId)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@taskId", taskId)
                };

                var result = await _taskManagerDbContext.Database.ExecuteSqlRawAsync("EXEC DeleteTask @taskId", parameters);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete task.", ex);
                //return false;
            }
        }

        public async Task<int> EditTask(Tasks taskEditRequest)
        {

            try
            {
                var parameters = new[]
            {
                        new SqlParameter("@id", taskEditRequest.Id),
                        new SqlParameter("@title", taskEditRequest.Title),
                        new SqlParameter("@description", taskEditRequest.Description),
                        new SqlParameter("@dueDate", taskEditRequest.Duedate)
                    };

                var result = await _taskManagerDbContext.Database.ExecuteSqlRawAsync("EXEC UpdateTask @id, @title, @description, @dueDate", parameters);

                await _taskManagerDbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete task.", ex);
                //return 0;
            }

        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            try
            {
                var result = await _taskManagerDbContext.Tasks.FromSqlRaw("GetAllTasks").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load tasks.", ex);
                //return null;
            }


        }

        public async Task<Tasks> GetTaskDetailsById(int taskId)
        {
            try
            {
                var result = await _taskManagerDbContext.Tasks.FromSqlInterpolated($"EXECUTE GetTaskDetailsById {taskId}").AsNoTracking().ToListAsync();


                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load task.", ex);
                //return null;
            }
        }
    }
}
