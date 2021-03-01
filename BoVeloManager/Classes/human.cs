using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class Human
    {
        public int id { get; }
        public string name { get; set; }

        public Human(int id_) {
            id = id_;
        }

    }
}
