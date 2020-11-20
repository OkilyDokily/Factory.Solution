using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class MachineEngineer
    {
        [Key]
        public int MachineEngineerId {get;set;}
        public int MachineId{get;set;}
        public int EngineerId{get;set;}
        public virtual Machine Machine{get;set;}
        public virtual Engineer Engineer{get;set;}
    }
}