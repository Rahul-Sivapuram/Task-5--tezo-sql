using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Common;

public class Constants
{
    public const string EmployeeDetailsTemplate ="EmpId: {0}\n"+"EmpNumber: {1}\n" + "EmpFirstName: {2}\n" + "EmpLastName: {3}\n" + "EmpDob: {4}\n" + "EmpEmailId: {5}\n" + "EmpMobileNumber: {6}\n" + "EmpJoiningDate: {7}\n" + "EmpLocation: {8}\n" + "EmpJobTitle: {9}\n" + "EmpDepartment: {10}\n" + "EmpManager: {11}\n" + "EmpProject: {12}\n";
    public const string EmployeeDeletedSuccessMessage = "Employee deleted successfully.";
    public const string DeletionFailedMessage = "Deletion Failed.";
    public const string EmployeeNotFoundMessage = "Employee not found.";
    public const string NoEmployeeWithIdMessage = "No employee found with EmployeeID: {0}";
    public const string EmployeeUpdatedSuccessMessage = "{0} updated successfully.";
    public const string NoEmployeeFoundMessage = "No Employee Found!";
    public const string OptionsMessage = "Options\n" + "add \t - \t To add an employee\n" + "display - \t To display all employee details\n" + "search \t - \t To display a particular employee data\n" + "delete \t - \t To delete an employee based on given employeenumber\n" + "update \t - \t To update employee details based on given employeenumber\n" + "add-role -\t To add an role\n"+"display-roles - To display all roles";
    public const string EmployeeAddedSuccessMessage = "{0} added successfully.";
    public const string InvalidOperationMessage = "Invalid operation. Valid operations are: add, display, update, delete, filter, help.";
    public const string InvalidCommandLineArgsMessage = "Invalid command-line arguments.";
    public const string RolesTemplate="RoleId : {0}\n"+"Role : {1}\n"+"Department : {2}\n";
    public const string InvalidOptionSelected = "Invalid option selected";
    public const string InsertionFailed = "Insertion Failed";
}