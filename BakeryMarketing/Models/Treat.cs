using System.Collections.Generic;

namespace BakeryMarketing.Models
{
    public class Treat
    {
        public Treat()
        {
            this.JoinEntities = new HashSet<>();
        }

        public int TreatId { get; set; }
        public string Name { get; set; }
        public virtual ApplicationUser User { get; set; } 
        public virtual ICollection<TreatFlavor> JoinEntities { get;}
    }
}