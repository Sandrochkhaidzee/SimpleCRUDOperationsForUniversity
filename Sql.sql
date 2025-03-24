CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL
);
GO

CREATE PROCEDURE InsertProduct
    @Name NVARCHAR(100),
    @Price DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Products (Name, Price)
    VALUES (@Name, @Price);
END
GO

CREATE PROCEDURE UpdateProduct
    @Id INT,
    @Name NVARCHAR(100),
    @Price DECIMAL(10,2)
AS
BEGIN
    UPDATE Products
    SET Name = @Name,
        Price = @Price
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE DeleteProduct
    @Id INT
AS
BEGIN
    DELETE FROM Products
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE GetProducts
AS
BEGIN
    SELECT Id, Name, Price FROM Products;
END
GO
