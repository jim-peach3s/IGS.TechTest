using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace IGS.TechTest.Models {
  /// <summary>
  /// Product model.
  /// </summary>
  [Serializable]
  public partial class Product {
    /// <summary>
    /// Product code. Identity column.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("id")]
    public int ProductCode { get; set; }

    /// <summary>
    /// Name of the product.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    private decimal _price;

    /// <summary>
    /// Price of the product. Getter and setter changed to allow for setting the decimal to 2 places. Is set to JsonIgnore as the tests/expected output requires it to be in a string.
    /// </summary>
    [JsonIgnore]
    [Column(TypeName = "money")]
    public decimal Price {
      get => Math.Round(_price, 2);
      set => _price = value;
    }

    /// <summary>
    /// Object used to display the price as a string when the object is serialised.
    /// </summary>
    [JsonPropertyName("price")]
    public string DisplayPrice => Price.ToString();

    public bool IsValid() {
      return !string.IsNullOrWhiteSpace(Name) && Price >= 0;
    }
  }
}