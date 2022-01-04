﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.Database.DatabaseContext;
using YMovies.Database.Models;
using YMovies.Database.Repositories.IRepository;

namespace YMovies.Database.Repositories.Repository
{
    class UserRepository:IRepository<User>
    {
        private readonly MoviesContext _context;
        public UserRepository(MoviesContext context) => _context = context;
        public IEnumerable<User> Items => _context.Users;
        public User GetItem(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public void AddItem(User item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(User item)
        {
            _context.Users.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return;
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}