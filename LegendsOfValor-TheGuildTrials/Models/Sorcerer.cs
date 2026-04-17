using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendsOfValor_TheGuildTrials.Models
{
    public class Sorcerer : Hero
    {
        public Sorcerer(string name, string runeMark) : base(name, runeMark, null, 40, 120, 0)
        {

        }

        public override void Train()
        {
            Power += 20;
            Mana += 25;
        }
    }
}
