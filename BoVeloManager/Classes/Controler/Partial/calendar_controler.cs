using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        private int poste;
        public int getAvailablePoste() {
            Console.WriteLine("getPOste = " + poste);
            return poste;
        }

        public DateTime getFirstAvailableDay() {
            DateTime fda = DateTime.Today;
            return a(fda);
        }

        public DateTime a(DateTime fda) {

            if ((fda.DayOfWeek == DayOfWeek.Saturday) || (fda.DayOfWeek == DayOfWeek.Sunday)) {
                return a(NextDay(fda));
            }

            if (numbOfBike(fda, 0) < Properties.Settings.Default.MAX_BIKE_PER_DAY) {
                poste = 0;
                return fda;
            } else if (numbOfBike(fda, 1) < Properties.Settings.Default.MAX_BIKE_PER_DAY) {
                poste = 1;
                return fda;
            } else if (numbOfBike(fda, 2) < Properties.Settings.Default.MAX_BIKE_PER_DAY) {
                poste = 2;
                return fda;
            } else {
                return a(NextDay(fda));
            }
        }

        private DateTime NextDay(DateTime fda) {
            fda = fda.AddDays(1);

            if ((fda.DayOfWeek == DayOfWeek.Saturday) || (fda.DayOfWeek == DayOfWeek.Sunday)) {
                return NextDay(fda);
            } else {
                return fda;
            }

        }

        public int numbOfBike(DateTime day, int poste) {
            int count = 0;
            foreach (Bike bike in bikeList) {
                if ((bike.getPlannedtDate().Date == day.Date) && (bike.getPoste() == poste)) {
                    count++;
                }
            }
            Console.WriteLine("number of bike = " + count);
            return count;
        }

    }
}
