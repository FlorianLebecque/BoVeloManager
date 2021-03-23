using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.Classes;

namespace BoVeloManager.tools {
    class Converter {
        public static Client ToClient(Human h) {
            return new Client(h.getId(), h.getFirstName(), h.getLastName(), h.getEtpName(), h.getEtpAdress(), h.getEmail(), h.getPhoneNumb(), h.getInscDate());
        }
        public static Supplier ToSupplier(Human h) {
            return new Supplier(h.getId(), h.getFirstName(), h.getLastName(), h.getEtpName(), h.getEtpAdress(), h.getEmail(), h.getPhoneNumb(), h.getInscDate());
        }
    }
}
