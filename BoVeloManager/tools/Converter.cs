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

        public static string GetCatName(KitCategory c) {
            switch (c) {
                case KitCategory.frame:
                    return "Frame";
                case KitCategory.tire:
                    return "Tire";
                case KitCategory.wheels:
                    return "Wheels";
                case KitCategory.saddle:
                    return "Saddle";
                case KitCategory.brake:
                    return "Brake";
                case KitCategory.speed:
                    return "Speed";
                case KitCategory.crankset:
                    return "Crankset";
                case KitCategory.sproket:
                    return "Sproke";
                case KitCategory.chain:
                    return "Chain";
                case KitCategory.air_chamber:
                    return "Air chamber";
                case KitCategory.derailler:
                    return "Derailler";
                case KitCategory.brake_disq:
                    return "Brake disque";
                case KitCategory.fork:
                    return "Fork";
                case KitCategory.handlebar:
                    return "Handleber";
                case KitCategory.tray:
                    return "Tray";
                case KitCategory.light:
                    return "Light";
                case KitCategory.luggage:
                    return "Luggage";
                case KitCategory.mudgard:
                    return "Mudgard";
                case KitCategory.reflector:
                    return "Reflector";
                case KitCategory.crutch:
                    return "Crutch";
                default:
                    return "Unkown";
            }
        }
    }
}
