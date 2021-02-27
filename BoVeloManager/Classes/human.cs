using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class human
    {
        public int id { get; }
        public string name { get; set; }

        public human(int id_) {
            id = id_;
        }

    }
}
