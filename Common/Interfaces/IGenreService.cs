using System.Collections.Generic;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IGenreService
    {
        void Initialize();
        List<Genre> Get();
    }
}
