using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.LogicEntity.Entities.Org
{
    public class User : BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
