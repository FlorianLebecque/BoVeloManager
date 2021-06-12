using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.tools;

namespace BoVeloManager.Classes {
    
    public partial class Controler {

        public List<Commande.displayInfo> GetCommandeDisplayInfo() {
            List<Commande.displayInfo> temp = new List<Commande.displayInfo>();

            foreach (Commande c in CommandeList) {

                temp.Add(c.GetDisplayInfo());
            }
            return temp;
        }
        public void Addcommande(Commande cmd) {
            CommandeList.Add(cmd);
            DatabaseClassInterface.addCommande(cmd);
        }
        public int getlastCommandeId() {

            int maxs=0;
            int maxc=0;

            if (CommandeList.Count > 0) {
                maxc =  CommandeList.Select(x => x.getId()).Max();
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
    }
}
