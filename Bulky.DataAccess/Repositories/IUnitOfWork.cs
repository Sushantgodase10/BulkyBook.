using Bulky.DataAccess.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        public CategoryRepository CategoryRepository{ get; }
        public ProductRepository ProductRepository { get; }

        void Save();
    }
}
