using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class user : human
    {

        private int grade;
        private string Role;
        private string hashPass;

        public user(int id_, string name_,int grade_,string hashPass_):base(id_) {

            name = name_;
            grade = grade_;

            setRole();

            hashPass = hashPass_;
        }

        public  string getUserName()
        {
            return name;
        }

        public int getId()
        {

            return id;
        }

        public int getGrade(){
            return grade;
        }

        public string getHashPass() {
            return hashPass;
        }

        public void setGrade(int number){
            grade = number;
            setRole();
        }

        public void setHashPass(string pass) {
            hashPass = pass;
        }

        public bool checkPass(string hash){
            return hash.ToUpper() == hashPass.ToUpper() ;
        }

        private void setRole() {
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
        }

        public displayInfo GetDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurUser = this;
            temp.name = this.name;
            temp.Role = this.Role;
            temp.id = this.id;

            return temp;
        }

        public struct displayInfo {
            public user CurUser { get; set; }
            public string name { get; set; }
            public string Role { get; set; }
            public int id { get; set; }
        }

    }
}
