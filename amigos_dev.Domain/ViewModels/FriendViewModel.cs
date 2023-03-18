using amigos_dev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amigos_dev.Domain.ViewModels
{
    public class FriendViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string FriendType { get; set; }
        public bool Selected { get; set; }

        public FriendViewModel(Friend friend)
        {
            this.Id = friend.Id;
            this.Name = friend.Name;
            this.Email = friend.Email;
            this.Birthday = friend.Birthday;
            this.FriendType = friend.FriendType;
            this.Selected = false;
        }

        public static List<FriendViewModel> GetAll(List<Friend> friends)
        {
            var listTipo = new List<FriendViewModel>();
            foreach (var item in friends)
            {
                listTipo.Add(new FriendViewModel(item));
            }
            return listTipo;
        }
    }
}
