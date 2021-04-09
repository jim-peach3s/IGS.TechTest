using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGS.TechTest.Models {
  /// <summary>
  /// Object used for updating or adding a new Product.
  /// </summary>
  [Serializable]
  public class ProductSlim {
    /// <summary>
    /// Name for the product update. Allows for a null value.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price to update. Allows for null value.
    /// </summary>
    public decimal? Price { get; set; }
  }
}