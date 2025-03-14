using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class PersistableEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime? CreatedOn { get; private set; }

        public DateTime? ModifiedOn { get; private set;}

        public void SetCreatedOn(DateTime? now)
        {
            CreatedOn = now;
            ModifiedOn = now;
        }

        public void SetModifiedOn(DateTime? now)
        { 
            ModifiedOn = now;
        }
    }
}
