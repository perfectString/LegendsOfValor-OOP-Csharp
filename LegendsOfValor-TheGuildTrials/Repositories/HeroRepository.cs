using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendsOfValor_TheGuildTrials.Models.Contracts;
using LegendsOfValor_TheGuildTrials.Repositories.Contratcs;

namespace LegendsOfValor_TheGuildTrials.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly List<IHero> _heroRepository;

        public HeroRepository()
        {
             _heroRepository = new List<IHero>();
        }


        public void AddModel(IHero entity)
        {
            _heroRepository.Add(entity);
        }

        public IReadOnlyCollection<IHero> GetAll()
        {
            return _heroRepository.AsReadOnly();
        }

        public IHero GetModel(string runeMarkOrGuildName)
        {

            return _heroRepository.FirstOrDefault(hero => hero.RuneMark == runeMarkOrGuildName);
        }
    }
}
