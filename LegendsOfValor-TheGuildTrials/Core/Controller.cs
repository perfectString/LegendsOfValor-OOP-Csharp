using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendsOfValor_TheGuildTrials.Core.Contracts;
using LegendsOfValor_TheGuildTrials.Models;
using LegendsOfValor_TheGuildTrials.Models.Contracts;
using LegendsOfValor_TheGuildTrials.Repositories;
using LegendsOfValor_TheGuildTrials.Repositories.Contratcs;
using LegendsOfValor_TheGuildTrials.Utilities.Messages;

namespace LegendsOfValor_TheGuildTrials.Core
{
    public class Controller : IController
    {
        private IRepository<IGuild> _guildRepo;
        private IRepository<IHero> _heroRepo;

        private readonly string[] validHeroTypes = new string[]
        {
            nameof(Warrior), nameof(Sorcerer), nameof(Spellblade)
        };
        private readonly string[] warriorValidGuild = new string[]
        {
            "WarriorGuild" , "ShadowGuild"
        };
        private readonly string[] sorcererValidGuild = new string[]
        {
            "ShadowGuild" , "SorcererGuild"
        };
        private readonly string[] spellbladeValidGuild = new string[]
        {
            "SorcererGuild" , "WarriorGuild"
        };

        public Controller()
        {
            _heroRepo = new HeroRepository();
            _guildRepo = new GuildRepository();
        }

        public string AddHero(string heroTypeName, string heroName, string runeMark)
        {
            if (!validHeroTypes.Contains(heroTypeName))
            {
                return string.Format(OutputMessages.InvalidHeroType, heroTypeName);
            }


            if (_heroRepo.GetAll().FirstOrDefault(hero => hero.RuneMark == runeMark) != null) //tuk ili dolu moje da imash greshka
            {
                return string.Format(OutputMessages.HeroAlreadyExists, runeMark);
            }

            IHero newHero = null;
            if (heroTypeName == nameof(Warrior))
            {
                newHero = new Warrior(heroName, runeMark);
            }
            else if (heroTypeName == nameof(Sorcerer))
            {
                newHero = new Sorcerer(heroName, runeMark);
            }
            else if (heroTypeName == nameof(Spellblade))
            {
                newHero = new Spellblade(heroName, runeMark);
            }
            _heroRepo.AddModel(newHero);
            return string.Format(OutputMessages.HeroAdded, heroTypeName, heroName, runeMark);

        }

        public string CreateGuild(string guildName)
        {
            if (_guildRepo.GetModel(guildName) != null)
            {
                return string.Format(OutputMessages.GuildAlreadyExists, guildName);
            }

            var newGuild = new Guild(guildName);
            _guildRepo.AddModel(newGuild);

            return string.Format(OutputMessages.GuildCreated, guildName);
        }

        public string RecruitHero(string runeMark, string guildName)
        {
            if (_heroRepo.GetAll().FirstOrDefault(hero => hero.RuneMark == runeMark) == null)
            {
                return string.Format(OutputMessages.HeroNotFound, runeMark);
            }

            if (_guildRepo.GetAll().FirstOrDefault(guild => guild.Name == guildName) == null)
            {
                return string.Format(OutputMessages.GuildNotFound, guildName);
            }

            var hero = _heroRepo.GetModel(runeMark);

            if (hero.GuildName != null)
            {
                return string.Format(OutputMessages.HeroAlreadyInGuild, hero.Name);
            }

            if (_guildRepo.GetModel(guildName).IsFallen)
            {
                return string.Format(OutputMessages.GuildIsFallen, guildName);
            }
            if (_guildRepo.GetModel(guildName).Wealth < 500)
            {
                return string.Format(OutputMessages.GuildCannotAffordRecruitment, guildName);
            }

            var guild = _guildRepo.GetModel(guildName);

            if (hero is Warrior && !warriorValidGuild.Contains(guildName))
            {
                return string.Format(OutputMessages.HeroTypeNotCompatible, nameof(Warrior), guildName);
            }
            else if (hero is Sorcerer && !sorcererValidGuild.Contains(guildName))
            {
                return string.Format(OutputMessages.HeroTypeNotCompatible, nameof(Sorcerer), guildName);
            }
            else if (hero is Spellblade && !spellbladeValidGuild.Contains(guildName))
            {
                return string.Format(OutputMessages.HeroTypeNotCompatible, nameof(Spellblade), guildName);
            }
            guild.RecruitHero(hero);
            hero.JoinGuild(guild);

            return string.Format(OutputMessages.HeroRecruited, hero.Name, guildName);
        }

