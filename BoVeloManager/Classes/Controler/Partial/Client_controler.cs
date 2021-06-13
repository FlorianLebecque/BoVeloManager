using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        public List<Client.displayInfo> GetClientDisplayInfo() {
            List<Client.displayInfo> temp = new List<Client.displayInfo>();

            foreach (Client c in clientList) {

                temp.Add(c.GetDisplayInfo());
            }
            return temp;
        }






        //this function displays the sales according to a search string
        public List<Client.displayInfo> GetClientDisplayInfo_search(string str)
        {
            str = str.ToUpper();
            if (str == "")
            {
                return GetClientDisplayInfo();
            }
            else
            {
                List<Client.displayInfo> temp = new List<Client.displayInfo>();

                foreach (Client s in clientList)
                {
                    if (s.getFirstName().ToUpper().Contains(str) || s.getLastName().ToUpper().Contains(str) || s.getName().ToUpper().Contains(str))
                    {
                        temp.Add(s.GetDisplayInfo());
                    }
                    else if (s.getEmail().ToUpper().Contains(str) || s.getEtpName().ToUpper().Contains(str) || s.getEtpAdress().ToUpper().Contains(str) || s.getPhoneNumb().ToUpper().Contains(str) || s.getId().ToString().ToUpper().Contains(str))
                    {
                        temp.Add(s.GetDisplayInfo());
                    }
                }
                return temp;
            }

        }


        public int getLastClientId() {
            return clientList.Select(x => x.getId()).Max();
        }

        public List<Client> getClientList() {
            return clientList;
        }
    }
}
