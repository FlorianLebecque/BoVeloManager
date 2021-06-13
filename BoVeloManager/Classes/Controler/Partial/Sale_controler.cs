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
            str = str.ToUpper();
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
                        if (s.getClient().getFirstName().ToUpper().Contains(str) || s.getClient().getLastName().ToUpper().Contains(str) || s.getClient().getName().ToUpper().Contains(str))
                        {
                            temp.Add(s.GetSaleDisplayInfo());
                        }
                        else if (s.getId().ToString().ToUpper().Contains(str) || s.getState().ToString().ToUpper().Contains(str) || s.getSeller().getName().ToUpper().Contains(str) || s.getSaleDate().ToString("MM/dd/yyyy").ToUpper().Contains(str) || s.getPreSaleDate().ToString("MM/dd/yyyy").ToUpper().Contains(str))
                        {
                            temp.Add(s.GetSaleDisplayInfo());
                        }
                        else if (s.getClient().getEmail().ToUpper().Contains(str) || s.getClient().getEtpName().ToUpper().Contains(str) || s.getClient().getEtpAdress().ToUpper().Contains(str) || s.getClient().getPhoneNumb().ToUpper().Contains(str))
                        {
                            temp.Add(s.GetSaleDisplayInfo());
                        }
                    }
                    else if (s.getId().ToString().ToUpper().Contains(str) || s.getState().ToString().ToUpper().Contains(str) || s.getSeller().getName().ToUpper().Contains(str) || s.getSaleDate().ToString("MM/dd/yyyy").ToUpper().Contains(str) || s.getPreSaleDate().ToString("MM/dd/yyyy").ToUpper().Contains(str))
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
            int maxs = 0;
            int maxc = 0;

            if (CommandeList.Count > 0) {
                maxc = CommandeList.Select(x => x.getId()).Max();
            }
            if (saleList.Count > 0) {
                maxs = saleList.Select(x => x.getId()).Max();
            }
            if (maxc > maxs) {
                return maxc;
            } else {
                return maxs;
            }
        }

        public Sale getSale_byId(int id) {

            List<Sale> result = saleList.Where(x => x.getId() == id).ToList();

            if(result.Count == 1) {
                return result[0];
            }

            throw new Exception("No sale found with Id : " + id.ToString());

        }

    }
}
