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

        public List<user.displayInfo> GetUsersDisplayInfo(int filter) {
            List<user.displayInfo> temp = new List<user.displayInfo>();

            foreach(user u in userList) {

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
