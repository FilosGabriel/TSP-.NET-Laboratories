using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lab4.Model
{
    public class Artist
    {
        // ef va crea o cheie pe baza conventiilor
        //mai multe detailii https://www.learnentityframeworkcore.com/conventions
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IList<AlbumArtists> Albums { get; set; }
    }
}