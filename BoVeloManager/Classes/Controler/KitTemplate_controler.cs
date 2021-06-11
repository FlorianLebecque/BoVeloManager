using BoVeloManager.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        public List<KitTemplate.displayInfo> getKitTemplateDisplayInfo() {

            List<KitTemplate.displayInfo> temp = new List<KitTemplate.displayInfo>();

            foreach (KitTemplate kt in kitTemplateList) {
                temp.Add(kt.GetDisplayInfo());
            }

            return temp;
        }

        public int getLastKitTemplateId() {
            if (kitTemplateList.Count > 0) {
                return kitTemplateList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public void createKit(KitTemplate kt) {
            kitTemplateList.Add(kt);
            DatabaseClassInterface.addKitTemplate(kt);
        }

        public List<KitTemplate> getKitTemplateList() {
            return kitTemplateList;
        }

    }
}
