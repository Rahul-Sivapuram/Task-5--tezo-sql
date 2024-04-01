using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DAL;
namespace EMS.BAL;

public class RoleBal : IRoleBal
{
    private readonly IRoleDal _roleDal;
    public RoleBal(IRoleDal roleDalObject)
    {
        _roleDal = roleDalObject;
    }

    public bool Insert(Role role)
    {
        bool res = false;
        List<Role> rolesList = _roleDal.GetAll();

        if (!rolesList.Exists(item => item.Name == role.Name))
        {
            role.Id = rolesList.Count + 1;
            _roleDal.Insert(role);
            res = true;
        }
        return res;
    }

    public List<string> GetRoleNamesForDepartment(int Id)
    {
        List<Role> roles = _roleDal.GetAll();
        List<string> roleNames = new List<string>();
        foreach (var role in roles)
        {
            if (role.DepartmentId == Id)
            {
                roleNames.Add(role.Name);
            }
        }
        return roleNames;
    }

    public int GetRoleId(string roleName)
    {
        List<Role> roles = Get();
        var item = roles.FirstOrDefault(i => string.Equals(i.Name, roleName.ToUpper(), StringComparison.OrdinalIgnoreCase));
        return item?.Id ?? -1;
    }

    public Role GetRoleByName(string userInput)
    {
        List<Role> roles = Get();
        Role item = roles.FirstOrDefault(item => userInput.ToUpper().Equals(item.Name, StringComparison.OrdinalIgnoreCase));
        return item;
    }

    public string GetNameByRoleId(int id)
    {
        List<Role> roles = _roleDal.GetAll();
        Role roleName = roles.FirstOrDefault(x => (int)x.Id == id);
        return roleName != null ? roleName.Name : null;
    }
    
    public List<Role> Get()
    {
        List<Role> roles = _roleDal.GetAll();
        return roles;
    }
}