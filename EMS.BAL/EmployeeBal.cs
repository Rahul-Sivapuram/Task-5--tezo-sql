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
    private readonly IRoleBal _roleBal;
    private readonly IDropDownBal _dropDownBal;
    public EmployeeBal(IEmployeeDal employeeDalObject, IRoleBal roleBalObject, IDropDownBal dropDownBalObject)
    {
        _employeeDal = employeeDalObject;
        _roleBal = roleBalObject;
        _dropDownBal = dropDownBalObject;
    }

    public bool Add(Employee employee)
    {
        return _employeeDal.Insert(employee);
    }

    public bool Delete(string employeeNumber)
    {
        return _employeeDal.Delete(employeeNumber);
    }

    public bool Update(string employeeNumber, Employee employee)
    {
        bool res = _employeeDal.Update(employeeNumber, employee);
        return res;
    }

    public List<EmployeeDetail> Filter(EmployeeFilter employee)
    {
        return _employeeDal.Filter(employee, Get());
    }

    public List<EmployeeDetail> Get(string employeeNumber = "")
    {
        List<Employee> employeeData = _employeeDal.GetAll();
        List<EmployeeDetail> employeeDetailsList = new List<EmployeeDetail>();
        foreach (var employee in employeeData)
        {

            EmployeeDetail employeeDetail = new EmployeeDetail
            {
                EmployeeNumber = employee.EmployeeNumber,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Dob = employee.Dob,
                EmailId = employee.EmailId,
                MobileNumber = employee.MobileNumber,
                JoiningDate = employee.JoiningDate,
            };
            employeeDetail.LocationName = _dropDownBal.GetNameByLocationId((int)employee.LocationId) ?? "Unknown";
            employeeDetail.JobName = _roleBal.GetNameByRoleId((int)employee.JobId) ?? "Unknown";
            employeeDetail.DeptName = _dropDownBal.GetNameByDepartmentId((int)employee.DeptId) ?? "Unknown";
            employeeDetail.ManagerName = _dropDownBal.GetNameByManagerId(employee.ManagerId ?? 0) ?? employeeDetail.FirstName + " " + employeeDetail.LastName;
            employeeDetail.ProjectName = _dropDownBal.GetNameByProjectId((int)employee.ProjectId) ?? "Unknown";
            employeeDetailsList.Add(employeeDetail);
        }
        if (!string.IsNullOrEmpty(employeeNumber))
        {
            return employeeDetailsList.Where(e => e.EmployeeNumber == employeeNumber).ToList();
        }

        return employeeDetailsList;
    }
}
