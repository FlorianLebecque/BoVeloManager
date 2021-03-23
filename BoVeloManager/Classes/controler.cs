using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.tools;

namespace BoVeloManager.Classes
{
    public class Controler {

        private static Controler instance = new Controler();

        private User loggedUser;

        private List<User> userList;
        private List<Client> clientList;
        private List<Bike> bikeList;
        private List<CatalogBike> CatalogBikeList;
        private List<BikeTemplate> bikeTemplateList;
        private List<Sale> saleList;
        private List<KitTemplate> kitTemplateList;
        public TempSale tempSale;


        private Controler() {
            LOAD_USERS();
            LOAD_CLIENTS();
            LOAD_KITEMP();
            LOAD_CATALOG();
            LOAD_BIKETEMP();
            LOAD_BIKES();
            LOAD_SALES();
            
            tempSale = new TempSale();
        }

        public static Controler Instance {
            get {
                return instance;
            }
        }

        #region LOAD

        public void LOAD_USERS() {
            userList = DatabaseClassInterface.getUsers();
        }

        public void LOAD_CLIENTS() {
            clientList = DatabaseClassInterface.getClients();
        }

        public void LOAD_KITEMP() {
            kitTemplateList = DatabaseClassInterface.getKitTemplates();
        }

        public void LOAD_CATALOG() {
            CatalogBikeList = DatabaseClassInterface.getCatalogBikes(kitTemplateList);
        }

        public void LOAD_BIKETEMP() {
            bikeTemplateList = DatabaseClassInterface.getBikeTemplates(CatalogBikeList, kitTemplateList);
        }

        public void LOAD_BIKES() {
            bikeList = DatabaseClassInterface.getBikes(bikeTemplateList);
        }

        public void LOAD_SALES() {
            saleList = DatabaseClassInterface.getSales(bikeList, userList, clientList);
        }

        #endregion 


        #region User

        public List<User.displayInfo> GetUsersDisplayInfo(int filter) {
            List<User.displayInfo> temp = new List<User.displayInfo>();

            foreach (User u in userList) {

                switch (filter) {
                    case 0:
                        temp.Add(u.GetDisplayInfo());
                        break;
                    default:

                        int grade = 2 - filter;
                        if (u.getGrade() == grade) {
                            temp.Add(u.GetDisplayInfo());
                        }

                        break;
                }


            }


            return temp;
        }

        public User getCurrentUser() {
            return loggedUser;
        }

        public User getUser_byName(string name) {
            foreach (User u in userList) {
                if (u.getName() == name) {
                    return u;
                }
            }

            return null;
        }

        public void setCurrentUser(User cur) {
            loggedUser = cur;
        }

        public int getLastUserId() {
            return userList.Select(x => x.getId()).Max();
        }

        public void createUser(User newUser) {

            userList.Add(newUser);
            DatabaseClassInterface.addUser(newUser);

        }

        public List<User> getUserList()
        {
            return userList;
        }

        #endregion

        #region Client

        public List<Client.displayInfo> GetClientDisplayInfo() {
            List<Client.displayInfo> temp = new List<Client.displayInfo>();

            foreach (Client c in clientList) {

                temp.Add(c.GetDisplayInfo());
            }
            return temp;
        }

        public int getLastClientId() {
            return clientList.Select(x => x.getId()).Max();
        }

        public void createClient(Client c) {

            clientList.Add(c);
            DatabaseClassInterface.addHuman(c);
        }

        public List<Client> getClientList()
        {
            return clientList;
        }

        #endregion

        #region Sale
        public List<Sale.displayInfo> GetSaleDisplayInfo() {
            List<Sale.displayInfo> temp = new List<Sale.displayInfo>();

            foreach (Sale s in saleList) {

                temp.Add(s.GetSaleDisplayInfo());
            }
            return temp;
        }

        public void createSale(Sale s)
        {
            saleList.Add(s);
            DatabaseClassInterface.addSale(s);
        }

