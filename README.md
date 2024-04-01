# Introduction
- This is a Employee Management System console application.
- It supports all CRUD operations for employees.

# Prerequisites
Before running this application, ensure that you have the following prerequisites:
- .NET Core SDK installed.

# Running The Application:

You can run the application using the dotnet run command followed by various options:

display: Display all employee details.
- `EMS.exe run -- -o display`

update: Update employee information.
- `EMS.exe run -- -o update -i TZ101`

delete: Delete an employee.
- `EMS.exe run -- -o delete -i TZ101`

help: Display help information.
- `EMS.exe run -- -h help`

add: Add a new employee.
- `EMS.exe run -- -o add`

filter: Apply filters to employee data.
- `EMS.exe run -- -o filter`

add-role: Add a new role.
- `EMS.exe run -- -o add-role`

display-roles: To display all roles.
- `EMS.exe run -- -o display-roles`

Command Line Arguments.
- -o: Specifies the operation to perform (display, update, delete, help, add, filter).
- -i: Specifies the employee ID for update and delete operations.
