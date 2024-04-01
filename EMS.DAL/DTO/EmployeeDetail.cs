using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.DAL;

public class EmployeeDetail
{   
    public int? Id { get; set; }
    public string? EmployeeNumber {get; set; }
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public string? Dob {get; set;}
    public string? EmailId {get; set;}
    public long? MobileNumber {get; set;}
    public string? JoiningDate {get; set;}
    public string? LocationName {get; set;}
    public string? JobName {get; set;}
    public string? DeptName {get; set;}
    public string? ManagerName {get; set;}
    public string? ProjectName {get; set;}
}
