using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Entities;


namespace ProTasker.Application.Interfaces.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        //If you wanna write some user speical methods
        //Add here-->> for example: Task<IEnumerable<User>> GetUsersByTeamAsync(Guid teamId);
        //or for example: Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
        //or maybe: Task<IEnumerable<User>> GetUsersOrderedByTaskCountAsync();
    }
}


