using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        public List<Supplier.displayInfo> GetSupplierDisplayInfo() {
            List<Supplier.displayInfo> temp = new List<Supplier.displayInfo>();

            foreach (Supplier s in supplierList) {

                temp.Add(s.GetDisplayInfo());
            }
            return temp;
        }

        public int getLastSupplierId() {
            return supplierList.Select(x => x.getId()).Max();
        }


        public List<Supplier> getSupplierList() {
            return supplierList;
        }

    }
}
