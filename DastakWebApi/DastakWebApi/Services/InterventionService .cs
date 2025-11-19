using DastakWebApi.Data;
using DastakWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Services;
public interface IInterventionService
{
    
}

public class InterventionService : IInterventionService
{
    private readonly DastakDbContext _context;
    public InterventionService(DastakDbContext context)
    {
        _context = context;
    }
    
}
