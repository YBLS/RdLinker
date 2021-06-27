using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Core.Data.Schema
{
    public class TableAttribute : Attribute
    {
        public string Name { get; set; }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
