using System;
using System.Collections.Generic;
using System.Text;

namespace Reviewer.DAL.Interfaces
{
    public interface IDbTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
