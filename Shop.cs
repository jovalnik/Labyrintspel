using System.Collections.Generic;
//using System.Collections.Generic;

namespace TheGame001
{
    public class Shop
        {
            public string Name { get; set; }
            public List<string> Inventory { get; set; }
            public List <(string, int,int)> WantsToBuy { get; set; } // vara, pris, antal
            public int Gold { get; set; }
        }





}





