namespace Bubberbreakfast.Models;

public class Breakfast {
    public Guid Id {get;}
    public string Name {get;}
    public string Description {get;}
    public DateTime StartDateTime {get;}
    public DateTime EndDateTime {get;}
    public DateTime LastModifiedDateTime {get;}
    public List<string> Savory {get;}
    public List<string> Sweet {get;}

    public Breakfast (
        Guid id, 
        string name, 
        string description, 
        DateTime startDatetime, 
        DateTime endDatetime, 
        DateTime lastModifiedDateTime,
        List<string> savory,
        List<string> sweet
        ) {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDatetime;
        EndDateTime = endDatetime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

}