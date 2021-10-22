using System.Collections.Generic;

namespace ProjectName.Models
{
    public class Class1
    {
        public Class1()
        {
            this.JoinEntities = new HashSet<>();
        }

        public int Class1Id { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser User { get; set; } 
        public virtual ICollection<Class1Class2> JoinEntities { get;}
    }
}