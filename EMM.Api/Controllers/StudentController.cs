using EMM.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMM.Domain.Entities;

namespace EMM.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : BaseController<User>
{
    public StudentController(DataContext dataContext) : base(dataContext)
    {
    }
}