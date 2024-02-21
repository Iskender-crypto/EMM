using System.Text.Json;
using EMM.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMM.Domain.Entities;
using EMM.Domain.Models;
using Microsoft.AspNetCore.Authentication;

namespace EMM.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : BaseController<Student>
{
    public StudentController(DataContext dataContext) : base(dataContext)
    {
    }
}