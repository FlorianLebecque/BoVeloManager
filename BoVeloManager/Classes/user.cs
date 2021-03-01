using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class User : Human
    {

        private int grade;
        private string hashPass;

        public User(int id_, string name_, int grade_, string hashPass_) : base(id_,name_) {
            grade = grade_;
            hashPass = hashPass_;
        }

        public int getGrade(){
            return grade;
        }

        public string getHashPass() {
            return hashPass;
        }

        public void setGrade(int number){
            grade = number;
        }

        public void setHashPass(string pass) {
            hashPass = pass;
        }

        public bool checkPass(string hash){
            return hash.ToUpper() == hashPass.ToUpper() ;
        }

        public displayInfo GetDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurUser = this;
            temp.name = this.getName();

            string Role = "";
            switch (grade) {
                case 2:
                    Role = "Manager";
                    break;
                case 1:
                    Role = "Seller";
                    break;
                case 0:
                    Role = "Worker";
                    break;
            }

            temp.Role = Role;
            temp.id = this.getId();

            return temp;
        }

        public struct displayInfo {
            public User CurUser { get; set; }
            public string name { get; set; }
            public string Role { get; set; }
            public int id { get; set; }
        }

    }
}
