using CodeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CodeTest.Controllers
{
    public class Question1Controller : Controller
    {
        //
        // GET: /Question1/Question1

        [AllowAnonymous]
        public ActionResult Question1()
        {
            return View();
        }

        //
        // POST: /Question1/Question1
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Question1(Question1Model model, String Command)
        {
            if (Command == "ReverseWord")
                model.output = ReverseWordOrder(model.input);
            else if (Command == "ReverseCharacters")
                model.output = ReverseCharacters(model.input);
            else if (Command == "AlphabeticallySortWords")
                model.output = AlphabeticallySortWords(model.input);
            else if (Command == "Encrypt")
                model.output = SHA512Generator(model.input);
            return View(model);
        }

        public string ReverseWordOrder(String input)
        {
            return String.Join(" ", input.Split(' ').Reverse().ToArray());
        }

        public string ReverseCharacters(string input)
        {
            return ReverseWordOrder(new string(input.Reverse().ToArray()));
        }

        public string AlphabeticallySortWords(string input)
        {
            string[] result = input.Split(' ');
            Array.Sort(result);
            return String.Join(" ", result);
        }

        public string SHA512Generator(string strInput)
        {
            SHA384 sha384 = new SHA384CryptoServiceProvider();
            //provide the string in byte format to the ComputeHash method. 
            //This method returns the SHA-384 hash code in byte array
            byte[] arrHash = sha384.ComputeHash(Encoding.UTF8.GetBytes(strInput));
            // use a Stringbuilder to append the bytes from the array to create a SHA-384 hash code string.
            StringBuilder sbHash = new StringBuilder();
            // Loop through byte array of the hashed code and format each byte as a hexadecimal code.
            for (int i = 0; i < arrHash.Length; i++)
            {
                sbHash.Append(arrHash[i].ToString("x2"));
            }

            // Return the hexadecimal SHA-384 hash code string.
            return sbHash.ToString();
        }

    }
}
