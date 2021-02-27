using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class user : human
    {

        public int grade { get; set; }
        public string Role { get; set; }

        public string hashPass;

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

        public void setGrade(int number){
            grade = number;
            setRole();
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

    }
}
