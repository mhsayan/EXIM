using System;
using System.Collections.Generic;
using System.Text;

namespace EXIM.Data
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
