using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Access to metadata attributes


namespace StoreFront.DATA.EF/*.StoreFrontMetadata*/
{

    #region Product Metadata

    public class ProductMetadata
    {
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*Must be 100 characters or less")]
        [DisplayFormat(NullDisplayText = "[-N/A]-")]
        [Display(Name = "Product Name")]
        [UIHint("MultilineText")]
        public string ProductName { get; set; }


        [DisplayFormat(NullDisplayText = "[-N/A]-")]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "*Must be a valid number, 0 or larger")]
        [DisplayFormat(NullDisplayText = "[-N/A]", DataFormatString = "{0:c}")]
        public Nullable<decimal> Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "*Must be a valid number, 0 or larger")]
        [DisplayFormat(NullDisplayText = "[-N/A]-")]
        [Display(Name = "Units Sold")]
        public Nullable<int> UnitsSold { get; set; }

        [Display(Name = "Image")]
        public string BookImage { get; set; }


        [Required(ErrorMessage = "*")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        public int StockID { get; set; }

        [Required(ErrorMessage = "*")]
        public int ShippingID { get; set; }

        [Required(ErrorMessage = "*")]
        public int ManufacturerID { get; set; }
    }
    #endregion

    #region Department Metadata

    public class DepartmentMetadata
    {
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }

    #endregion

    #region Stock Metadata

    public class StockMetadata
    {

        [DisplayFormat(NullDisplayText = "[-N/A]-")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Stock Status")]
        public string StockStatus { get; set; }
    }



    #endregion

    #region Category Metadata

    public class CategoryMetadata
    {
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [DisplayFormat(NullDisplayText = "-[N/A]-")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Category Discount")]
        public string CategoryDiscount { get; set; }
    }

    #endregion

    #region Shipping Metadata

    public class ShippingMetadata
    {
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Shipping Status")]
        public string ShippingStatus { get; set; }

        [DisplayFormat(NullDisplayText = "-[N/A]-")]
        [StringLength(100, ErrorMessage = "*Must be 100 characters or less")]
        [Display(Name = "Shipping Date")]
        public string ShippingDate { get; set; }
    }

    #endregion

    #region Employee Metadata

    public class EmployeeMetadata
    {
        [Required(ErrorMessage = "*")]
        [StringLength(25, ErrorMessage = "*Must be 25 characters or less")]
        public string Position { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "*Must be 20 characters or less")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "*Must be 20 characters or less")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Hire Date")]
        public string HireDate { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
        //Create a custom, read-only property for FullName and update the Display attribute
        [Display(Name = "Employee")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }

    #endregion

    #region Manufacturer Metadata

    public class ManufacturerMetadata
    {
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Display(Name = "Manufacturer Name")]
        public string ManufacturersName { get; set; }


        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "*Must be 20 characters or less")]
        [Display(Name = "Company Status")]
        public string CompanyStatus { get; set; }


        [StringLength(50, ErrorMessage = "*Must be 50 charactes or less")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "*Must be 2 characters")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string State { get; set; }

        [StringLength(5, ErrorMessage = "*Must be 5 characters")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [StringLength(30, ErrorMessage = "*Must be 30 characters or less")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string Country { get; set; }
    }

    #endregion
}
