namespace CenterApp.Core.Models;
public class Stuff
{
    public int Stuff_Id { get; set; }
    public string Stuff_Name { get; set; }
    public string Stuff_Phone { get; set; }
    public string Stuff_Email { get; set; }
    public string Stuff_Specilist { get; set; }
    public string? Stuff_Image { get; set; }
    public DateTime Stuff_BirthOfDate { get; set; }

    public List<Stuff> ToList()
    {
        throw new NotImplementedException();
    }
}
