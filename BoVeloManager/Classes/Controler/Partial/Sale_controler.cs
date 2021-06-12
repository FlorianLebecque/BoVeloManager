using BoVeloManager.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        public List<Sale.displayInfo> GetSaleDisplayInfo() {
            List<Sale.displayInfo> temp = new List<Sale.displayInfo>();

            foreach (Sale s in saleList) {

                temp.Add(s.GetSaleDisplayInfo());
            }
            return temp;
        }

       

        //this function displays the sales according to a search string
        public List<Sale.displayInfo> GetSaleDisplayInfo_search(string str)
        {
            if (str == "") 
            {
                return GetSaleDisplayInfo(); 
            }
            else
            {
                List<Sale.displayInfo> temp = new List<Sale.displayInfo>();

                foreach (Sale s in saleList)
                {
                    if (s.getClient() != null)
                    {
                        if (s.getClient().getFirstName().Contains(str) || s.getClient().getLastName().Contains(str) || s.getClient().getName().Contains(str))
                        {
                            temp.Add(s.GetSaleDisplayInfo());
                        }
                        else if (s.getId().ToString().Contains(str) || s.getState().ToString().Contains(str) || s.getSeller().getName().Contains(str) || s.getSaleDate().ToString("MM/dd/yyyy").Contains(str) || s.getPreSaleDate().ToString("MM/dd/yyyy").Contains(str))
                        {
                            temp.Add(s.GetSaleDisplayInfo());
                        }
                    }
                    else if (s.getId().ToString().Contains(str) || s.getState().ToString().Contains(str) || s.getSeller().getName().Contains(str) || s.getSaleDate().ToString("MM/dd/yyyy").Contains(str) || s.getPreSaleDate().ToString("MM/dd/yyyy").Contains(str))
                    {
                        temp.Add(s.GetSaleDisplayInfo());
                    }



                }
                return temp;
            }

        }






        public void createSale(Sale s) {
            saleList.Add(s);
            DatabaseClassInterface.addSale(s);
        }

        public int getLastSaleId() {
            if (saleList.Count > 0) {
                return saleList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public Sale getSale_byId(int id) {
            foreach (Sale s in saleList) {
                if (s.getId() == id) {
                    return s;
                }
            }

            throw new Exception("No sale found with Id : " + id.ToString());

        }

    }
}
