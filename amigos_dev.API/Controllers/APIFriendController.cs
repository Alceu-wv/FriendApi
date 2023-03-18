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


        // GET: APIFriendController
        [HttpGet]
        public async Task<List<Friend>> GetFriends()
        {
            List<Friend> friends = _service.GetAll().ToList();
            return friends;
        }

    }
}

/*
// GET: APIFriendController/Details/5
public ActionResult Details(int id)
{
    return View();
}

// GET: APIFriendController/Create
public ActionResult Create()
{
    return View();
}

// POST: APIFriendController/Create
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(IFormCollection collection)
{
    try
    {
        return RedirectToAction(nameof(Index));
    }
    catch
    {
        return View();
    }
}

// GET: APIFriendController/Edit/5
public ActionResult Edit(int id)
{
    return View();
}

// POST: APIFriendController/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Edit(int id, IFormCollection collection)
{
    try
    {
        return RedirectToAction(nameof(Index));
    }
    catch
    {
        return View();
    }
}

// GET: APIFriendController/Delete/5
public ActionResult Delete(int id)
{
    return View();
}

// POST: APIFriendController/Delete/5
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Delete(int id, IFormCollection collection)
{
    try
    {
        return RedirectToAction(nameof(Index));
    }
    catch
    {
        return View();
    }
}
*/