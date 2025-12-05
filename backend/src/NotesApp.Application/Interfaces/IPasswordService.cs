using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface IPasswordService
{
    string HashPassword(User user, string password);
    bool VerifyPassword(User user, string password, string hashedPassword);
}
