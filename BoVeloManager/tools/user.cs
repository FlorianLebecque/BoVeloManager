using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {
    class user {
        static private string userName = "";
        static private int grade = 10;
        static private int id = 0;

        public static string getUserName() {
            return userName;
        }

        public static void setUserName(string name) {
            userName = name;
        }

        public static int getGrade() {
            return grade;
        }

        public static void setGrade(int number) {
            grade = number;
        }

        public static bool checkUserPass(string username,string in_pass) {

            string hash_pass = tools.md5.CreateMD5(in_pass);

            //fist get the user password
            string query = tools.DatabaseQuery.getUserPass(username);
            DataTable dt = tools.Database.getData(query);

            //check if we get any result
            string pass = "";
            if (dt.Rows.Count > 0) {
                pass = (string)dt.Rows[0]["psw"];
            }
            
            return (pass.ToUpper() == hash_pass.ToUpper()) && (pass != "");
        }


    }
}
