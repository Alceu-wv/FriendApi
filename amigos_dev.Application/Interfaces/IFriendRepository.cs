using amigos_dev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amigos_dev.Application.Interfaces
{
    public interface IFriendRepository
    {
        List<Friend> GetAll();
        void Create(Friend friend);
    }
}
