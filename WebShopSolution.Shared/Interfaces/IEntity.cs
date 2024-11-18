namespace WebShopSolution.Shared.Interfaces;

public interface IEntity<T>
{
    T Id { get; set; }
}