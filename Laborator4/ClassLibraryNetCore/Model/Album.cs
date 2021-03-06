﻿using System.Collections.Generic;

namespace Lab4.Model
{
    public class Album
    {
        // ef va crea o cheie pe baza conventiilor
        //mai multe detailii https://www.learnentityframeworkcore.com/conventions
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public virtual ICollection<AlbumArtists> Artists { get; set; }
    }
}