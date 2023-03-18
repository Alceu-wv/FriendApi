using amigos_dev.Domain.Entities;
using amigos_dev.Domain.Interfaces;
using amigos_dev.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace amigos_dev.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendService _service;
        public FriendController(IFriendService service)
        {
            _service = service;
        }

        private void SetSession(List<int> selected)
        {

            var selectedFriends = JsonConvert.SerializeObject(selected);
            HttpContext.Session.SetString("selectedFriends", selectedFriends);
        }

        private List<int> GetSelected()
        {
            var selectedFriends = HttpContext.Session.GetString("selectedFriends");

            if (string.IsNullOrEmpty(selectedFriends))
            {
                return new List<int>();
            }
            return JsonConvert.DeserializeObject<List<int>>(selectedFriends);
        }

        public IActionResult Index()
        {
            return Ok(_service.GetAll());
        }

        public IActionResult FriendsProximos()
        {
            List<int> selected = GetSelected();

            List<FriendViewModel> friendsProximos = _service.GetAllViewModel().ToList();

            foreach (var friend in friendsProximos)
            {
                friend.Selected = selected.Contains(friend.Id);
            }
            
            return View(friendsProximos);
        }

        public IActionResult FriendsDistantes()
        {
            List<int> selected = GetSelected();

            List<FriendViewModel> friendsDistantes = _service.GetAllViewModel().ToList();
            
            foreach (var friend in friendsDistantes)
            {
                friend.Selected = selected.Contains(friend.Id);
            }

            return View(friendsDistantes);
        }

        [HttpPost]
        public IActionResult Create(Friend viewModel)
        {
            if (ModelState.IsValid)
            {
                Friend friend = new()
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Birthday = viewModel.Birthday,
                    FriendType = viewModel.FriendType
                };

                _service.Create(friend);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SelectFriend(List<int> selected)
        {
            //TODO: configurar o session da aplicação
            SetSession(selected);
            return RedirectToAction("Index", "Home");
        }
    }
}
