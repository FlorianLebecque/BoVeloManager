using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.tools;

namespace BoVeloManager.Classes
{
    public class controler
    {

        private static controler instance = new controler();

        private user loggedUser;

        private List<user> userList;

        static controler() {

        }

        private controler(){

            userList = DatabaseClassInterface.getUsers();



        }

        public static controler Instance {
            get {
                return instance;
            }
        }
    

    #region user


        public List<user> GetUserList() {
            return userList;
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
