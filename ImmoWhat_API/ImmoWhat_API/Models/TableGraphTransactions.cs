using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImmoWhat_API.Models
{
    public class TableGraphTransactions
    {
        public int annee { get; set; }
        public int nbTransactionsVilla { get; set; }
        public int nbTransactionsMaison { get; set; }
        public int nbTransactionsAppartement { get; set; }
        public int nbTransactionsTerrain { get; set; }
    }
}