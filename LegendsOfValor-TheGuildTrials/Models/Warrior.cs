using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendsOfValor_TheGuildTrials.Models
{
    public class Warrior : Hero
    {
        public Warrior(string name, string runeMark) : base(name, runeMark, null, 60, 0, 100)
        {

        }

        public override void Train()
        {
            Power += 30;
            Stamina += 10;
        }
    }
}
