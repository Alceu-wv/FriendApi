using amigos_dev.Domain.Entities;
using amigos_dev.Domain.ViewModels;

namespace amigos_dev.Domain.Interfaces
{
    public interface IFriendService
    {
        Friend GetById(int id);
        public List<Friend> GetAll();
        void Create(Friend friend);
        void Delete(int id);
        void Update(Friend friend);
        public List<FriendViewModel> GetAllViewModel();
    }
}
