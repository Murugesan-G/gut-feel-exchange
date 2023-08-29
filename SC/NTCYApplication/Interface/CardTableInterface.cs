using NTCYApplication.Models.CardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTCYApplication.Interfaces
{
    public interface CardTableInterface
    {
        string CreateCardTableDetails(Dictionary<string, object> CardTableIDictionary);
        string UpdateCardTable(Dictionary<string, object> CardTableDictionary);
        Dictionary<string, object> SelectCardTable(int TableNo);
        List<CardTable> ViewAllCardTables();
        string DeleteCardTable(int TableNo);
    }
}