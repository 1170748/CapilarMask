using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CapilarMask.Models
{
    public class Note
    {
        
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public string Fase { get; set; }
        public DateTime Date { get; set; }
        public int Tratamento_selecionado { get; set; }

       
    }
}
