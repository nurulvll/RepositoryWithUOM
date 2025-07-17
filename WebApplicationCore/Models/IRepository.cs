using Core.Interface;
using System;

namespace WebApplicationCore.Models
{
    public interface IRepository
    {
        IUnitOfWork IUnitOfWork { set; get; }

    }
}
