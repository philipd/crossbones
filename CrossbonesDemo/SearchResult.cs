using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossbonesDemo
{
    //Class which holds the search results of the peer in a string array
    //which will be sent over the wire
    [Serializable]

    class SearchResult
    {
        public string[] sFileNamesSend { get; set; }

    }
}
