using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using amigos_dev.Domain.Entities;
using amigos_dev.Domain.Interfaces;
using amigos_dev.Domain.ViewModels;
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
using Microsoft.Extensions.Hosting;


namespace amigos_dev.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIFriendController : ControllerBase
    {
        private readonly IFriendService _service;
        public APIFriendController(IFriendService service)
        {
            _service = service;
        }

        [HttpPost("CreateFriend")]
        public IActionResult Create(Friend viewModel)
        {
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro ao criar amigo: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // GET: APIFriendController
        [HttpGet("GetFriends")]
        public async Task<List<Friend>> GetFriends()
        {
            List<Friend> friends = _service.GetAll().ToList();
            return friends;
        }

        [HttpPost("DeleteFriend")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir amigo: {ex.Message}");
            }
        }

        [HttpPost("EditFriend")]
        public IActionResult Edit(FriendViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Friend friend = new()
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Email = viewModel.Email,
                        Birthday = viewModel.Birthday,
                        FriendType = viewModel.FriendType
                    };

                    _service.Update(friend);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Erro ao atualizar amigo: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

