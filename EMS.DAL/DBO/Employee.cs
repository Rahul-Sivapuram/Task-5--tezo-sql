namespace EMS.DAL;

public class Employee
{
    public int? Id { get; set; }
    public string? EmployeeNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Dob { get; set; }
    public string? EmailId { get; set; }
    public long? MobileNumber { get; set; }
    public string? JoiningDate { get; set; }
    public int? LocationId { get; set; }
    public int? JobId { get; set; }
    public int? DeptId { get; set; }
    public bool IsManager { get; set; }
    public int? ManagerId { get; set; }
    public int? ProjectId { get; set; }
}
