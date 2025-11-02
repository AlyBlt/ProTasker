using ProTasker.Application.Models;  // ApplicationUser'ı buradan alıyoruz
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();  
        Task<ApplicationUser?> GetByIdAsync(Guid id);  
        Task AddAsync(ApplicationUser user);  
        Task UpdateAsync(ApplicationUser user);  
        Task<bool> DeleteAsync(Guid id);  
    }
}