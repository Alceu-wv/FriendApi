using amigos_dev.Domain.Entities;
using amigos_dev.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amigos_dev.Domain.Interfaces
{
    public interface IFriendService
    {
        public List<Friend> GetAll();
        void Create(Friend friend);

        public List<FriendViewModel> GetAllViewModel();
    }
}
