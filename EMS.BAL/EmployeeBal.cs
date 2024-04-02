using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using EMS.DAL;
namespace EMS.BAL;

public class EmployeeBal : IEmployeeBal
{
    private readonly IEmployeeDal _employeeDal;
    
    public EmployeeBal(IEmployeeDal employeeDalObject)
    {
        _employeeDal = employeeDalObject;
    }

    public int Add(Employee employee)
    {
        return _employeeDal.Insert(employee);
    }

    public int Delete(string employeeNumber)
    {
        return _employeeDal.Delete(employeeNumber);
    }

    public int Update(string employeeNumber, Employee employee)
    {
        int res = _employeeDal.Update(employeeNumber, employee);
        return res;
    }

    public List<EmployeeDetail> Filter(EmployeeFilter employee)
    {
        return _employeeDal.Filter(employee);
    }

    public List<EmployeeDetail> Get(string employeeNumber = "")
    {
        List<EmployeeDetail> employeeData = _employeeDal.GetAll();
        if (!string.IsNullOrEmpty(employeeNumber))
        {
            return employeeData.Where(e => e.EmployeeNumber == employeeNumber).ToList();
        }
        return employeeData;
    }
}
