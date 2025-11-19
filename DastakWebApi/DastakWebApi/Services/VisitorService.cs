using DastakWebApi.Data;
using DastakWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Services;
public interface IVisitorService
{
    Visitor GetVisitorById(int id);
}

public class VisitorService : IVisitorService
{
    private readonly DastakDbContext _context;
    public VisitorService(DastakDbContext context)
    {
        _context = context;
    }
    public Visitor GetVisitorById(int id)
    {
        var data = _context.Visitors
      .Where(u => u.Id == id)
      .FirstOrDefault();
        return data;
    }
}
