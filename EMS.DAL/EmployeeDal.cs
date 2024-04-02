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
    private readonly string _connectionString;

    public EmployeeDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<EmployeeDetail> GetAll()
    {
        List<EmployeeDetail> empData = new List<EmployeeDetail>();
        SqlConnection conn = new SqlConnection(_connectionString);
        try
        {
            using (conn)
            {
                conn.Open();
                string sqlSelect = @"SELECT 
                emp.Id, emp.Uid, emp.FirstName, emp.LastName, emp.Dob as DOB, emp.EmailId, emp.MobileNumber, emp.JoiningDate, 
                Location.Name as Location, 
                Role.Name as Role, 
                Department.Name as Department,
                CONCAT(manager.FirstName, ' ', manager.LastName) as Manager,
                Project.Name as Project  
                FROM 
                    Employee as emp
                LEFT JOIN Employee as manager ON emp.ManagerId = manager.Id
                JOIN Location ON emp.LocationId = Location.Id
                JOIN Role ON emp.RoleId = Role.Id
                JOIN Department ON emp.DepartmentId = Department.Id
                JOIN Project ON emp.ProjectId = Project.Id;";
                using (SqlCommand command = new SqlCommand(sqlSelect, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeDetail emp = ReadEmployeeDetail(reader);
                            empData.Add(emp);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
        return empData;
    }

    public int Insert(Employee employee)
    {
        string sqlSelect = @"INSERT INTO Employee(Uid, FirstName, LastName, DOB, EmailId, MobileNumber, JoiningDate, LocationId, RoleId, DepartmentId, IsManager, ManagerId, ProjectId) VALUES 
        (@Uid, @FirstName, @LastName, @DOB, @EmailId, @MobileNumber, @JoiningDate, @LocationId, @RoleId, @DepartmentId, @IsManager, @ManagerId, @ProjectId);";

        SqlConnection connection = new SqlConnection(_connectionString);
        try
        {
            using (connection)
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

                    int insertedId = Convert.ToInt32(command.ExecuteScalar());
                    return insertedId;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            connection.Close();
        }
    }


    public int Update(string employeeNumber, Employee employee)
    {
        string sqlUpdate = @"UPDATE Employee SET FirstName = @FirstName,LastName = @LastName,DOB = @DOB,EmailId = @EmailId,
        MobileNumber = @MobileNumber,JoiningDate = @JoiningDate,LocationId = @LocationId,RoleId = @RoleId,DepartmentId = @DepartmentId,IsManager = @IsManager,ManagerId = @ManagerId,ProjectId = @ProjectId
        WHERE Uid = @Uid";
        SqlConnection connection = new SqlConnection(_connectionString);
        try
        {
            using (connection)
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
                    return rowsAffected;
                }
            }
        }
        catch (Exception e)
        {
            return -1;
        }
        finally
        {
            connection.Close();
        }
    }

    public int Delete(string employeeId)
    {
        string sqlSelect = @"delete from Employee where Id=@Id";
        SqlConnection connection = new SqlConnection(_connectionString);
        try
        {
            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                {
                    command.Parameters.AddWithValue("@Id", employeeId);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
        catch (Exception e)
        {
            return -1;
        }
        finally
        {
            connection.Close();
        }
    }

    public List<EmployeeDetail> Filter(EmployeeFilter? employee)
    {
        string sqlSelect = @"SELECT 
        emp.Id, emp.Uid, emp.FirstName, emp.LastName, emp.Dob as DOB, emp.EmailId, emp.MobileNumber, emp.JoiningDate, 
        Location.Name as Location, 
        Role.Name as Role, 
        Department.Name as Department,
        CONCAT(manager.FirstName, ' ', manager.LastName) as Manager,
        Project.Name as Project  
        FROM 
            Employee as emp
        LEFT JOIN Employee as manager ON emp.ManagerId = manager.Id
        JOIN Location ON emp.LocationId = Location.Id
        JOIN Role ON emp.RoleId = Role.Id
        JOIN Department ON emp.DepartmentId = Department.Id
        JOIN Project ON emp.ProjectId = Project.Id";

        List<string> conditions = new List<string>();
        if (employee != null)
        {
            if (!string.IsNullOrEmpty(employee.EmployeeName))
                conditions.Add($"emp.FirstName LIKE '{employee.EmployeeName}%'");
            if (employee.Location != null)
                conditions.Add($"Location.Name = '{employee.Location.Name}'");
            if (employee.JobTitle != null)
                conditions.Add($"Role.Name = '{employee.JobTitle.Name}'");
            if (employee.Manager != null)
                conditions.Add($"(manager.FirstName + ' ' + manager.LastName) = '{employee.Manager.Name}'");
            if (employee.Project != null)
                conditions.Add($"Project.Name = '{employee.Project.Name}'");
        }
        if (conditions.Any())
            sqlSelect += " WHERE " + string.Join(" AND ", conditions);

        List<EmployeeDetail> filteredEmployees = new List<EmployeeDetail>();
        SqlConnection connection = new SqlConnection(_connectionString);
        try
        {
            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeDetail emp = ReadEmployeeDetail(reader);
                            filteredEmployees.Add(emp);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw;
        }
        finally
        {
            connection.Close();
        }
        return filteredEmployees;
    }

    private T GetValueOrDefault<T>(SqlDataReader reader, string columnName, T defaultValue = default(T))
    {
        int ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? defaultValue : reader.GetFieldValue<T>(ordinal);
    }

    private EmployeeDetail ReadEmployeeDetail(SqlDataReader reader)
    {
        EmployeeDetail emp = new EmployeeDetail();
        emp.Id = GetValueOrDefault(reader, "Id", 0);
        emp.EmployeeNumber = GetValueOrDefault(reader, "Uid", string.Empty);
        emp.FirstName = GetValueOrDefault(reader, "FirstName", string.Empty);
        emp.LastName = GetValueOrDefault(reader, "LastName", string.Empty);
        emp.Dob = GetValueOrDefault(reader, "DOB", DateTime.MinValue).ToString("dd-MM-yyyy");
        emp.EmailId = GetValueOrDefault(reader, "EmailId", string.Empty);
        emp.MobileNumber = GetValueOrDefault(reader, "MobileNumber", 0L);
        emp.JoiningDate = GetValueOrDefault(reader, "JoiningDate", DateTime.MinValue).ToString("dd-MM-yyyy");
        emp.LocationName = GetValueOrDefault(reader, "Location", string.Empty);
        emp.JobName = GetValueOrDefault(reader, "Role", string.Empty);
        emp.DeptName = GetValueOrDefault(reader, "Department", string.Empty);
        emp.ManagerName = GetValueOrDefault(reader, "Manager", string.Empty);
        emp.ProjectName = GetValueOrDefault(reader, "Project", string.Empty);
        return emp;
    }
}
