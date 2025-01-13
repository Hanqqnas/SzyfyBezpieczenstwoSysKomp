using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SzyfyBezpieczenstwoSysKomp.Models;

namespace SzyfyBezpieczenstwoSysKomp.Controllers
{
    public class CezarController : Controller
    {
        private readonly ILogger<CezarController> _logger;

        public CezarController(ILogger<CezarController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("~/Views/SzyfrCezara/Cezar.cshtml", new CezarModel());
        }

        [HttpPost]
        public IActionResult Index(CezarModel model, string operacja)
        {
            _logger.LogInformation(
                "Operacja: {Operacja}, Tekst przed szyfrowaniem: {Tekst}, Przesunięcie: {Przesuniecie}",
                operacja, model.TekstPrzedSzyfrowaniem, model.Przesuniecie);
            if (string.IsNullOrEmpty(model.TekstPrzedSzyfrowaniem))
            {
                ModelState.AddModelError("TekstPrzedSzyfrowaniem", "Tekst do szyfrowania nie może być pusty.");
            }
            if (model.Przesuniecie == 0)
            {
                ModelState.AddModelError("Przesuniecie", "Przesunięcie nie może wynosić 0.");
            }
            if (ModelState.IsValid)
            {
                int przesuniecie = operacja == "Odszyfrowanie" ? -model.Przesuniecie : model.Przesuniecie;
                model.TekstZaszyfrowany = Szyfrowanie(model.TekstPrzedSzyfrowaniem, przesuniecie);
            }

            return View("~/Views/SzyfrCezara/Cezar.cshtml", model);
        }

        private string Szyfrowanie(string tekst, int przesuniecie)
        {
            string maleLitery = "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż";
            string duzeLitery = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";

            char[] znaki = tekst.ToCharArray();
            for (int i = 0; i < znaki.Length; i++)
            {
                char znak = znaki[i];
                if (char.IsLetter(znak))
                {
                    if (maleLitery.Contains(znak))
                    {
                        int indeks = (maleLitery.IndexOf(znak) + przesuniecie) % maleLitery.Length;
                        if (indeks < 0) indeks += maleLitery.Length;
                        znaki[i] = maleLitery[indeks];
                    }
                    else if (duzeLitery.Contains(znak))
                    {
                        int indeks = (duzeLitery.IndexOf(znak) + przesuniecie) % duzeLitery.Length;
                        if (indeks < 0) indeks += duzeLitery.Length;
                        znaki[i] = duzeLitery[indeks];
                    }
                }
            }

            return new string(znaki);
        }
    }
}
