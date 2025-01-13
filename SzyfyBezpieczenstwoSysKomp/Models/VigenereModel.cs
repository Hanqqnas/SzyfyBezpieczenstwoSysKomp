namespace SzyfyBezpieczenstwoSysKomp.Models;

public class VigenereModel
{
    public string Message { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public bool IsEncrypting { get; set; }
}
