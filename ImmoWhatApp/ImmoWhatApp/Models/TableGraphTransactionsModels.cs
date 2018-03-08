using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhatApp.Models
{
    public class TableGraphTransactionsModels
    {
        public int annee { get; set; }
        public int nbTransactionsVilla { get; set; }
        public int nbTransactionsMaison { get; set; }
        public int nbTransactionsAppartement { get; set; }
        public int nbTransactionsTerrain { get; set; }
    }
}