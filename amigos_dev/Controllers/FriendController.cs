using amigos_dev.Domain.Entities;
using amigos_dev.Domain.Interfaces;
using amigos_dev.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

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

        public async Task<IActionResult> FriendsProximosAsync()
        {
            List<FriendViewModel> friendsProximos = new();
            List<int> selected = GetSelected();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "TESTE");
                using (var response = await client.GetAsync("https://localhost:7122/APIFriend"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Erro401", "Home");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var friends = JsonConvert.DeserializeObject<List<Friend>>(apiResponse);
                    friendsProximos = FriendViewModel.GetAll(friends);
                }
            }


            foreach (var friend in friendsProximos)
            {
                friend.Selected = selected.Contains(friend.Id);
            }
            
            return View(friendsProximos);
        }

        public async Task<IActionResult> FriendsDistantesAsync()
        {
            List<FriendViewModel> FriendsDistantes = new();
            List<int> selected = GetSelected();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "TESTE");
                using (var response = await client.GetAsync("https://localhost:7122/APIFriend"))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Erro401", "Home");
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var friends = JsonConvert.DeserializeObject<List<Friend>>(apiResponse);
                    FriendsDistantes = FriendViewModel.GetAll(friends);
                }
            }


            foreach (var friend in FriendsDistantes)
            {
                friend.Selected = selected.Contains(friend.Id);
            }

            return View(FriendsDistantes);
        }
        /*
        {
            List<int> selected = GetSelected();

            List<FriendViewModel> friendsDistantes = _service.GetAllViewModel().ToList();
            
            foreach (var friend in friendsDistantes)
            {
                friend.Selected = selected.Contains(friend.Id);
            }

            return View(friendsDistantes);
        }
        */

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
