using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGS.TechTest.Models;
using IGS.TechTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IGS.TechTest.Controllers {
  /// <summary>
  /// Controller to provide methods to interact with the Products table.
  /// </summary>
  [Route("/v1")]
  public class ProductController : ControllerBase {
    private readonly IProductsService _productsService;

    /// <summary>
    /// Initialises a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="productsService">Service for interacting with the products table.</param>
    public ProductController(IProductsService productsService) {
      _productsService = productsService;
    }

    /// <summary>
    /// Endpoint to get a list of all products.
    /// </summary>
    /// <returns>A list of all the products in the db.</returns>
    /// <response code="200">Returns list of all products.</response>
    [HttpGet]
    [Route("products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<Product>>> GetProducts() {
      return Ok(await _productsService.GetAllProducts());
    }

    /// <summary>
    /// Endpoint to get a product by Id.
    /// </summary>
    /// <param name="id">Id of product to get.</param>
    /// <returns>Product relating to passed Id.</returns>
    /// <response code="200">Returns product for id.</response>
    /// <response code="404">If there is no product for id.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("product/{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id) {
      Product product = await _productsService.GetProduct(id);

      if (product is null) {
        return NotFound();
      }

      return Ok(product);
    }

    /// <summary>
    /// Endpoint to add a product to the database.
    /// </summary>
    /// <param name="productToAdd">Product to add.</param>
    /// <returns>Db entry of the new product.</returns>
    /// <response code="200">Returns the db record for the new product.</response>
    /// <response code="400">Returns if the sent product is invalid (doesn't have a name).</response>
    [HttpPost]
    [Route("product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> AddProduct(Product productToAdd) {
      if (!productToAdd.IsValid()) {
        return BadRequest("Sent product is invalid");
      }

      return Ok(await _productsService.AddProduct(productToAdd));
    }

    /// <summary>
    /// Endpoint to update an existing product record.
    /// </summary>
    /// <param name="id">Id of the record to update.</param>
    /// <param name="productToUpdate">A slimmed down <see cref="Product"/> record. Object doesn't need to include all values.</param>
    /// <returns>Record of updated product.</returns>
    /// <response code="200">Returns the db record for the updated product.</response>
    /// <response code="404">If there is no record existing for the passed Id.</response>
    [HttpPut]
    [Route("product/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateProduct(int id, ProductSlim productToUpdate) {
      Product productUpdated = await _productsService.UpdateProduct(id, productToUpdate);

      if (productUpdated is null) {
        return NotFound();
      }

      return Ok(productUpdated);
    }

    /// <summary>
    /// Endpoint to delete an existing product record.
    /// </summary>
    /// <param name="id">Id of the record to delete.</param>
    /// <returns></returns>
    /// <response code="200">Returns an empty OK response if the record was deleted.</response>
    /// <response code="404">If there was no record for that Id.</response>
    [HttpDelete]
    [Route("product/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProduct(int id) {
      bool wasDeleted = await _productsService.DeleteProduct(id);

      if (wasDeleted) {
        return Ok();
      }

      return NotFound();
    }
  }
}