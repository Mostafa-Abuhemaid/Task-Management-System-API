using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domain.Entites
{
    public class BaseClass<T>
    {
        public T Id { get; set; }
    }
}
