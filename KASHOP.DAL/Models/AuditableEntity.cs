using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Models
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CratedById { get; set; }
        public ApplicationUser? CratedBy { get; set; }


        public DateTime? UpdatedAT { get; set; }
        public string? UpdatedById { get; set; }
        public ApplicationUser? UpdatedBy { get; set; }


    }
}