        public int getLastSaleId()
        {
            if (saleList.Count > 0)
            {
                return saleList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public Sale getSale_byId(int id) {
            foreach (Sale s in saleList) {
                if (s.getId() == id) {
                    return s;
                }
            }

            throw new Exception("No sale found with Id : " + id.ToString());

        }

        #endregion

        #region KitTemplate

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

        #endregion

        #region Bike

        public void createBike(Bike b)
        {
            bikeList.Add(b);
            DatabaseClassInterface.addBike(b);
        }

        public int getLastBike()
        {
            if (bikeList.Count > 0)
            {
                return bikeList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public List<Bike.displayInfo> GetBikeDisplayInfo()
        {
            List<Bike.displayInfo> temp = new List<Bike.displayInfo>();

            foreach (Bike b in bikeList)
            {

                temp.Add(b.GetDisplayInfo());
            }
            return temp;
        }

        public List<Bike> getBikesList()
        {
            return bikeList;
        }

        public int getLastBikeId()
        {
            if (bikeList.Count > 0)
            {
                //Console.WriteLine("######");
                foreach (Bike b in bikeList)
                {
                    //Console.WriteLine("id = " + b.getId());
                }
                return bikeList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public int getLastBikeTemplate()
        {
            if (bikeTemplateList.Count > 0)
            {
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

        #endregion

        #region BikeTemplate

        public void createBikeTemplate(BikeTemplate t)
        {
            DatabaseClassInterface.addBikeTemplate(t);
            bikeTemplateList.Add(t);
        }

        public void link_kit_to_tbike(BikeTemplate bt, KitTemplate kt)
        {
            DatabaseClassInterface.link_kit_to_tbike(bt, kt);
        }

        public List<BikeTemplate> GetBikeTemplateList()
        {
            return bikeTemplateList;
        }

        public BikeTemplate getBikeTemplateById(int id_tBike)
        {
            foreach (BikeTemplate bt in bikeTemplateList)
            {

                if (bt.getId() == id_tBike)
                {
                    return bt;
                }

            }
            return null;
        }

        public int getLastBikeTemplateId()
        {
            if (bikeTemplateList.Count > 0)
            {
                return bikeTemplateList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public List<BikeTemplate> getBikeTemplateList() {
            return bikeTemplateList;
        }

        #endregion

        #region CatalogBike

        public List<CatalogBike.displayInfo> getCatalogBikeDisplayInfo() {
            List<CatalogBike.displayInfo> temp = new List<CatalogBike.displayInfo>();

            foreach (CatalogBike cb in CatalogBikeList) {
                temp.Add(cb.GetDisplayInfo());
            }

            return temp;
        }

        public List<CatalogBike> getCatalogBike() {
            return CatalogBikeList;
        }

        public int getlastCatalogBikeId() {
            if (CatalogBikeList.Count > 0) {
                return CatalogBikeList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public void createCatalogBike(CatalogBike cb) {
            CatalogBikeList.Add(cb);
            DatabaseClassInterface.addCatalogBike(cb);
        }

        #endregion

        private int poste;
        public int getAvailablePoste()
        {
            Console.WriteLine("getPOste = " + poste);
            return poste;
        }

        public DateTime getFirstAvailableDay()
        {
            DateTime fda = DateTime.Today;
            return a(fda);
        }

        public DateTime a(DateTime fda)
        {
            if (numbOfBike(fda, 0) < Properties.Settings.Default.MAX_BIKE_PER_DAY)
            {                
                poste = 0;
                return fda;
            }
            else if (numbOfBike(fda, 1) < Properties.Settings.Default.MAX_BIKE_PER_DAY)
            {
                poste = 1;
                return fda;
            }
            else if (numbOfBike(fda, 2) < Properties.Settings.Default.MAX_BIKE_PER_DAY)
            {
                poste = 2;
                return fda;
            }
            else
            {                
                return a(NextDay(fda));
            }
        }   

        private DateTime NextDay(DateTime fda) {
            fda = fda.AddDays(1);

            if((fda.DayOfWeek == DayOfWeek.Saturday)||(fda.DayOfWeek == DayOfWeek.Sunday)) {
                return NextDay(fda);
            } else {
                return fda;
            }

        }

        public int numbOfBike(DateTime day, int poste) {
            int count = 0;
            foreach (Bike bike in bikeList)
            {
                if ((bike.getPlannedtDate().Date  == day.Date) && (bike.getPoste() == poste))
                {                    
                    count++;
                }
            }
            Console.WriteLine("number of bike = " + count);
            return count;
        }
    }
}
