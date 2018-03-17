using System;
using System.Collections.Generic;
using INTEC.Data;
using INTEC.Helpers.Extensions;
using INTEC.Helpers.Infraestructure;
using INTEC.Helpers.Utils;
using INTEC.Models;
using INTEC.Models.Infraestructure;
using INTEC.Repository.Framework;
using System.Linq;

namespace INTEC.Services.Base
{
    public class BaseService<Vm, Ent> : IBaseService<Vm> where Ent : BaseEntity where Vm : BaseViewModel
    {
        protected IRepository<Ent> Repository;

        public BaseService(IRepository<Ent> repository)
        {
            this.Repository = repository;
        }

        public ServiceResult Delete(Vm viewModel)
        {
            ServiceResult serviceResult = new ServiceResult();

            if (viewModel == null)
            {
                serviceResult.Success = false;
                serviceResult.ResultTitle = "ERROR";
                serviceResult.Messages.Add(Error.GetErrorMessage(Error.EmptyModel));
                return serviceResult;
            }

            try
            {
                Ent toDelete = Repository.GetById((int)viewModel.Id).Data as Ent;

                if(toDelete == null)
                {
                    serviceResult.Success = false;
                    serviceResult.ResultTitle = "ERROR";
                    serviceResult.Messages.Add(Error.GetErrorMessage(Error.RecordNotFound));
                    return serviceResult;
                }

                var sr = Repository.Delete(toDelete);
                serviceResult.Success = sr.Success;
                serviceResult.ResultObject = null;
                serviceResult.ResultTitle = Error.GetErrorMessage(Error.CorrectTransaction);

            }
            catch (Exception ex)
            {
                serviceResult.LogError(ex);
            }

            return serviceResult;
        }

        public ServiceResult GetAll()
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                serviceResult.Success = true;
                serviceResult.ResultObject = MapperHelper.Instance.Map<List<Ent>, List<Vm>>(this.Repository.GetAll().Data);
                serviceResult.ResultTitle = Error.GetErrorMessage(Error.CorrectTransaction);
                serviceResult.Messages.Add(serviceResult.ResultTitle);
            }
            catch (Exception ex)
            {
                serviceResult.LogError(ex);
            }

            return serviceResult;
        }

        protected ServiceResult GetById(int id)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult.Success = true;
                serviceResult.Messages.Add(Error.GetErrorMessage(Error.CorrectTransaction));
                serviceResult.ResultObject = MapperHelper.Instance.Map<Ent, Vm>(this.Repository.GetById(id).Data);
            }
            catch (Exception ex)
            {
                serviceResult.LogError(ex);
            }

            return serviceResult;
        }

        public ServiceResult GetByRowId(string RowId)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                var result = ((List<Ent>)this.Repository.GetAll(i => i.RowId == RowId).Data).FirstOrDefault();
                serviceResult.Success = true;
                serviceResult.Messages.Add(Error.GetErrorMessage(Error.CorrectTransaction));
                serviceResult.ResultObject = MapperHelper.Instance.Map<Ent, Vm>(result);

            }
            catch (Exception ex)
            {
                serviceResult.LogError(ex);
            }

            return serviceResult;
        }

        public ServiceResult Insert(Vm viewModel)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                var ToInsert = MapperHelper.Instance.Map<Vm, Ent>(viewModel);
                ToInsert.RowId = Guid.NewGuid().ToString();

                var sr = this.Repository.Insert(ToInsert);

                serviceResult.Success = sr.Success;
                serviceResult.ResultTitle = (sr.Success ? "SUCCESS" : "ERROR");
                serviceResult.Messages.Add((sr.Success ? "Inserted" : "Failed"));
                serviceResult.ResultObject = MapperHelper.Instance.Map<Ent, Vm>(sr.Data);

            }
            catch (Exception ex)
            {
                serviceResult.LogError(ex);
            }

            return serviceResult;
        }

        public ServiceResult Update(Vm viewModel)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                Ent ToUpdate = this.Repository.GetById((int)viewModel.Id).Data;

                if (ToUpdate == null)
                {
                    serviceResult.Messages.Add(Error.GetErrorMessage(Error.RecordNotFound));
                    return serviceResult;
                }

                ToUpdate = MapperHelper.Instance.Map<Vm, Ent>(viewModel, ToUpdate);

                serviceResult.Success = true;
                serviceResult.Messages.Add(Error.GetErrorMessage(Error.CorrectTransaction));
                serviceResult.ResultObject = MapperHelper.Instance.Map<Ent, Vm>(this.Repository.Update(ToUpdate).Data);
            }
            catch (Exception ex)
            {
                serviceResult.LogError(ex);
            }

            return serviceResult;
        }
    }
}
