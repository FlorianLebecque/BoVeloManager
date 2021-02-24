using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools
{
    class Sales
    {
        List<Article> Sale = new List<Article>();

        float total_price; 

        public Sales()
        {
            total_price = 0;
        }

        public float getTotalPrice()
        {
            foreach (Article art in Sale)
            {
                total_price += art.get_price();
            }

            return total_price;
        }
       
    }

    class Article
    {
        private string type;

        private string kit_frame;
        private string kit_wheels;
        private string kit_brakes;
        private string kit_saddle;
        private string kit_handlebar;

        private List<string> kit_addons = new List<string>();

        private int quantity;

        private float price;

        public Article()
        {

        }

        public float get_price()
        {
            //price = ;
            return price;
        }


    }
}
