using MicroService.Model;
using System;
using System.Collections.Generic;
using System.Text;
namespace AspNetrCore.Interface
{
    public interface IUserService
    {
        User FindUser(int id);
        IEnumerable<User> UserAll();

    }
}
