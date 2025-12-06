namespace NotesApp.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChagangesAsync(CancellationToken cancellationToken = default);
}
