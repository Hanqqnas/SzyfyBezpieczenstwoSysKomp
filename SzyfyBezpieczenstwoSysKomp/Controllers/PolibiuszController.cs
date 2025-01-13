using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using SzyfyBezpieczenstwoSysKomp.Models;
using SzyfyBezpieczenstwoSysKomp.Models.Polibiusz;

namespace SzyfyBezpieczenstwoSysKomp.Controllers
{
    public class PolibiuszController : Controller
    {
        private readonly PolibiuszModel _polibiuszModel;

        public PolibiuszController()
        {
            _polibiuszModel = new PolibiuszModel();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Polibiusz/Index.cshtml");
        }
        
        [HttpPost]
        public IActionResult Encrypt(string inputText)
        {
            var grid = _polibiuszModel.GenerateRandomGrid();
            var encrypted = _polibiuszModel.EncryptWithPolibius(inputText, grid);

            string encryptedNumberString = encrypted.Replace(" ", ""); 
            BigInteger x = BigInteger.Parse(encryptedNumberString); 

            Random random = new Random();
            int a = random.Next(1, 11); 
            int b = random.Next(1, 11); 
            BigInteger y = (a * x * x )+ b; 

            var model = new PolibiuszResultModel
            {
                Grid = grid,
                EncryptedText = encrypted,
                Result = y
            };

            return View("~/Views/Polibiusz/Index.cshtml", model);
        }
        
        public static BigInteger Sqrt(BigInteger value)
        {
            if (value < 0)
                throw new ArgumentException("Nie można obliczyć pierwiastka kwadratowego z liczby ujemnej.");

            if (value == 0)
                return 0;

            BigInteger n = (value / 2) + 1;
            BigInteger n1 = (n + (value / n)) / 2;

            while (n1 < n)
            {
                n = n1;
                n1 = (n + (value / n)) / 2;
            }

            return n;
            
        }
    }
}