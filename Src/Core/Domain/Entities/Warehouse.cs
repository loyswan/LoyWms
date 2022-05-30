using LoyWms.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Domain.Entities
{
    public class Warehouse : AuditableEntity, IAggregateRoot
    {
        //- WarehouseNo 仓库编号
        //- Name 仓库名
        //- Address 仓库地点
        //- Description 仓库说明
        //- Keeper 库管员
        public string WarehouseNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }


        public long KeeperId { get; set; }

        public Employee Keeper { get; set; }
    }
}
