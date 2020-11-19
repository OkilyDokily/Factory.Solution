using System.Collections.Generic;

namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId {get;set;}
        public string Name{get;set;}
        public ICollection<MachineEngineers> Machines {get;set;}

        public Engineer()
        {
            this.Machines = new HashSet<MachineEngineers>();
        }
    }
}