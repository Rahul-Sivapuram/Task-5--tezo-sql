using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.DAL;

public interface IEmployeeDal
{
    List<EmployeeDetail> GetAll();
    int Insert(Employee data);
    int Update(string employeeNumber, Employee employee);
    int Delete(string employeeId);
    List<EmployeeDetail> Filter(EmployeeFilter? employee);
}
