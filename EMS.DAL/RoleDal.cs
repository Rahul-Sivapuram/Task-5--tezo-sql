using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Common;
using System.Data.SqlClient;
namespace EMS.DAL;

public class RoleDal : IRoleDal
{
    private readonly string _connectionString;

    public RoleDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Role> GetAll()
    {
        List<Role> roles = new List<Role>();
        SqlConnection connection = new SqlConnection(_connectionString);
        try
        {
            using (connection)
            {
                connection.Open();
                string sqlSelect = @"select * from Roles;";
                using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Role role = new Role();
                            role.Id = reader.GetInt32(0);
                            role.Name = reader.GetString(1);
                            role.DepartmentId = reader.GetInt32(2);
                            roles.Add(role);
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
        return roles;
    }

    public int Insert(Role role)
    {
        string sqlSelect = @"INSERT INTO Roles(Name, DepartmentId) VALUES 
        (@Name, @DepartmentId);";
        SqlConnection connection = new SqlConnection(_connectionString);
        try
        {
            using (connection)
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlSelect, connection))
                {
                    command.Parameters.AddWithValue("@Name", role.Name);
                    command.Parameters.AddWithValue("@DepartmentId", role.DepartmentId);
                    int rowsAffected = command.ExecuteNonQuery();
                    int insertedId = Convert.ToInt32(command.ExecuteScalar());
                    return insertedId;
                }
            }
        }
        catch (System.Exception)
        {
            throw;
            return -1;
        }
        finally
        {
            connection.Close();
        }
    }

}