using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
        public string id_model;

        public int id_kit_frame;
        public int id_kit_wheels;
        public int id_kit_brakes;
        public int id_kit_saddle;
        public int id_kit_handlebar;

        public int id_kit_addons;

        public int quantity;

        public float price;

        public string model;
        public string frame;
        public string wheels;
        public string brakes;
        public string saddle;
        public string handlebar;

        public Article()
        {

        }

        public void setKit()
        {
            string kits_data = tools.DatabaseQuery.getAllKits();
            DataTable kits_table = tools.Database.getData(kits_data);

            foreach (DataRow row in kits_table.Rows)
            {
                if (Convert.ToInt32(row["id"]) == id_kit_frame)
                {
                    frame = (string)row["name"];
                }
                else if (Convert.ToInt32(row["id"]) == id_kit_wheels)
                {
                    wheels = (string)row["name"];
                }
                else if (Convert.ToInt32(row["id"]) == id_kit_brakes)
                {
                    brakes = (string)row["name"];
                }
                else if (Convert.ToInt32(row["id"]) == id_kit_saddle)
                {
                    saddle = (string)row["name"];
                }
                else if (Convert.ToInt32(row["id"]) == id_kit_handlebar)
                {
                    handlebar = (string)row["name"];
                }
            }
        }
       

        public float get_price()
        {
            //price = ;
            return price;
        }


    }
}
