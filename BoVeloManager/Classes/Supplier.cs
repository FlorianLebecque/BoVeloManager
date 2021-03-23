using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {
    class Supplier : Human {
        public Supplier(int id_, string first_name_, string last_name_, string etp_name_, string etp_adress_, string email_, string phone_num_, DateTime insc_date_) : base(id_, first_name_, last_name_, etp_name_, etp_adress_, email_, phone_num_, insc_date_) {
        }

        public new displayInfo GetDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurInstance = this;
            temp.name = this.getName();
            temp.id = this.getId();
            temp.etp_name = this.getEtpName();
            temp.etp_adress = this.getEtpAdress();
            temp.email = this.getEmail();
            temp.phone_num = this.getPhoneNumb();
            temp.insc_date = this.getInscDate().ToString("yyyy-MM-dd");

            return temp;
        }

        public new struct displayInfo {
            public Supplier CurInstance { get; set; }
            public string name { get; set; }
            public int id { get; set; }
            public string etp_name { get; set; }
            public string etp_adress { get; set; }
            public string email { get; set; }
            public string phone_num { get; set; }
            public string insc_date { get; set; }
        }
    }
}
