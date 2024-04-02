using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data.SqlClient;
using EMS.Common;
namespace EMS.DAL;

public class DropDownDal : IDropDownDal
{
    private readonly string _connectionString;
    private readonly IEmployeeDal _employeeDal;

    public DropDownDal(string connectionString, IEmployeeDal employeeDalObject)
    {
        _connectionString = connectionString;
        _employeeDal = employeeDalObject;
    }

    private List<DropDown> GetDropDownItems(string filePath)
    {
        List<DropDown> data = new List<DropDown>();
        SqlConnection conn = new SqlConnection(_connectionString);
        try
        {
            using (conn)
            {
                conn.Open();
                string sqlSelect = @"select * from " + filePath;
                using (SqlCommand command = new SqlCommand(sqlSelect, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DropDown item = new DropDown();
                            item.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            item.Name = reader.GetString(reader.GetOrdinal("Name"));
                            data.Add(item);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
        return data;
    }

    public List<DropDown> GetLocations()
    {
        return GetDropDownItems("Location");
    }

    public List<DropDown> GetDepartments()
    {
        return GetDropDownItems("Department");
    }

    public List<DropDown> GetManagers()
    {
        List<DropDown> empData = new List<DropDown>();
        string sqlSelect = @"SELECT 
        emp.Id, emp.Uid, emp.FirstName, emp.LastName, emp.Dob as DOB, emp.EmailId, emp.MobileNumber, emp.JoiningDate, Location.Name as Location, Role.Name as Role, Department.Name as Department,CONCAT(manager.FirstName, ' ', manager.LastName) as Manager,Project.Name as Project  
        FROM Employee as emp LEFT JOIN Employee as manager ON emp.ManagerId = manager.Id
        JOIN Location ON emp.LocationId = Location.Id JOIN Role ON emp.RoleId = Role.Id
        JOIN Department ON emp.DepartmentId = Department.Id JOIN Project ON emp.ProjectId = Project.Id
        WHERE emp.ManagerId IS NULL";
        SqlConnection conn = new SqlConnection(_connectionString);
        try
        {
            using (conn)
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sqlSelect, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DropDown manager = new DropDown();
                            manager.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            manager.Name = reader.GetString(reader.GetOrdinal("FirstName")) + " " + reader.GetString(reader.GetOrdinal("LastName"));
                            empData.Add(manager);
                        }
                    }
                }
            }
        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
        return empData;
    }

    public List<DropDown> GetProjects()
    {
        return GetDropDownItems("Project");
    }
}