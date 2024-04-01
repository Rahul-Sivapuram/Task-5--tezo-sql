using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EMS.Common;
namespace EMS.DAL;

public class EmployeeDal : IEmployeeDal
{
    private readonly string _filePath;
    private readonly JsonHelper _jsonHelper;
    private readonly string _connectionString;

    public EmployeeDal(string path, JsonHelper jsonHelperObject, string connectionString)
    {
        _filePath = path;
        _jsonHelper = jsonHelperObject;
        _connectionString = connectionString;
    }

    public List<Employee> GetAll()
    {
        List<Employee> empData = new List<Employee>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string sqlSelect = @"select * from Employee;";
            using (SqlCommand command = new SqlCommand(sqlSelect, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.Id = reader.GetInt32(0);
                        emp.EmployeeNumber = reader.GetString(1);
                        emp.FirstName = reader.GetString(2);
                        emp.LastName = reader.GetString(3);
                        emp.Dob = reader.GetDateTime(4).ToString("dd-MM-yyyy");
                        emp.EmailId = reader.GetString(5);
                        emp.MobileNumber = reader.GetInt64(6);
                        emp.JoiningDate = reader.GetDateTime(7).ToString("dd-MM-yyyy");
                        emp.LocationId = reader.GetInt32(8);
                        emp.JobId = reader.GetInt32(9);
                        emp.DeptId = reader.GetInt32(10);
                        emp.IsManager = reader.GetBoolean(11);
                        emp.ManagerId = reader.IsDBNull(12) ? null : reader.GetInt32(12);
                        emp.ProjectId = reader.GetInt32(13);
                        empData.Add(emp);
                    }
                }
            }
            return empData;
        }
    }

    public bool Insert(Employee employee)
    {
        string sqlSelect = @"INSERT INTO Employee(Uid, FirstName, LastName, DOB, EmailId, MobileNumber, JoiningDate, LocationId, RoleId, DepartmentId, IsManager, ManagerId, ProjectId) VALUES 
        (@Uid, @FirstName, @LastName, @DOB, @EmailId, @MobileNumber, @JoiningDate, @LocationId, @RoleId, @DepartmentId, @IsManager, @ManagerId, @ProjectId);";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(sqlSelect, connection))
            {
                command.Parameters.AddWithValue("@Uid", employee.EmployeeNumber);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DOB", employee.Dob);
                command.Parameters.AddWithValue("@EmailId", employee.EmailId);
                command.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                command.Parameters.AddWithValue("@LocationId", employee.LocationId);
                command.Parameters.AddWithValue("@RoleId", employee.JobId);
                command.Parameters.AddWithValue("@DepartmentId", employee.DeptId);
                command.Parameters.AddWithValue("@IsManager", employee.IsManager);
                command.Parameters.AddWithValue("@ManagerId", (object)employee.ManagerId ?? DBNull.Value);
                command.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public bool Insert(List<Employee> employeeData)
    {
        _jsonHelper.Save(_filePath, employeeData);
        return true;
    }

    public bool Update(string employeeNumber, Employee employee)
    {
        string sqlUpdate = @"UPDATE Employee SET FirstName = @FirstName,LastName = @LastName,DOB = @DOB,EmailId = @EmailId,
        MobileNumber = @MobileNumber,JoiningDate = @JoiningDate,LocationId = @LocationId,RoleId = @RoleId,DepartmentId = @DepartmentId,IsManager = @IsManager,ManagerId = @ManagerId,ProjectId = @ProjectId
        WHERE Uid = @Uid";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
            {
                command.Parameters.AddWithValue("@Uid", employeeNumber);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DOB", employee.Dob);
                command.Parameters.AddWithValue("@EmailId", employee.EmailId);
                command.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);
                command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                command.Parameters.AddWithValue("@LocationId", employee.LocationId);
                command.Parameters.AddWithValue("@RoleId", employee.JobId);
                command.Parameters.AddWithValue("@DepartmentId", employee.DeptId);
                command.Parameters.AddWithValue("@IsManager", employee.IsManager);
                command.Parameters.AddWithValue("@ManagerId", (object)employee.ManagerId ?? DBNull.Value);
                command.Parameters.AddWithValue("@ProjectId", employee.ProjectId);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }

    public bool Delete(string employeeNumber)
    {
        string sqlSelect = @"delete from Employee where Uid=@Uid";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(sqlSelect, connection))
            {
                command.Parameters.AddWithValue("@Uid", employeeNumber);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    public List<EmployeeDetail> Filter(EmployeeFilter? employee, List<EmployeeDetail> employeeData)
    {
        var filteredEmployees = employeeData.Where(emp => IsEmployeeFiltered(emp, employee)).ToList();
        return filteredEmployees;
    }

    private bool IsEmployeeFiltered(EmployeeDetail emp, EmployeeFilter employee)
    {
        bool filterEmployeeName = string.IsNullOrEmpty(employee.EmployeeName) || emp.FirstName.StartsWith(employee.EmployeeName);
        bool filterLocation = employee.Location == null || emp.LocationName == employee.Location.Name;
        bool filterJobTitle = employee.JobTitle == null || emp.JobName == employee.JobTitle.Name;
        bool filterManager = employee.Manager == null || emp.ManagerName == employee.Manager.FirstName + " " + employee.Manager.LastName;
        bool filterProject = employee.Project == null || emp.ProjectName == employee.Project.Name;
        return filterEmployeeName && filterLocation && filterJobTitle && filterManager && filterProject;
    }
}
