using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGS.TechTest.Models;

namespace IGS.TechTest.Tests.Fixtures {
  public class ProductsFixtures {
    public Product Product1 = new Product {
      Price = 10,
      Name = "Apple",
      ProductCode = 1
    };

    public Product Product2 = new Product {
      Price = 2,
      Name = "Pear",
      ProductCode = 2
    };
  }
}