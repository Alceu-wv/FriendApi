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
        public List<Friend> GetAll()
        {
            return _dbContext.Friend.ToList();
        }
    }
}
