﻿using System.ComponentModel.DataAnnotations;

namespace LibraryBlazor.Entity.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<Book> Books { get; } = [];
    }
}
