using DastakWebApi.Data;
using DastakWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Services;
public interface ILegalAssistanceService
{
    LegalAssistance GetLegalAssistanceById(int id);
}

public class LegalAssistanceService : ILegalAssistanceService
{
    private readonly DastakDbContext _context;
    public LegalAssistanceService(DastakDbContext context)
    {
        _context = context;
    }
    public LegalAssistance GetLegalAssistanceById(int id)
    {
        var data = _context.LegalAssistances
      .Where(u => u.Id == id)
      .FirstOrDefault();
        return data;
    }
}
