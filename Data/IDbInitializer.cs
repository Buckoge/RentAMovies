using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentAMovies.Data
{
    public interface IDbInitializer
    {
        void Initialize();
    }
}
