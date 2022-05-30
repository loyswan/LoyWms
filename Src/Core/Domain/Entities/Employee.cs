using LoyWms.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Domain.Entities
{
    public class Employee : AuditableEntity, IAggregateRoot
    {
        //- EmployeeNo 工号
        //- Name 姓名
        //- Gender 性别
        //- BirthDate 出生日期
        //- HireDate 入职日期
        //- Manager 领导
        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }

        public long? ManagerId { get; set; }

        public Employee Manager { get; set; }

    }

    public enum Gender
    {
        Unknow, //未知性别
        Male,   //男性
        Female  //女性
    }
}
