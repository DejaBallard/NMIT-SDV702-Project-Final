using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignB_Server_CLI
{
    /// <summary>
    /// Data Storage for all items being sold
    /// </summary>
    public class clsAllItems
    {
        // Generic
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Image64 { get; set; }
        public char Type { get; set; }

        //if  type = R (Ring)
        public string RingSize { get; set; }

        //if type = B(Bracelet)
        public float? Diameter { get; set; }

        //if type = N(Necklace)
        public int? Length { get; set; }
        public override string ToString()
        {
            return Name + "\t" + Quantity;
        }
    }

    /// <summary>
    /// Data storage for Orders
    /// </summary>
   public class clsOrder
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public clsAllItems Item { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime TimeStamp { get; set; }
    }
    
    /// <summary>
    /// Data storage for brands
    /// </summary>
   public class clsBrand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image64 { get; set; }
        public List<clsAllItems> ItemList { get; set; }
    }
}
