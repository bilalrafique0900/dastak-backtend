using DastakWebApi.Data;
using DastakWebApi.Models;
using DastakWebApi.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Services;
public interface IMenuService
{
    List<FileViewModel> SelectFiles();
    List<FileViewModel> AllFiles();

}

public class MenuService : IMenuService
{
    private readonly DastakDbContext _context;
    public MenuService(DastakDbContext context)
    {
        _context = context;
    }

    public List<FileViewModel> SelectFiles()
    {
        // Query that manually joins File and Parent based on FileNo
        var files = (from file in _context.Files
                     join parent in _context.Parents on file.FileNo equals parent.FileNo
                     join basicinfo in _context.BasicInfos on parent.ReferenceNo equals basicinfo.ReferenceNo
                     where file.Active == 1
                     && parent.Active == 1
                     && parent.IsAdmitted == 1
                     && parent.Discharged == 0
                     select new FileViewModel
                     {
                         Id = file.Id,
                         FileNo = file.FileNo,
                         FirstName = parent.FirstName,
                         LastName = parent.LastName,
                         ReferenceNo = parent.ReferenceNo,
                         CreatedAt = file.CreatedAt,
                         CreatedBy = file.CreatedBy,
                         UpdatedAt = file.UpdatedAt,
                         UpdatedBy = file.UpdatedBy,
                         Active = file.Active,
                         AssessmentRisk = parent.AssessmentRisk,
                         ParentId = parent.Id,
                         Title = parent.Title,
                         ParentActive = parent.Active,
                         City=basicinfo.City,
                         Status = parent.Discharged == 1 ? "Closed File" :
                              parent.Pending == 1 ? "Pending File" :
                              parent.Active == 1 ? "Resident" : "Unknown"

                     }).AsNoTracking().ToList();

        return files;
    }
    public List<FileViewModel> AllFiles()
    {
        // Query that manually joins File and Parent based on FileNo
        var files = (from file in _context.Files
                     join parent in _context.Parents on file.FileNo equals parent.FileNo
                     join basicinfo in _context.BasicInfos on parent.ReferenceNo equals basicinfo.ReferenceNo
                     select new FileViewModel
                     {
                         Id = file.Id,
                         FileNo = file.FileNo,
                         FirstName = parent.FirstName,
                         LastName = parent.LastName,
                         ReferenceNo = parent.ReferenceNo,
                         CreatedAt = file.CreatedAt,
                         CreatedBy = file.CreatedBy,
                         UpdatedAt = file.UpdatedAt,
                         UpdatedBy = file.UpdatedBy,
                         Active = file.Active,
                         AssessmentRisk = parent.AssessmentRisk,
                         ParentId = parent.Id,
                         Title = parent.Title,
                         ParentActive = parent.Active,
                         City = basicinfo.City,
                              Status = parent.Discharged == 1 ? "Close File" :
                              parent.Pending == 1 ? "Pending File" :
                              parent.Active == 1 ? "Resident" : "Unknown"

                     }).ToList();

        return files;
    }

}
