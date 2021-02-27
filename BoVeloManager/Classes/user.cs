using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    class user : human
    {
        private readonly int id;
        private string name;
        private int grade;


        public user(int id_, string name_) {

            name = name_;
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

        public static void setGrade(int number){

        }

    }
}
