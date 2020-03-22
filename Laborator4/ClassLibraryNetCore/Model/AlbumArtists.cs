using System.ComponentModel.DataAnnotations;
using Lab4.Model;

namespace Lab4
{
    public class AlbumArtists
    {
        // ef va crea o cheie pe baza conventiilor
        //mai multe detailii https://www.learnentityframeworkcore.com/conventions
        [Key] public int AlbumsArtistsId { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}