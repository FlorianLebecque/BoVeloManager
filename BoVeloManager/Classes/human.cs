using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class Human
    {
        private int id;
        private string name;

        public Human(int id_,string name_) {
            id = id_;
            name = name_;
        }

        public int getId() {
            return id;
        }

        public string getName() {
            return name;
        }

    }
}
