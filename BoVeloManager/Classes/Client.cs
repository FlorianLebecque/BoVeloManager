using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    class Client : Human
    {

        private string etp_name;
        private string etp_adress;
        private string email;
        private string phone_num;
        private string date;
        public Client(int id_, string first_name_, string last_name_, string etp_name_, string etp_adress_, string email_, string phone_num_, string date_) : base(id_, first_name_ + last_name_) {

                etp_name = etp_name_;
                email = email_;
                phone_num = phone_num_;
                date = date_;
            }

        public string getEtpName() {
            return etp_name;
        }
        public string getEtpAdress() {
            return etp_adress;
        }
        public string getEmail() {
            return email;
        }
        public string getPhoneNumb() {
            return phone_num;
        }
        public string getInscDate() {
            return date;
        }

    }
}