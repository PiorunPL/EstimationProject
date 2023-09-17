namespace Domain;

public class Requirement
{
    public String Name { get; set; }
    public String Description { get; set; }
    public int Estimation { get; set; } //TODO: Change estimation type from int to some dedicated type. More likely to use some universal inteface 
}