using System;
using System.Collections.Generic;
using System.IO;
using E_PLAYERS_AspNetCore.Interfaces;

namespace E_PLAYERS_AspNetCore.Models
{
    public class Equipe : EPlayersBase , IEquipe
    {

        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/equipe.csv";

        public Equipe()
        {
            CreateFolderAndFile(PATH);
        }

        /// <summary>
        /// Criando e Inserindo novas equipes
        /// </summary>
        /// <param name="e"></param>
        public void Create(Equipe e)
        {
            string[] linhas = {PrepareLine(e)};
            File.AppendAllLines(PATH, linhas);
        }

        /// <summary>
        /// demonstração de como ira ficar os dados no database
        /// </summary>
        /// <param name="e">objeto equipe</param>
        /// <returns></returns>
        private string PrepareLine (Equipe e)
        {
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        /// <summary>
        /// Remove equipe filtrando o IDEquipe
        /// </summary>
        /// <param name="IdEquipe">codigo de identificação de equipe</param>
        public void Delete(int IdEquipe)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
           
            linhas.RemoveAll(a => a.Split(";")[0] == IdEquipe.ToString());

            RewriteCSV(PATH, linhas);
        }

        /// <summary>
        /// Lê todas as linhas do arquivo Database atraves da lista
        /// </summary>
        /// <returns></returns>
        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Equipe equipe = new Equipe();
                equipe.IdEquipe = Int32.Parse(linha[0]);
                equipe.Nome = linha[1];
                equipe.Imagem = linha[2];

                equipes.Add(equipe);
            }
            return equipes;
        }

        /// <summary>
        /// Altera a equipe, p exclui e reescreve 
        /// </summary>
        /// <param name="e"></param>
        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
           
            linhas.RemoveAll(a => a.Split(";")[0] == e.IdEquipe.ToString());
            linhas.Add( PrepareLine(e) );
            RewriteCSV(PATH, linhas);
        }
    }
}