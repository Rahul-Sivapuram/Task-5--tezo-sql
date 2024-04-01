using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EMS.DAL;

public interface IDropDownDal
{
    List<DropDown> GetLocations();
    List<DropDown> GetDepartments();
    List<Employee> GetManagers();
    List<DropDown> GetProjects();
}
