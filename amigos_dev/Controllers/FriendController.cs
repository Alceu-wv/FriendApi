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
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
using amigos_dev.Application.Interfaces;

namespace amigos_dev.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendService _service;
        private readonly HttpClient _client;
        public FriendController(IFriendService service)
        {
            _service = service;
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7122/APIFriend");
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
                using (var response = await client.GetAsync("https://localhost:7122/APIFriend/GetFriends"))
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

        public IActionResult SelectFriend(List<int> selected)
        {
            SetSession(selected);
            return RedirectToAction("Index", "Home");
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

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"api/friends/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var error = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                return StatusCode((int)response.StatusCode, error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Friend viewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(viewModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"api/friends/{viewModel.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var error = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                    return StatusCode((int)response.StatusCode, error);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public IActionResult EditView(int id)
        {
            var friend = _service.GetById(id);

            if (friend == null)
            {
                return NotFound();
            }

            var viewModel = new FriendViewModel(friend);

            return View(viewModel);
        }

    }
}

