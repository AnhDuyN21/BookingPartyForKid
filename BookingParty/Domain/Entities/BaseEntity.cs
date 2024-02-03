using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? ModificationDate { get; set; }

        public int? ModificationBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedBy { get; set; }

        public bool Status { get; set; }
    }
}
