using DastakWebApi.Data;
using DastakWebApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DastakWebApi.Services;
public interface IUserService
{
    User GetUserById(int id);
    User GetUserByEmail(string email);
    User GetCurrentUserData(int id);

    void UpdateUser(User user);
    void AddUser(User user);
}

public class UserService : IUserService
{
    private readonly DastakDbContext _context;
    public UserService(DastakDbContext context)
    {
        _context = context;
    }
    public User GetUserById(int id)
    {
        var data = _context.Users
      .Where(u => u.Id == id)
      .FirstOrDefault();
        return data;
    }
    public User GetUserByEmail(string email)
    {
        var data = _context.Users
      .Where(u => u.Email == email)
      .FirstOrDefault();
        return data;
    }

    public User GetCurrentUserData(int id)
    {
        var data =  _context.Users
      .Where(u => u.Id==id)
      .FirstOrDefault();
        return data; 
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
        // Update user in the database
    }
    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        // Add user in the database
    }
}
