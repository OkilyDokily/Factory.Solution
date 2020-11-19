namespace Factory.Models
{
    public class MachineEngineers
    {
        public int MachineEngineersId {get;set;}
        public int MachineId{get;set;}
        public int EngineerId{get;set;}
        public virtual Machine Machine{get;set;}
        public virtual Engineer Engineer{get;set;}
    }
}