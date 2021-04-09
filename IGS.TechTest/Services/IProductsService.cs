using System.Collections.Generic;
using System.Threading.Tasks;
using IGS.TechTest.Models;

namespace IGS.TechTest.Services {
  /// <summary>
  /// Interface for the products service.
  /// </summary>
  public interface IProductsService {
    /// <summary>
    /// Method to get all products from the table.
    /// </summary>
    /// <returns>List of all products in the table <see cref="IList{Product}"/></returns>
    Task<IList<Product>> GetAllProducts();

    /// <summary>
    /// Method to get a single product from the table by Id.
    /// </summary>
    /// <param name="id">Id of the product to get.</param>
    /// <returns>The product for the passed Id <see cref="Product"/></returns>
    Task<Product> GetProduct(int id);

    /// <summary>
    /// Method to add a new product to the table.
    /// </summary>
    /// <param name="product">The product to add.</param>
    /// <returns>The record created in the database for the new product.</returns>
    Task<Product> AddProduct(Product product);

    /// <summary>
    /// Method to update an existing product with values passed from a slimmed down version of <see cref="Product"/>.
    /// </summary>
    /// <param name="id">The id of the product to update.</param>
    /// <param name="product">The new values to update the product with <see cref="ProductSlim"/></param>
    /// <returns>The updated record for the product.</returns>
    Task<Product> UpdateProduct(int id, ProductSlim product);

    /// <summary>
    /// Method to delete an existing product from the database.
    /// </summary>
    /// <param name="id">Id of the product to delete.</param>
    /// <returns>True or false on wether the product was deleted or not. Will return false if the product doesn't exist for the passed Id.</returns>
    Task<bool> DeleteProduct(int id);
  }
}