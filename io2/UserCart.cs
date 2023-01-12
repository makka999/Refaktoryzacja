using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io2
{
    class UserCart
    {
    public string FirstName;
    public string LastName;
    public string Email;
    public string Rola;
    public Product[] CartProduct = new Product[3]; //max 3 Product in Cart
    }

}
