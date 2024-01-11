using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Models
{
    public class Content
    {
        public Content(IEnumerable<ParametrQuery> parametrs)
        {
            ParametrQueries = parametrs;
        }
        public IEnumerable<ParametrQuery> ParametrQueries { get; set; }
    }
}
