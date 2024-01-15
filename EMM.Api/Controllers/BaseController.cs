using System.Linq.Expressions;
using EMM.Domain.Entities;
using EMM.Domain.Models;
using EMM.Domain.Utils;
using EMM.Infrastructure.Ef;
using Microsoft.AspNetCore.Mvc;


namespace EMM.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController<TEntity> : ControllerBase where TEntity: Entity,new()
{
    private readonly DataContext DataContext;
    public BaseController(DataContext dataContext)
    {
        DataContext = dataContext;
    }
    
    protected virtual IQueryable<TEntity> FilterPredicate(Filter filter, IQueryable<TEntity> items)
    {
        switch (filter.Name)
        {
            default: return items;
        }
    }
    
    protected virtual IQueryable<TEntity> IncludePredicate(IQueryable<TEntity> items)
    {
        return items;
    }
    
    protected virtual TEntity GetModel(Expression<Func<TEntity, bool>> expression)
    {
        var model = DataContext.Set<TEntity>().FirstOrDefault(expression);
        if (model == null) throw new NullReferenceException();
        return model;
    }
    
    [HttpGet]
    public ListResponse<TEntity> GetList( string? filter = "", string? orderField = "Id", string? orderType = "ASC",
        int? pageIndex = 1, int? pageSize = 20)
    {
        var query = DataContext.Set<TEntity>()
            .Complete(IncludePredicate)
            .Filter(filter ?? "", FilterPredicate)
            .Sort(orderField, orderType);
        
        var total = query.Count();
        List<TEntity> items = query.Paginate(pageIndex,pageSize)
            .ToList();
        
        return new ListResponse<TEntity>(total, items);
    }

    [HttpGet("{id}")]
    public TEntity GetById(long id)
    {
        var model = GetModel(u => u.Id == id);
        if (model == null) throw new NullReferenceException();
        return model;
    }
    [HttpPost]
    public TEntity Add([FromBody] TEntity model)
    {
        DataContext.Set<TEntity>().Add(model);
        DataContext.SaveChanges();
        var result = GetModel(u => u.Id == model.Id);
        return result;
    }
    [HttpPost("range")]
    public List<TEntity> AddRange([FromBody] List<TEntity> items)
    {
        DataContext.Set<TEntity>().AddRange(items);
        DataContext.SaveChanges();
        return items;
    } 

    [HttpPut]
    public TEntity Update([FromBody] TEntity model)
    {
        DataContext.Set<TEntity>().Update(model);
        DataContext.SaveChanges();
        var result = GetModel(u => u.Id == model.Id);
        return result;
    }

    [HttpDelete("{id}")]
    public bool Delete(long id)
    {
        try
        {
            var model = DataContext.Set<TEntity>().Find(id);
            if (model == null) throw new NullReferenceException();
            DataContext.Set<TEntity>().Remove(model);
            DataContext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}