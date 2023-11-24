using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Premitives
{
    public abstract class Entity
    {
        public Entity(Guid id) { 
        
            Id = id;
        
        }
        protected Entity()
        {
            
        }

        public Guid Id { get; set; }
    }
}
