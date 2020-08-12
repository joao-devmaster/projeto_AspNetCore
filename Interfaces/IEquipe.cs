using System.Collections.Generic;
using E_PLAYERS_AspNetCore.Models;

namespace E_PLAYERS_AspNetCore.Interfaces
{
    public interface IEquipe
    {
        void Create(Equipe e);
        List<Equipe> ReadAll();
        void Update(Equipe e);
        void Delete(int IdEquipe); 
    }
}