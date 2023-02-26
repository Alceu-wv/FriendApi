using amigos_dev.Domain.Interfaces;
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
    }
}
