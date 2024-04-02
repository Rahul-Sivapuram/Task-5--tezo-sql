using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EMS.DAL;

public interface IDropDownDal
{
    List<DropDown> GetLocations();
    List<DropDown> GetDepartments();
    List<DropDown> GetManagers();
    List<DropDown> GetProjects();
}
