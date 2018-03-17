using System;
using INTEC.Models.Infraestructure;

namespace INTEC.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static ServiceResult LogError(this ServiceResult sr, Exception ex)
        {
            sr.Success = false;
            sr.ResultObject = "REVISE EL LOG DEL SISTEMA";
            sr.Messages.Add(ex.Message);

            return sr;
        }
    }
}