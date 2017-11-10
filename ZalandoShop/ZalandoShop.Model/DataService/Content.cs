using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoShop.Model.DataService
{
    public class Content
    {
        public string name { get; set; }
        public string color { get; set; }
        public Brand brand { get; set; }
        public List<Unit> units { get; set; }
        public Media media { get; set; }
    }
}
