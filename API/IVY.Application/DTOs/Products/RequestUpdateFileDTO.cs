namespace IVY.Application.DTOs;

public class RequestUpdateFileDTO
{   
    public int Psc_Id { get; set; }
    public List<ProductSubColorFileAddFileDTO>? FileAdds { get; set; }
    public List<ProductSubColorFileUpdateFileDTO>? FileUpdates { get; set; }
    public List<int>? RemoveIds { get; set; }
}