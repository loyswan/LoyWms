using LoyWms.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Domain.Entities
{
    public class Customer : AuditableEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string? Description { get; set; }

    }
}
