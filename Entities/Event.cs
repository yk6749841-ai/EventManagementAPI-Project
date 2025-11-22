using System;

namespace EventManagementAPI.Entities
{
    public class Event
    {
        public int Id { get; set; }         
        public string Title { get; set; }         
        public DateTime DateTime { get; set; }    
        public string Location { get; set; }    
        public int Capacity { get; set; }      
        public string Description { get; set; }  
    }
}
