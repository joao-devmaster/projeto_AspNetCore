using System.Collections.Generic;
using E_PLAYERS_AspNetCore.Models;

namespace E_PLAYERS_AspNetCore.Interfaces
{
    public interface INoticias
    {
         void Create(Noticias n);
        List<Noticias> ReadAll();
        void Update(Noticias n);
        void Delete(int IdNoticia);
        
    }
}