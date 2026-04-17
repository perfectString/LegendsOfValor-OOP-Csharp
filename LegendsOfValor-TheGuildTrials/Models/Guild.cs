using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendsOfValor_TheGuildTrials.Models.Contracts;
using LegendsOfValor_TheGuildTrials.Utilities.Messages;

namespace LegendsOfValor_TheGuildTrials.Models
{
    public class Guild : IGuild
    {
        private string _nameGuild;
        private int _wealth;
        private List<string> legionList = new List<string>();

        private readonly string[] validGuilds = new string[]
        {
            "WarriorGuild" , "SorcererGuild" , "ShadowGuild"
        };

        public Guild(string name)
        {
            Name = name;
            Wealth = 5000;
            legionList = new List<string>();
            IsFallen = false;
        }

        public string Name
        {
            get => _nameGuild;

            private set
            {
                if (!validGuilds.Contains(value))
                {
                    throw new ArgumentException(ErrorMessages.InvalidGuildName);
                }

                _nameGuild = value;
            }
        }
        public int Wealth
        {
            get => _wealth;
            private set
            {
                if (value < 0)
                {
                    _wealth = 0;
                }
                else
                {
                    _wealth = value;
                }
            }
        }

        public IReadOnlyCollection<string> Legion
        {
            get => legionList.AsReadOnly();
            private set { }
        }

        public bool IsFallen
        {
            get; private set;
        }

        public void LoseWar()
        {
            this.IsFallen = true;
            this.Wealth = 0;
        }

        public void RecruitHero(IHero hero)
        {
            this.Wealth -= 500;
            legionList.Add(hero.RuneMark);

        }

        public void TrainLegion(ICollection<IHero> heroesToTrain)
        {
            foreach (var item in heroesToTrain)
            {
                this.Wealth -= 200;
            }
        }

        public void WinWar(int goldAmount)
        {
            this.Wealth += goldAmount;
        }
    }
}
