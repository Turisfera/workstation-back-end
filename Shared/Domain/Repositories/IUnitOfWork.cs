namespace tripmatch_back.Shared.Domain;

public interface IUnitOfWork
{
    Task CompleteAsync();
}