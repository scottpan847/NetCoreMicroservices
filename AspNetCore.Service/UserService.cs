using AspNetrCore.Interface;
using MicroService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Service
{
    public class UserService : IUserService
    {

        private  List<User> _userList = new List<User>() 
        {
            new User()
            {
                Id = 1,
                Account = "Admin",
                Email = "11111@qq.com",
                Name = "jack",
                Pwd = "111",
                LoginTime = DateTime.Now,
                Role = "Admin"
            },
            new User()
            {
                Id = 2,
                Account = "Apple",
                Email = "22222@qq.com",
                Name = "rose",
                Pwd = "222",
                LoginTime = DateTime.Now,
                Role = "Admin"
            },
            new User()
            {
                Id = 3,
                Account = "Cole",
                Email = "33333@qq.com",
                Name = "lisa",
                Pwd = "333",
                LoginTime = DateTime.Now,
                Role = "Admin"
            },
        };

        //public UserService(List<User> _userList)
        //{
        //    this._userList = _userList;
        //}
        public User FindUser(int id)
        {
            return _userList.Find(x=>x.Id==id);
        }

        public IEnumerable<User> UserAll()
        {
            return _userList;
        }
    }
}
