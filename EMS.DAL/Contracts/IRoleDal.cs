using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.DAL;

public interface IRoleDal
{
    List<Role> GetAll();
    bool Insert(Role role);
}
