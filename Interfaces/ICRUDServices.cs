
using ConsoleApp1.Entities;

namespace ConsoleApp1.Interfaces;

public interface ICRUDServices
{
    Task <List<Product>> GetAllProductsAsync();
    Task InsertProductAsync(string name, decimal price);
    Task UpdateProductAsync(int id, string name, decimal price);
    Task DeleteProductAsync(int id);
}
