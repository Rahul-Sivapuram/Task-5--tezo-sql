using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.DAL;
namespace EMS.BAL;

public interface IEmployeeBal
{
    int Add(Employee employee);
    int Delete(string employeeNumber);
    int Update(string employeeNumber, Employee employee);
    List<EmployeeDetail> Filter(EmployeeFilter employee);
    List<EmployeeDetail> Get(string? employeeNumber);
}
