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

        public List<Sale.displayInfo> GetSaleDisplayInfo_byClient(string Client_)
        {
            if (Client_ == "") { return GetSaleDisplayInfo(); }
            else
            {
                List<Sale.displayInfo> temp = new List<Sale.displayInfo>();

                foreach (Sale s in saleList)
                {
                    //private Client clt = s.getClient();

                    Console.WriteLine(s.getClient().ToString());
                    Console.WriteLine("////////////////////////////////////");
                

                    if (s.getClient().getFirstName() == Client_ || s.getClient().getLastName() == Client_ || s.getClient().getName() == Client_)
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
