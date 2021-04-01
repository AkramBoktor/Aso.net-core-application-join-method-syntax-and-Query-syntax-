using System;
using System.Collections.Generic;

using System.Linq;

namespace Video_LinQ_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeelist = Data.GetEmployees();
            List<Department> departmentlist = Data.GetDepartments();

            //method syntax 
            var resultfilters = employeelist.Select(emp => new
            {
                fullname = emp.FirstName + " " + emp.LastName,
                AnnualSalary = emp.AnuualSalary,
                DepartmentId = emp.DepartmentId,
                Manager = emp.IsManager
            }).Where(emp=> emp.AnnualSalary == 60000.3m) ;

            foreach (var result in resultfilters)
            {
                Console.WriteLine($"1 - FullName {result.fullname} - AnualSalary{result.AnnualSalary,10} - Department id  : {result.DepartmentId} - Manager : {result.Manager}");
            }
            Console.WriteLine($"-------------------------------------------------------------------------------------");

            //Query Syntax

            var resultfilters2 = from emp in employeelist
                                 where emp.AnuualSalary == 60000.3m
                                 select new
                                 {
                                     fullname = emp.FirstName + " " + emp.LastName,
                                     AnnualSalary = emp.AnuualSalary
                                 };
            foreach (var result in resultfilters2)
            {
                Console.WriteLine($"2- FullName {result.fullname} - AnualSalary{result.AnnualSalary,10}");
            }

            Console.WriteLine($"-------------------------------------------------------------------------------------");

            //Query Syntax Deffered (Lazy Evaluation) will amir print
            var resultfilters3 = from emp in employeelist
                                 where emp.AnuualSalary == 60000.3m
                                 select new
                                 {
                                     fullname = emp.FirstName + " " + emp.LastName,
                                     AnnualSalary = emp.AnuualSalary
                                 };

            employeelist.Add(new Employee
            {
                Id = 5,
                AnuualSalary = 60000.3m,
                DepartmentId = 2,
                FirstName = "Amir",
                LastName = "Boktor",
                IsManager = true
            });

            foreach (var result in resultfilters3)
            {
                Console.WriteLine($"3 -FullName {result.fullname} - AnualSalary{result.AnnualSalary,10}");
            }

            Console.WriteLine($"-------------------------------------------------------------------------------------");

            //Query Syntax Immediate  will not amir print
            var resultfilters4 = (from emp in employeelist
                                 where emp.AnuualSalary == 60000.3m
                                 select new
                                 {
                                     fullname = emp.FirstName + " " + emp.LastName,
                                     AnnualSalary = emp.AnuualSalary
                                 }).ToList();

            employeelist.Add(new Employee
            {
                Id = 5,
                AnuualSalary = 60000.3m,
                DepartmentId = 2,
                FirstName = "Amir",
                LastName = "Boktor",
                IsManager = true
            });

            foreach (var result in resultfilters4)
            {
                Console.WriteLine($"4 -FullName {result.fullname} - AnualSalary{result.AnnualSalary,10}");
            }
            Console.WriteLine($"-------------------------------------------------------------------------------------");

            ///////////////////////////////////////////////// Join ////////////////////////////////////////
            ////// inner join 
            ///join method syntax
            var resultjoin = departmentlist.Join(employeelist,
                department => department.Id,
                employee => employee.DepartmentId,
                (department, employee) => new
                {
                    fullName = employee.FirstName+ " " + employee.LastName,
                    Anuualsalary = employee.AnuualSalary,
                    Department = department.LongName

                });
            foreach (var result in resultjoin)
            {
                Console.WriteLine($"Join -FullName {result.fullName} - AnualSalary{result.Anuualsalary,10} - Department:{result.Department}");
            }

            Console.WriteLine($"-------------------------------------------------------------------------------------");

            ///join Query syntax
            var resultjoin2 = from dept in departmentlist
                              join emp in employeelist
                             on dept.Id equals emp.DepartmentId
                              select new
                              {
                                  fullName = emp.FirstName + " " + emp.LastName,
                                  Anuualsalary = emp.AnuualSalary,
                                  Department = dept.LongName
                              };
            foreach (var result in resultjoin2)
            {
                Console.WriteLine($"Join -FullName {result.fullName} - AnualSalary{result.Anuualsalary,10} - Department:{result.Department}");
            }

            ////////////// left outer join method syntax
            var resultleftjoin = departmentlist.GroupJoin(employeelist,
            department => department.Id,
                employee => employee.DepartmentId,
                (department, employeeGroup) => new
                {
                   employee = employeeGroup,
                    Department = department.LongName

                });
            foreach (var result in resultleftjoin)
            {
                Console.WriteLine($"Group join Department Name :{result.Department}");
                foreach (var item in result.employee)
                {
                    Console.WriteLine($" \t Employee Name :{item.FirstName} {item.LastName} ");

                }
            }

            ////////////// left outer join Query syntax
            var resultleftjoin2 = from dept in departmentlist
                              join emp in employeelist
                             on dept.Id equals emp.DepartmentId
                             into employeeGroup
                              select new
                              {
                                  employee = employeeGroup,
                                  Department = dept.LongName
                              };
            foreach (var result in resultleftjoin)
            {
                Console.WriteLine($"Group join left Department Name :{result.Department}");
                foreach (var item in result.employee)
                {
                    Console.WriteLine($" \t Employee Name :{item.FirstName} {item.LastName} ");

                }
            }
                Console.ReadKey();


        }
    }
        public class Employee
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public decimal AnuualSalary { get; set; }

            public bool IsManager { get; set; }

            public int DepartmentId { get; set; }

        }

        public class Department
        {
            public int Id { get; set; }
            public string ShortName { get; set; }
            public string LongName { get; set; }
        }

        public static class Data
        {
            public static List<Employee> GetEmployees()
            {
                List<Employee> employees = new List<Employee>();
                Employee employee = new Employee()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Jones",
                    AnuualSalary = 60000.3m,
                    IsManager = true,
                    DepartmentId = 1
                };
                employees.Add(employee);
                employee = new Employee()
                {
                    Id = 2,
                    FirstName = "Sarah",
                    LastName = "Gendy",
                    AnuualSalary = 4000.7m,
                    IsManager = true,
                    DepartmentId = 2
                };
                employees.Add(employee);
                employee = new Employee()
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Gad",
                    AnuualSalary = 5000.7m,
                    IsManager = false,
                    DepartmentId = 2
                };
                employees.Add(employee);
                employee = new Employee()
                {
                    Id = 4,
                    FirstName = "Peter",
                    LastName = "Samuel",
                    AnuualSalary = 8000.7m,
                    IsManager = false,
                    DepartmentId = 1
                };
                employees.Add(employee);
                return employees;
            }


            public static List<Department> GetDepartments()
            {
                List<Department> departments = new List<Department>();
                Department department = new Department()
                {
                    Id = 1,
                    ShortName = "HR",
                    LongName = "Human Resource"
                };
                departments.Add(department);
                department = new Department()
                {
                    Id = 2,
                    ShortName = "FN",
                    LongName = "Financie"
                };
                departments.Add(department);
                department = new Department()
                {
                    Id = 3,
                    ShortName = "TE",
                    LongName = "Technology"
                };
                departments.Add(department);
                return departments;
            }

        }

    
}
