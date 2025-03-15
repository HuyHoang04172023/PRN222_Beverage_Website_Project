using Microsoft.EntityFrameworkCore;
using PRN222_Beverage_Website_Project.Models;

namespace PRN222_Beverage_Website_Project.DataAccess
{
    public class ShopDAO
    {
        private readonly Prn222BeverageWebsiteProjectContext _context;
        public ShopDAO()
        {
            _context = new Prn222BeverageWebsiteProjectContext();
        }

        public void AddShop(Shop shop)
        {
            
            try
            {
                _context.Shops.Add(shop);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error saving shop: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
        }
    }
}
