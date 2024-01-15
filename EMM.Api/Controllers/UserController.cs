using EMM.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMM.Domain.Entities;

namespace EMM.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController<User>
{
    public UserController(DataContext dataContext) : base(dataContext)
    {
    }
}