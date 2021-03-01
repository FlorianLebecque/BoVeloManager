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

        private user loggedUser;

        private List<user> userList;

        

        private Controler(){

            userList = DatabaseClassInterface.getUsers();



        }

        public static Controler Instance {
            get {
                return instance;
            }
        }
    
    #region user

        public List<user.displayInfo> GetUserList() {
            List<user.displayInfo> temp = new List<user.displayInfo>();

            foreach(user u in userList) {
                temp.Add(u.GetDisplayInfo());
            }


            return temp;
        }

        public List<user> GetUserList_byGrade(int g) {
            List<user> temp = new List<user>();

            foreach(user u in userList) {
                if(u.getGrade() == g) {
                    temp.Add(u);
                }
            }

            return temp;
        }

        public user getCurrentUser() {
            return loggedUser;
        }

        public user getUser_byName(string name) {
            foreach(user u in userList){
                if(u.getUserName() == name){
                    return u;
                }
            }

            return null;
        }

        public void setCurrentUser(user cur) {
            loggedUser = cur;
        }

    #endregion


    }
}
