using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.tools;

namespace BoVeloManager.Classes
{
    public class Controler{

        private static Controler instance = new Controler();

        private User loggedUser;

        private List<User> userList;
        private List<Client> clientList;
        private List<Bike> bikeList;
        private List<BikeTemplate> bikeTemplateList;



        private List<Sale> saleList;
        private List<KitTemplate> kitTemplateList;
        

        private Controler(){

            userList = DatabaseClassInterface.getUsers();
            clientList = DatabaseClassInterface.getClients();
            kitTemplateList = DatabaseClassInterface.getKitTemplates();
            bikeList = DatabaseClassInterface.getBikes();
            saleList = DatabaseClassInterface.getSales(bikeList,userList, clientList);
        }

        public static Controler Instance {
            get {
                return instance;
            }
        }
    
    #region User

        public List<User.displayInfo> GetUsersDisplayInfo(int filter) {
            List<User.displayInfo> temp = new List<User.displayInfo>();

            foreach(User u in userList) {

                switch (filter) {
                    case 0:
                        temp.Add(u.GetDisplayInfo());
                        break;
                    default:

                        int grade = 2 - filter;
                        if(u.getGrade() == grade) {
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
            foreach(User u in userList){
                if(u.getName() == name){
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

    #endregion

    #region Client

        public List<Client.displayInfo> GetClientDisplayInfo() {
            List<Client.displayInfo> temp = new List<Client.displayInfo>();

            foreach (Client c in clientList) {

                temp.Add(c.GetDisplayInfo());
            }
            return temp;
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
    #endregion

    #region KitTemplate

        public List<KitTemplate.displayInfo> getKitTemplateDisplayInfo(){

            List<KitTemplate.displayInfo> temp = new List<KitTemplate.displayInfo>();

            foreach (KitTemplate kt in kitTemplateList) {
                temp.Add(kt.GetDisplayInfo());
            }

            return temp;
        }

        public int getLastKitTemplate(){
            return kitTemplateList.Select(x => x.getId()).Max();
        }

        public void createKit(KitTemplate kt) {
            kitTemplateList.Add(kt);
            DatabaseClassInterface.addKitTemplate(kt);
        }

    #endregion


    #region Bike
        public List<Bike.displayInfo> GetBikeDisplayInfo()
        {
            List<Bike.displayInfo> temp = new List<Bike.displayInfo>();

            foreach (Bike b in bikeList)
            {

                temp.Add(b.GetDisplayInfo());
            }
            return temp;
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

        #endregion
    }
}
