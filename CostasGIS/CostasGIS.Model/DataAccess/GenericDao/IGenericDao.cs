using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.DataAccess.GenericDao
{
    internal interface IGenericDao<E, PK>
    {
        E Create(E entity);

        E Find(PK id);

        IEnumerable<E> FindAll();

        Boolean Exists(PK id);
  
        E Update(E entity);

        void Remove(PK id);
    }
}
