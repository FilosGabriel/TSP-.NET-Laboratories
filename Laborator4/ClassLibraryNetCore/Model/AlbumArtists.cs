using System.ComponentModel.DataAnnotations;
using Lab4.Model;

namespace Lab4
{
    public class AlbumArtists
    {
        [Key] public int AlbumsArtistsId { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}