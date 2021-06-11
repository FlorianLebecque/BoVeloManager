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

        public int getLastClientId() {
            return clientList.Select(x => x.getId()).Max();
        }

        public List<Client> getClientList() {
            return clientList;
        }
    }
}
