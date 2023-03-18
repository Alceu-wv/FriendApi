using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using amigos_dev.Domain.Interfaces;
using amigos_dev.Domain.Entities;
using amigos_dev.Domain.ViewModels;
using amigos_dev.Application.Interfaces;

namespace amigos_dev.Infrastructure.Services
{
    internal class FriendService : IFriendService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendService(IFriendRepository friendRepository) { _friendRepository = friendRepository; }

        public void Create(Friend friend)
        {
            _friendRepository.Create(friend);
        }

        public List<Friend> GetAll()
        {
            return _friendRepository.GetAll();
        }

        List<FriendViewModel> IFriendService.GetAllViewModel()
        {
            return FriendViewModel.GetAll(this.GetAll());
        }
    }
}