        public string StartWar(string attackerGuildName, string defenderGuildName)
        {
            var attackersGuild = _guildRepo.GetModel(attackerGuildName);
            var defendersGuild = _guildRepo.GetModel(defenderGuildName);

            if (attackerGuildName is null || defendersGuild is null)
            {
                return string.Format(OutputMessages.OneOfTheGuildsDoesNotExist);
            }

            if (attackersGuild.IsFallen || defendersGuild.IsFallen)
            {
                return string.Format(OutputMessages.OneOfTheGuildsIsFallen);
            }

            double sumAttackerPower = 0;
            double sumDefenderPower = 0;

            foreach (var attacker in attackersGuild.Legion)
            {
                var hero = _heroRepo.GetAll().FirstOrDefault(h => h.RuneMark == attacker);

                if (hero != null)
                {
                    sumAttackerPower += hero.Power + hero.Mana + hero.Stamina;
                }
            }

            foreach (var defender in defendersGuild.Legion)
            {
                var hero = _heroRepo.GetAll().FirstOrDefault(h => h.RuneMark == defender);

                if (hero != null)
                {
                    sumDefenderPower += hero.Power + hero.Mana + hero.Stamina;
                }
            }

            bool attackerWon = false;
            if (sumAttackerPower > sumDefenderPower)
            {
                attackerWon = true;
            }

            if (attackerWon)
            {
                attackersGuild.WinWar(defendersGuild.Wealth);
                var moneyEarned = defendersGuild.Wealth;
                defendersGuild.LoseWar();

                return string.Format(OutputMessages.WarWon, attackerGuildName, defenderGuildName, moneyEarned);


            }
            else
            {

                defendersGuild.WinWar(attackersGuild.Wealth);
                var moneyEarned = attackersGuild.Wealth;
                attackersGuild.LoseWar();

                return string.Format(OutputMessages.WarLost, defenderGuildName, moneyEarned, attackerGuildName);


            }

        }

        public string TrainingDay(string guildName)
        {

            var guild = _guildRepo.GetModel(guildName);
            if (guild is null)
            {
                return string.Format(OutputMessages.GuildNotFound, guildName);
            }

            if (guild.IsFallen)
            {
                return string.Format(OutputMessages.GuildTrainingDayIsFallen, guildName);
            }

            double totalCost = 0;
            foreach (var member in guild.Legion)
            {
                totalCost += 200;
            }

            if (guild.Wealth < totalCost)
            {
                return string.Format(OutputMessages.TrainingDayFailed, guildName);
            }

            var runeMark = guild.Legion;
            var herosInLegion = _heroRepo.GetAll().Where(h => runeMark.Contains(h.RuneMark)).ToList();
            //tuk moje da imash greshka

            guild.TrainLegion(herosInLegion);

            return string.Format(OutputMessages.TrainingDayStarted, guildName, herosInLegion.Count, totalCost);
        }

        public string ValorState()
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("Valor State:");
            foreach (var guild in _guildRepo.GetAll())
            {
                sb.AppendLine($"{guild.Name} (Wealth: {guild.Wealth})");

                foreach (var rune in guild.Legion)
                {
                    var hero = _heroRepo.GetAll().First(h => h.RuneMark == rune);
                    sb.AppendLine("-" + hero.ToString());
                    sb.AppendLine("--" + hero.Essence());
                }

            }
            return sb.ToString().TrimEnd();

        }
    }
}
