using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lab4.Model
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IList<AlbumArtists> Albums { get; set; }
    }
}