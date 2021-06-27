using System;
using System.Collections.Generic;
using System.Text;

namespace RDLinker.Core.Data.Schema
{
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public ColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
