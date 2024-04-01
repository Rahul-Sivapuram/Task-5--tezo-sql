using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.DAL;

public class Role
{
    public int? Id { set; get; }
    public string? Name { set; get; }
    public int? DepartmentId { set; get; }
}
