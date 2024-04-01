using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Common;
using System.Data.SqlClient;
namespace EMS.DAL;

public class RoleDal : IRoleDal
{
    private readonly string _filePath;
    private readonly JsonHelper _jsonHelper;
    private readonly string _connectionString;

    public RoleDal(string path, JsonHelper jsonHelperObject, string connectionString)
    {
        _filePath = path;
        _jsonHelper = jsonHelperObject;
        _connectionString = connectionString;
    }

    public List<Role> GetAll()
    {
        // string jsonData = File.ReadAllText(_filePath);
        // List<Role> roles = _jsonHelper.Deserialize<List<Role>>(jsonData);
        // return roles;
        List<Role> roles = new List<Role>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlSelect = @"select * from Role;";
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
        return roles;
    }

    public bool Insert(Role role)
    {
        // List<Role> roles = GetAll();
        // roles.Add(role);
        // _jsonHelper.Save(_filePath, roles);
        // return true;
        string sqlSelect = @"INSERT INTO Role(Name, DepartmentId) VALUES 
        (@Name, @DepartmentId);";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(sqlSelect, connection))
            {
                command.Parameters.AddWithValue("@Name", role.Name);
                command.Parameters.AddWithValue("@DepartmentId", role.DepartmentId);
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

}