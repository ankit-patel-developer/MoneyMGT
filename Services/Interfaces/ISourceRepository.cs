using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;

namespace Services.Interfaces
{
    public interface ISourceRepository
    {
        IEnumerable<Source> GetAllSources();
    }
}
