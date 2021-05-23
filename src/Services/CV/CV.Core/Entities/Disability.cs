using System;
using Career.Domain;

namespace CurriculumVitae.Core.Entities
{
    public class Disability : EntityBase, ISoftDeletable
    {
        public string TypeId { get; set; }
        public float Rate { get; set; }
        public DateTime? CertificateStartDate { get; set; }
        public DateTime? CertificateExpireDate { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
    }
}