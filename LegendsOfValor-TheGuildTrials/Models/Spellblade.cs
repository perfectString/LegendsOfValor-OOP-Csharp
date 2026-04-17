using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendsOfValor_TheGuildTrials.Models
{
    public class Spellblade : Hero
    {
        public Spellblade(string name, string runeMark) : base(name, runeMark, null, 50, 60, 60)
        {

        }

        public override void Train()
        {
            Power += 15;
            Mana += 10;
            Stamina += 10;
        }
    }
}
