using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegendsOfValor_TheGuildTrials.Models.Contracts;
using LegendsOfValor_TheGuildTrials.Repositories.Contratcs;

namespace LegendsOfValor_TheGuildTrials.Repositories
{
    public class GuildRepository : IRepository<IGuild>
    {
        private readonly List<IGuild> _guildRepository;

        public GuildRepository()
        {
            _guildRepository = new List<IGuild>();
        }

        public void AddModel(IGuild entity)
        {
            _guildRepository.Add(entity);
        }

        public IReadOnlyCollection<IGuild> GetAll()
        {
            return _guildRepository.AsReadOnly();
        }

        public IGuild GetModel(string runeMarkOrGuildName)
        {
            return _guildRepository.FirstOrDefault(guild => guild.Name == runeMarkOrGuildName);
        }
    }
}
