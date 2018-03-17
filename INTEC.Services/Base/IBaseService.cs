using System;
using INTEC.Models.Infraestructure;

namespace INTEC.Services.Base
{
    public interface IBaseService<T>
    {
        ServiceResult Insert(T viewModel);
        ServiceResult Update(T viewModel);
        ServiceResult Delete(T viewModel);
        ServiceResult GetAll();
        ServiceResult GetById(int id);
        ServiceResult GetByRowId(string RowId);
    }
}
