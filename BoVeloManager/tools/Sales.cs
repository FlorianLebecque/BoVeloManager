using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools
{
    class Sales
    {
        public List<Article> Sale = new List<Article>();

        float total_price; 

        public Sales()
        {
            total_price = 0;
        }

        public void add_article_to_sale(Article article)
        {
            Sale.Add(article);
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
        public string model;

        public int id_kit_frame;
        public int id_kit_wheels;
        public int id_kit_brakes;
        public int id_kit_saddle;
        public int id_kit_handlebar;

        public int id_kit_addons;

        public int quantity;

        public float price;

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
