using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DAL;
namespace EMS.BAL;

public interface IDropDownBal
{
    int GetLocationId(string locationName);
    int GetManagerId(string managerName);
    int GetProjectId(string projectName);
    int GetDepartmentId(string departmentName);
    DropDown GetLocationByName(string userInput);
    DropDown GetDepartmentByName(string userInput);
    DropDown GetManagerByName(string userInput);
    DropDown GetProjectByName(string userInput);
    string GetNameByLocationId(int id);
    string GetNameByDepartmentId(int id);
    string GetNameByManagerId(int id);
    string GetNameByProjectId(int id);
    List<DropDown> GetDepartmentOptions();
    List<DropDown> GetLocationOptions();
    List<DropDown> GetManagerOptions();
    List<DropDown> GetProjectOptions();
}