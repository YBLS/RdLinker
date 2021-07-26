using RDLinker.Data;
using RDLinker.Data.Schema;
using System;

namespace RDLinker.Entity
{
    public class BaseEntity : IEntity
    {
        [Column("created_time")]
        public DateTime? CreatedTime { get; set; }

        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("updated_time")]
        public DateTime UpdatedTime { get; set; }

        [Column("updated_by")]
        public DateTime? UpdatedBy { get; set; }
    }
}
