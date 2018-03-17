using System;
using System.Collections.Generic;
using INTEC.Data.Entities;
using INTEC.Helpers.Utils;
using INTEC.Models.Infraestructure;
using INTEC.Models.ViewModels;
using INTEC.Repository.Framework;
using INTEC.Services.Base;
using INTEC.Services.Interfaces;
using System.Linq;
using INTEC.Helpers.Infraestructure;
using INTEC.Helpers.Extensions;

namespace INTEC.Services.Implementations
{
    public class UserService : BaseService<UserViewModel, User>, IUserService
    {
        private IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
        }

        public ServiceResult Get(string id)
        {
            return GetByRowId(id);
        }

        public ServiceResult Login(UserViewModel viewModel)
        {
            ServiceResult serviceResult = new ServiceResult();

            viewModel.Password = MD5Hasher.ComputeHash(viewModel.Password);

            try
            {
                var user = ((List<User>)userRepository.
                GetAll(i => i.Username == viewModel.Username
                       && i.Password == viewModel.Password).Data).FirstOrDefault();

                if(user != null)
                {
                    user.LastAccessDate = DateTime.Now;

                    userRepository.Update(user);
                }

                serviceResult.Success = true;
                serviceResult.ResultObject = MapperHelper.Instance.Map<User, UserViewModel>(user);
                serviceResult.Messages.Add(Error.GetErrorMessage(Error.CorrectTransaction));
            }
            catch (Exception ex)
            {
                serviceResult.LogError(ex);
            }

            return serviceResult;
        }

        public ServiceResult Register(UserViewModel viewModel)
        {
            return Insert(viewModel);

            // remove this if the above code works
            ServiceResult serviceResult = new ServiceResult();

            viewModel.Password = MD5Hasher.ComputeHash(viewModel.Password);

            try
            {
                viewModel.RowId = Guid.NewGuid().ToString();
                var user = MapperHelper.Instance.Map<UserViewModel, User>(viewModel);
                user.Created = DateTime.Now;

                userRepository.Insert(user);

                serviceResult.Success = true;
                serviceResult.Messages.Add(Error.GetErrorMessage(Error.CorrectTransaction));
            }
            catch (Exception ex)
            {
                serviceResult.LogError(ex);
            }

            return serviceResult;
        }
    }
}
