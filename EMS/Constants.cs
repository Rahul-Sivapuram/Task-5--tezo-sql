using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Common;

public class Constants
{
    public const string EmployeeDetailsTemplate ="EmpNumber: {0}\n" + "EmpFirstName: {1}\n" + "EmpLastName: {2}\n" + "EmpDob: {3}\n" + "EmpEmailId: {4}\n" + "EmpMobileNumber: {5}\n" + "EmpJoiningDate: {6}\n" + "EmpLocation: {7}\n" + "EmpJobTitle: {8}\n" + "EmpDepartment: {9}\n" + "EmpManager: {10}\n" + "EmpProject: {11}\n";
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