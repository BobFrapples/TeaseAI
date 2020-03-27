using System;
using System.Collections.Generic;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Data.Interfaces;

namespace TeaseAI.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public List<Genre> Get() => _genreRepository.Get();

        public void Initialize()
        {
            var defaultData = CreateDefaultGenre();
            foreach(var genre in defaultData)
            {
                var getGenre = _genreRepository.Get(genre.Id);
                if (getGenre.IsSuccess)
                    _genreRepository.Update(genre);
                else
                    _genreRepository.Create(genre);
            }
        }

        private List<Genre> CreateDefaultGenre()
        {
            return new List<Genre>
            {
                new Genre{Id = 1, Name = "Blog"},
                new Genre{Id = 2, Name = "Butt"},
                new Genre{Id = 3, Name = "Boobs"},
                new Genre{Id = 4, Name = "Hardcore"},
                new Genre{Id = 5, Name = "Softcore"},
                new Genre{Id = 6, Name = "Lesbian"},
                new Genre{Id = 7, Name = "Blowjob"},
                new Genre{Id = 8, Name = "Femdom"},
                new Genre{Id = 9, Name = "Lezdom"},
                new Genre{Id = 10, Name = "Hentai"},
                new Genre{Id = 11, Name = "Gay"},
                new Genre{Id = 12, Name = "Maledom"},
                new Genre{Id = 13, Name = "Captions"},
                new Genre{Id = 14, Name = "General"},
                new Genre{Id = 15, Name = "Liked"},
                new Genre{Id = 16, Name = "Disliked"},
                new Genre{Id = 17, Name = "CockHero"},
                new Genre{Id = 18, Name = "FemSub"},
                new Genre{Id = 19, Name = "General"},
                new Genre{Id = 20, Name = "JOI"},
            };
        }
    }
}
