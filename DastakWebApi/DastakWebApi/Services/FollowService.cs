using DastakWebApi.Data;
using DastakWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Services;
public interface IFollowService
{
    
}

public class FollowService : IFollowService
{
    private readonly DastakDbContext _context;
    public FollowService(DastakDbContext context)
    {
        _context = context;
    }
  
}
