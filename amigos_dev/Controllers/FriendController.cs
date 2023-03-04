using amigos_dev.Domain.Entities;
using amigos_dev.Domain.Interfaces;
using amigos_dev.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace amigos_dev.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendService _service;
        public FriendController(IFriendService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return Ok(_service.GetAll());
        }

        public IActionResult FriendsProximos()
        {
            List<Friend> friendsProximos = _service.GetAll().ToList();
            return View(friendsProximos);
        }

        public IActionResult FriendsDistantes()
        {
            List<Friend> friendsDistantes = _service.GetAll().ToList();
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
    }
}
