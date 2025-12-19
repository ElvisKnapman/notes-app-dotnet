namespace NotesApp.Application.Interfaces;

public interface ITokenStore
{
    public void Set(string token);
    public void Clear();
}
