using System.Numerics;

namespace SzyfyBezpieczenstwoSysKomp.Models.Polibiusz;

public class PolibiuszResultModel
{
    public char[,] Grid { get; set; } 
    public string EncryptedText { get; set; } 
    public BigInteger Result { get; set; }
    
}