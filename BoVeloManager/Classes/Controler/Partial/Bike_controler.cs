using BoVeloManager.tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        public void createBike(Bike b) {
            bikeList.Add(b);
            DatabaseClassInterface.addBike(b);
        }

        public int getLastBike() {
            if (bikeList.Count > 0) {
                return bikeList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public List<Bike.displayInfo> GetBikeDisplayInfo() {
            List<Bike.displayInfo> temp = new List<Bike.displayInfo>();

            foreach (Bike b in bikeList) {

                temp.Add(b.GetDisplayInfo());
            }
            return temp;
        }

        public List<Bike> getBikesList() {
            return bikeList;
        }

        public int getLastBikeId() {
            if (bikeList.Count > 0) {
                //Console.WriteLine("######");
                foreach (Bike b in bikeList) {
                    //Console.WriteLine("id = " + b.getId());
                }
                return bikeList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public int getLastBikeTemplate() {
            if (bikeTemplateList.Count > 0) {
                return bikeTemplateList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public static int getNBRWeek(DateTime dt) {
            return (new GregorianCalendar(GregorianCalendarTypes.Localized).GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday));
        }

        public List<Bike.displayInfo> GetBikeDisplayInfo_byWeekAndPost(int week, int poste) {
            List<Bike.displayInfo> temp = new List<Bike.displayInfo>();

            foreach (Bike b in bikeList) {
                if ((date.getNBRWeek(b.getPlannedtDate()) == week) && (b.getPoste() == poste)) {
                    temp.Add(b.GetDisplayInfo());
                }
            }

            return temp;
        }

    }
}
