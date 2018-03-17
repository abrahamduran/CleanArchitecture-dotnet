using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INTEC.Models.ViewModels;
using INTEC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INTEC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("Index")]
        public JsonResult Index()
        {
            var result = userService.GetAll();

            return Json(result.ResultObject);
        }

        [HttpGet("Detail")]
        public JsonResult Detail(string id)
        {
            var result = userService.Get(id);
            
            return Json(result.ResultObject);
        }

        [HttpPost("Login")]
        public JsonResult Login(string Username, string Password)
        {
            UserViewModel viewModel = new UserViewModel();
            viewModel.Username = Username;
            viewModel.Password = Password;

            var result = userService.Login(viewModel);

            return Json(result.ResultObject);
        }

        [HttpPost("Register")]
        public JsonResult Register(string Username, string Password, string Email)
        {
            UserViewModel viewModel = new UserViewModel();
            viewModel.Username = Username;
            viewModel.Password = Password;
            viewModel.Email = Email;
            viewModel.Enrollment = "1068864";

            var result = userService.Register(viewModel);

            return Json(result.ResultObject);
        }
    }
}
