using RDLinker.Core.Data;
using RDLinker.Core.Data.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDLinker.Test.Data.Entity
{
    [Table("systemuser")]
    public class SystemUser : BaseEntity
    {
        [Key]
        [Column("systemuserid")]
        public string SystemUserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("age")]
        public int Age { get; set; }

    }
}
