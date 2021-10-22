using System.Collections.Generic;

namespace ProjectName.Models
{
  public class Class2
    {
        public Class2()
        {
            this.JoinEntities = new HashSet<>();
        }

        public int Class2Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<> JoinEntities { get; set; }
    }
}