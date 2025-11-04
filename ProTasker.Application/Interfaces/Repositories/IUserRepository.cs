using ProTasker.Application.Models;  // ApplicationUser'ı burada alıyoruz
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>  // ApplicationUser'ı kullanıyoruz
    {
        // Kullanıcıya özel metotlar burada tanımlanabilir
        // Örneğin: Task<IEnumerable<ApplicationUser>> GetUsersByTeamAsync(Guid teamId);
        // ya da: Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string role);
        // veya: Task<IEnumerable<ApplicationUser>> GetUsersOrderedByTaskCountAsync();
        Task<ApplicationUser?> GetByUserNameAsync(string username);
    }
}