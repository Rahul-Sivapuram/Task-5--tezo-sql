using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DAL;
namespace EMS.BAL;

public interface IRoleBal
{
    bool Insert(Role role);
    List<string> GetRoleNamesForDepartment(int id);
    int GetRoleId(string roleName);
    Role GetRoleByName(string userInput);
    string GetNameByRoleId(int id);
    List<Role> Get();
}
