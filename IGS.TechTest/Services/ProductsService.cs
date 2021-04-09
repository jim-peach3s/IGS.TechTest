using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IGS.TechTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IGS.TechTest.Services
{
  /// <summary>
  /// Service to abstract accessing the Products table.
  /// </summary>
  public class ProductsService : IProductsService {
    private readonly MarketplaceContext _context;

    /// <summary>
    /// Initialises a new instance of the <see cref="ProductsService"/> class.
    /// </summary>
    /// <param name="context">DBContext for the marketplace db.</param>
    public ProductsService(MarketplaceContext context) {
      _context = context;
    }

    /// <inheritdoc />
    public async Task<IList<Product>> GetAllProducts() {
      return await _context.Products.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Product> GetProduct(int id) {
      return await _context.Products.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<Product> AddProduct(Product product) {
      EntityEntry<Product> entity = await _context.Products.AddAsync(product);
      await _context.SaveChangesAsync();
      return entity.Entity;
    }

    /// <inheritdoc />
    public async Task<Product> UpdateProduct(int id, ProductSlim product) {
      Product toUpdate = await GetProduct(id);

      if (toUpdate == default) {
        return null;
      }

      toUpdate.Name = product.Name ?? toUpdate.Name;
      toUpdate.Price = product.Price ?? toUpdate.Price;

      EntityEntry<Product> updatedProduct = _context.Update(toUpdate);
      await _context.SaveChangesAsync();
      return updatedProduct.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteProduct(int id) {
      Product toDelete = await GetProduct(id);

      if (toDelete == default) {
        return false;
      }

      _context.Remove(toDelete);

      await _context.SaveChangesAsync();

      await ReseedIdentifier();

      return true;
    }

    /// <summary>
    /// Method to reseed the tables identity column on delete.
    /// This allows the ProductCode to be re-used after one has been deleted as the Identity column usually increments by 1.
    /// </summary>
    /// <returns></returns>
    private async Task ReseedIdentifier() {
      Assembly thisAssembly = Assembly.GetExecutingAssembly();
      await using Stream stream = thisAssembly.GetManifestResourceStream("IGS.TechTest.Sql.ReseedProductIndex.sql");
      using StreamReader streamReader = new StreamReader(stream);
      string commandText = await streamReader.ReadToEndAsync();

      await _context.Database.ExecuteSqlRawAsync(commandText);
    }
  }
}
