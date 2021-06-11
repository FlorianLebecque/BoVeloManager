using BoVeloManager.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        public int getLastHumanId() {
            return Math.Max(getLastSupplierId(), getLastClientId());
        }

        public void createHuman(Human c, int fct) {
            switch (fct) {
                case 0:
                    clientList.Add(Converter.ToClient(c));
                    break;
                case 1:
                    supplierList.Add(Converter.ToSupplier(c));
                    break;
            }

            DatabaseClassInterface.addHuman(c, fct);
        }

    }
}
