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

        

        private Controler(){

            userList = DatabaseClassInterface.getUsers();



        }

        public static Controler Instance {
            get {
                return instance;
            }
        }
    
    #region user

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


    }
}
