using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using IGS.TechTest.Controllers;
using IGS.TechTest.Models;
using IGS.TechTest.Services;
using IGS.TechTest.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IGS.TechTest.Tests.Controllers {
  [TestClass]
  public class ProductControllerTests {
    private IProductsService _productsService;
    private ProductController _productController;
    private ProductsFixtures _productsFixtures;

    [TestInitialize]
    public void Setup() {
      _productsService = A.Fake<IProductsService>();
      _productController = new ProductController(_productsService);
      _productsFixtures = new ProductsFixtures();
    }

    [TestMethod]
    public async Task GetProducts_ReturnsAListOfAllProducts() {
      A.CallTo(() => _productsService.GetAllProducts())
       .Returns(new List<Product> { _productsFixtures.Product1, _productsFixtures.Product2 });
      ActionResult<IList<Product>> returns = await _productController.GetProducts();
      A.CallTo(() => _productsService.GetAllProducts()).MustHaveHappened();
      Assert.AreEqual(returns.Result.GetType(), typeof(OkObjectResult));
      OkObjectResult result = returns.Result as OkObjectResult;
      List<Product> resultValue = result.Value as List<Product>;
      Assert.AreEqual(resultValue.Count, 2);
    }

    [TestMethod]
    public async Task GetProduct_WithValidProductId_ReturnsValidProduct() {
      A.CallTo(() => _productsService.GetProduct(1)).Returns(_productsFixtures.Product1);
      ActionResult<Product> returns = await _productController.GetProductById(1);
      A.CallTo(() => _productsService.GetProduct(A<int>._)).MustHaveHappened();
      Assert.AreEqual(returns.Result.GetType(), typeof(OkObjectResult));
      OkObjectResult result = returns.Result as OkObjectResult;
      Product resultValue = result.Value as Product;
      Assert.AreEqual(resultValue, _productsFixtures.Product1);
    }

    [TestMethod]
    public async Task GetProduct_WithInvalidProductId_ReturnsNotFound() {
      A.CallTo(() => _productsService.GetProduct(4)).Returns((Product) null);
      ActionResult<Product> returns = await _productController.GetProductById(4);
      A.CallTo(() => _productsService.GetProduct(A<int>._)).MustHaveHappened();
      Assert.AreEqual(returns.Result.GetType(), typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task AddProduct_WithValidProduct_ReturnsOK() {
      Product toAdd = new Product { Name = "ToAdd", Price = 20 };
      A.CallTo(() => _productsService.AddProduct(toAdd)).Returns(toAdd);
      ActionResult<Product> returns = await _productController.AddProduct(toAdd);
      A.CallTo(() => _productsService.AddProduct(A<Product>._)).MustHaveHappened();
      Assert.AreEqual(returns.Result.GetType(), typeof(OkObjectResult));
    }

    [DataTestMethod]
    [DataRow("", 1)]
    [DataRow("Test", -1)]
    public async Task AddProduct_WithInvalidProduct_ReturnsBadRequest(string name, int price) {
      Product toAdd = new Product { Name = name, Price = (decimal) price };
      A.CallTo(() => _productsService.AddProduct(toAdd)).Returns(toAdd);
      ActionResult<Product> returns = await _productController.AddProduct(toAdd);
      A.CallTo(() => _productsService.AddProduct(A<Product>._)).MustNotHaveHappened();
      Assert.AreEqual(returns.Result.GetType(), typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task UpdateProduct_WithValidProductId_AndValidUpdateParams_ReturnsOk() {
      ProductSlim toUpdate = new ProductSlim { Name = "Updated" };
      A.CallTo(() => _productsService.UpdateProduct(1, toUpdate))
       .Returns(new Product {
         Price = _productsFixtures.Product1.Price, Name = toUpdate.Name,
         ProductCode = _productsFixtures.Product1.ProductCode
       });
      ActionResult returns = await _productController.UpdateProduct(1, toUpdate);
      A.CallTo(() => _productsService.UpdateProduct(A<int>._, A<ProductSlim>._)).MustHaveHappened();
      Assert.AreEqual(returns.GetType(), typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task UpdateProduct_WithInvalidProductId_AndValidUpdateParams_ReturnsNotFound() {
      ProductSlim toUpdate = new ProductSlim { Name = "Updated" };
      A.CallTo(() => _productsService.UpdateProduct(3, toUpdate)).Returns((Product) null);
      ActionResult returns = await _productController.UpdateProduct(3, toUpdate);
      A.CallTo(() => _productsService.UpdateProduct(A<int>._, A<ProductSlim>._)).MustHaveHappened();
      Assert.AreEqual(returns.GetType(), typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task DeleteProduct_WithValidProductId_ReturnsOk() {
      A.CallTo(() => _productsService.DeleteProduct(1)).Returns(true);
      ActionResult returns = await _productController.DeleteProduct(1);
      A.CallTo(() => _productsService.DeleteProduct(A<int>._)).MustHaveHappened();
      Assert.AreEqual(returns.GetType(), typeof(OkResult));
    }

    [TestMethod]
    public async Task DeleteProduct_WithInvalidProductId_ReturnsOk() {
      A.CallTo(() => _productsService.DeleteProduct(3)).Returns(false);
      ActionResult returns = await _productController.DeleteProduct(3);
      A.CallTo(() => _productsService.DeleteProduct(A<int>._)).MustHaveHappened();
      Assert.AreEqual(returns.GetType(), typeof(NotFoundResult));
    }
  }
}