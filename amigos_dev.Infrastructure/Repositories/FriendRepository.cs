using amigos_dev.Application.Interfaces;
using amigos_dev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amigos_dev.Infrastructure.Repositories
{
    internal class FriendRepository : IFriendRepository
    {
        private readonly FDBContext _dbContext;
        public FriendRepository(FDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Friend GetById(int id)
        {
            return _dbContext.Friend.Find(id);
        }

        public void Create(Friend friend)
        {
            _dbContext.Friend.Add(friend);
            _dbContext.SaveChanges();
        }

        public List<Friend> GetAll()
        {
            return _dbContext.Friend.ToList();
        }

        public void Delete(int id)
        {
            var friend = _dbContext.Friend.Find(id);
            if (friend != null)
            {
                _dbContext.Friend.Remove(friend);
                _dbContext.SaveChanges();
            }
        }

        public void Update(Friend friend)
        {
            _dbContext.Friend.Update(friend);
            _dbContext.SaveChanges();
        }
    }
}
