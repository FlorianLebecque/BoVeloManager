using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class BikeTemplate
    {
        string name = "cc";
        int price = 5;
        int id = 54564;
        public string getName()
        {
            return name;
        }
        public int getPrice()
        {
            return price;
        }
        public int getId()
        {
            return id;
        }
    }
}
