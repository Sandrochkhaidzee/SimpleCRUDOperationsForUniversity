using ConsoleApp1.Entities;
using ConsoleApp1.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace ConsoleApp1.Services;

class CRUDServices : ICRUDServices
{
    private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=ProductsForUniversity;Trusted_Connection=True;";


    public async Task DeleteProductAsync(int id)
    {
        using SqlConnection connection = new(ConnectionString);
        using SqlCommand command = new("DeleteProduct", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@Id", id));

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        using SqlConnection connection = new(ConnectionString);
        using SqlCommand command = new("GetProducts", connection);
        command.CommandType = CommandType.StoredProcedure;

        var products = new List<Product>();

        await connection.OpenAsync();
        using SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            products.Add(new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            });
        }

        return products;
    }

    public async Task InsertProductAsync(string name, decimal price)
    {
        using SqlConnection connection = new(ConnectionString);
        using SqlCommand command = new("InsertProduct", connection);
        command.CommandType = CommandType.StoredProcedure;

        DbParameter paramName = new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = name };
        DbParameter paramPrice = new SqlParameter("@Price", SqlDbType.Decimal) { Value = price };

        command.Parameters.Add(paramName);
        command.Parameters.Add(paramPrice);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateProductAsync(int id, string name, decimal price)
    {
        using SqlConnection connection = new(ConnectionString);
        using SqlCommand command = new("UpdateProduct", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@Id", id));
        command.Parameters.Add(new SqlParameter("@Name", name));
        command.Parameters.Add(new SqlParameter("@Price", price));

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }
}
