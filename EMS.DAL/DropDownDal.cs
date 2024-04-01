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
    private readonly JsonHelper _jsonHelper;
    private readonly IConfigurationRoot _configuration;
    private readonly string _connectionString;
    private readonly IEmployeeDal _employeeDal;
    public DropDownDal(IConfigurationRoot configurationObject, JsonHelper jsonHelperObject, string connectionString,IEmployeeDal employeeDalObject)
    {
        _configuration = configurationObject;
        _jsonHelper = jsonHelperObject;
        _connectionString = connectionString;
        _employeeDal = employeeDalObject;
    }

    private List<DropDown> GetDropDownItems(string filePath)
    {
        List<DropDown> data = new List<DropDown>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
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
                        item.Id = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        data.Add(item);
                    }
                }
            }
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

    public List<Employee> GetManagers()
    {   
         List<Employee> managers = new List<Employee>();
         _employeeDal.GetAll().ForEach(e => {
            if(e.IsManager)
            {
                managers.Add(e);
            }
         });
         return managers;
    }

    public List<DropDown> GetProjects()
    {
        return GetDropDownItems("Project");
    }
}