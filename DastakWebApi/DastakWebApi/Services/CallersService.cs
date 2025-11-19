using DastakWebApi.Data;
using DastakWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Services;
public interface ICallersService
{
    Caller GetCallersById(int id);
}

public class CallersService : ICallersService
{
    private readonly DastakDbContext _context;
    public CallersService(DastakDbContext context)
    {
        _context = context;
    }
    public Caller GetCallersById(int id)
    {
        var data = _context.Callers
      .Where(u => u.Id == id)
      .FirstOrDefault();
        return data;
    }
}
