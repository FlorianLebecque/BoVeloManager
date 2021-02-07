using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {
    class user {
        static private string userName = "";
        static private int grade = 10;

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


    }
}
