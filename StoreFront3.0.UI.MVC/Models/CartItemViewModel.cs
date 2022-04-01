using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.DATA.EF; //Added for access to the Entity Models (Books)
using System.ComponentModel.DataAnnotations; //Added for access to validation/display metadata attributes

namespace StoreFront3._0.UI.MVC.Models
{
    //Added this model to combine Domain/Entity-related info (Book) with
    //other info -- Therefor, it's a ViewModel

    public class CartItemViewModel
    {
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }

        public Product Product { get; set; }

        public CartItemViewModel(int qty, Product product)
        {
            Qty = qty;
            Product = product;
        }
    }
}