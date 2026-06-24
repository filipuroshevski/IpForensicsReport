using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void StartTransaction();
        void Commit();
    }
}
