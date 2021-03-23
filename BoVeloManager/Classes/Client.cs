using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {
    public class Client : Human {
        public Client(int id_, string first_name_, string last_name_, string etp_name_, string etp_adress_, string email_, string phone_num_, DateTime insc_date_) : base(id_, first_name_, last_name_, etp_name_, etp_adress_, email_, phone_num_, insc_date_) {
        }

    }
}
