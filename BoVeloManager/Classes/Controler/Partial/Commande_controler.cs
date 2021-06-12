using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {
    
    public partial class Controler {

        public List<Commande.displayInfo> GetCommandeDisplayInfo() {
            List<Commande.displayInfo> temp = new List<Commande.displayInfo>();

            foreach (Commande c in CommandeList) {

                temp.Add(c.GetDisplayInfo());
            }
            return temp;
        }

    }
}
