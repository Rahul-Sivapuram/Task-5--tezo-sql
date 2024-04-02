using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.DAL;

public class EmployeeFilter
{
    public DropDown? Location { get; set; }
    public Role? JobTitle { get; set; }
    public DropDown? Department { get; set; }
    public DropDown? Manager { get; set; }
    public DropDown? Project { get; set; }
    public string? EmployeeName { get; set; }
}
