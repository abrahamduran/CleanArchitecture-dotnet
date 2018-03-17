using System;
using INTEC.Models.Infraestructure;
using INTEC.Models.ViewModels;
using INTEC.Services.Base;

namespace INTEC.Services.Interfaces
{
    public interface IUserService : IBaseService<UserViewModel>
    {
        ServiceResult Get(string id);
        ServiceResult Login(UserViewModel viewModel);
        ServiceResult Register(UserViewModel viewModel);
    }
}
