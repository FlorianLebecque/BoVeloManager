using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {
    class user {
        private static string userName = "";
        private static int grade = 10;
        private static int id = 0;

        private static bool SETUSER = false;
        private static bool SETGRADE = false;
        private static bool SETID = false;

        public static string getUserName() {
            return userName;
        }

        public static int getId() {
            return id;
        }
        

        public static int getGrade() {
            return grade;
        }

        public static void setUserName(string name) {
            if (!SETUSER) {
                userName = name;
                SETUSER = true;
            } else {
                throw new Exception("User already set");
            }
            
        }

        public static void setGrade(int number) {
            if (!SETGRADE) {
                grade = number;
                SETGRADE = true;
            } else {
                throw new Exception("Grade already set");
            }
            
        }

        public static void setId(int number) {
            if (!SETID) {
                id = number;
                SETID = true;
            } else {
                throw new Exception("User Id already set");
            }
        }

        public static void RESET() {
            SETGRADE = false;
            SETID = false;
            SETUSER = false;
        }

        public static bool checkUserPass(string username,string in_pass) {

            string hash_pass = tools.md5.CreateMD5(in_pass);
            
            return checkPass(username, hash_pass);
        }

        public static bool checkUserPassMD5(string username, string hash_pass) {

            return checkPass(username,hash_pass);
        }

        private static bool checkPass(string username, string hash_pass) {
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
