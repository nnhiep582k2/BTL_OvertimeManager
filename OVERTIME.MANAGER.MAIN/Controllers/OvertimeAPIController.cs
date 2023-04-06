﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OVERTIME.MANAGER.MAIN.Models;
using OVERTIME.MANAGER.MAIN.ViewModels;
using X.PagedList;

#nullable disable
namespace OVERTIME.MANAGER.MAIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeAPIController : ControllerBase
    {
        private readonly OvertimeManagerContext db = new OvertimeManagerContext();

        [HttpGet]
        [Route("searchQuery={searchQuery}&pageIndex={pageIndex}&pageSize={pageSize}")]
        public ListItem<Employee> GetByFilter(string searchQuery = "", int pageIndex = 1, int pageSize = 15)
        {
            var lstEmployee = db.Employees.AsNoTracking().Where(x => x.EmployeeName.ToLower().Contains(searchQuery.ToLower()) || x.EmployeeCode.ToLower().Contains(searchQuery.ToLower()) || x.JobPositionName.ToLower().Contains(searchQuery.ToLower()) || x.OrganizationName.ToLower().Contains(searchQuery.ToLower())).OrderByDescending(x => x.ModifiedDate);

            ListItem<Employee> lst = new ListItem<Employee>()
            {
                list = new PagedList<Employee>(lstEmployee, pageIndex, pageSize),
                searchQuery = searchQuery,
                pageIndex = pageIndex,
                pageSize = pageSize,
                totalRecord = lstEmployee.Count(),
            };

            return lst;
        }
    }
}
