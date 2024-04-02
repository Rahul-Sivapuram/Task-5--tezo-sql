using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using EMS.DAL;
namespace EMS.BAL;

public class DropDownBal : IDropDownBal
{
    private readonly IDropDownDal _dropDownDal;

    public DropDownBal(IDropDownDal dropDownDalObject)
    {
        _dropDownDal = dropDownDalObject;
    }

    public int GetLocationId(string locationName)
    {
        List<DropDown> items = _dropDownDal.GetLocations();
        return GetId(items, locationName);
    }

    public int GetManagerId(string managerName)
    {
        var item = _dropDownDal.GetManagers().FirstOrDefault(i => string.Equals(i.Name, managerName, StringComparison.OrdinalIgnoreCase));
        return item?.Id ?? -1;
    }

    public int GetProjectId(string projectName)
    {
        List<DropDown> items = _dropDownDal.GetProjects();
        return GetId(items, projectName);
    }

    public int GetDepartmentId(string departmentName)
    {
        List<DropDown> departmentList = _dropDownDal.GetDepartments();
        bool ans = departmentList.Any(department => department.Name == departmentName.ToUpper());
        if (ans)
        {
            return GetId(departmentList, departmentName);
        }
        else
        {
            return -1;
        }
    }

    public DropDown GetLocationByName(string locationName)
    {
        List<DropDown> data = _dropDownDal.GetLocations();
        return GetItemByName(data, locationName);
    }

    public DropDown GetDepartmentByName(string departmentName)
    {
        List<DropDown> data = _dropDownDal.GetDepartments();
        return GetItemByName(data, departmentName);
    }

    public DropDown GetManagerByName(string managerName)
    {
        List<DropDown> data = _dropDownDal.GetManagers();
        DropDown item = data.FirstOrDefault(item => managerName.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
        return item;
    }

    public DropDown GetProjectByName(string projectName)
    {
        List<DropDown> data = _dropDownDal.GetProjects();
        return GetItemByName(data, projectName);
    }

    private int GetId(List<DropDown> items, string input)
    {
        var item = items.FirstOrDefault(i => string.Equals(i.Name, input.ToUpper(), StringComparison.OrdinalIgnoreCase));
        return item?.Id ?? -1;
    }

    private DropDown GetItemByName(List<DropDown> data, string userInput)
    {
        DropDown item = data.FirstOrDefault(item => userInput.ToUpper().Equals(item.Name, StringComparison.OrdinalIgnoreCase));
        return item;
    }

    public string GetNameByLocationId(int id)
    {
        var location = _dropDownDal.GetLocations().FirstOrDefault(x => x.Id == id);
        return location != null ? location.Name : null;
    }

    public string GetNameByDepartmentId(int id)
    {
        var department = _dropDownDal.GetDepartments().FirstOrDefault(x => x.Id == id);
        return department != null ? department.Name : null;
    }

    public string GetNameByManagerId(int id)
    {   
        var manager = _dropDownDal.GetManagers().FirstOrDefault(x => x.Id == id);
        return manager != null ? manager.Name : null;
    }

    public string GetNameByProjectId(int id)
    {
        var project = _dropDownDal.GetProjects().FirstOrDefault(x => x.Id == id);
        return project != null ? project.Name : null;
    }

    public List<DropDown> GetDepartmentOptions()
    {
        return _dropDownDal.GetDepartments();
    }

    public List<DropDown> GetLocationOptions()
    {
        return _dropDownDal.GetLocations();
    }

    public List<DropDown> GetManagerOptions()
    {   
        return _dropDownDal.GetManagers();
    }

    public List<DropDown> GetProjectOptions()
    {
        return _dropDownDal.GetProjects();
    }
}
