using BoVeloManager.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {

    partial class DatabaseClassInterface {

        public static List<User> getUsers() {

            //get the user query and data from the database
            string query = DatabaseQuery.getUsers(0);
            DataTable dt = tools.Database.getData(query);

            //convert all the user into a user object
            List<User> temp = new List<User>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string name = (string)dt.Rows[i]["user"];
                int grade = Convert.ToInt32(dt.Rows[i]["grade"]);
                string psw = (string)dt.Rows[i]["psw"];

                temp.Add(new User(id, name, grade, psw));
            }

            return temp;
        }

        public static int updateUser(User moduser) {
            string q = DatabaseQuery.setUserGrade(moduser.getId(), moduser.getGrade()) + ";";
            q += DatabaseQuery.setUserPass(moduser.getId(), moduser.getHashPass());

            return Database.setData(q);
        }

        public static int addUser(User NewUser) {
            string q = DatabaseQuery.addUser(NewUser.getName(), NewUser.getHashPass(), NewUser.getGrade());
            return Database.setData(q);
        }

    }
}
