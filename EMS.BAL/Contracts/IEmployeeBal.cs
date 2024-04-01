using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DAL;
namespace EMS.BAL;

public interface IEmployeeBal
{
    bool Add(Employee employee);
    bool Delete(string employeeNumber);
    bool Update(string employeeNumber, Employee employee);
    List<EmployeeDetail> Filter(EmployeeFilter employee);
    List<EmployeeDetail> Get(string? employeeNumber);
}
