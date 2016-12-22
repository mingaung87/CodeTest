using CodeTest.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeTest.Controllers
{
    public class AmotizeController : Controller
    {
        //
        // GET: /Account/AmotizeSchedule

        [AllowAnonymous]
        public ActionResult AmortizationSchedule()
        {
            return View();
        }

        //
        // POST: /Account/AmortizationSchedule

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AmortizationSchedule(AmotizeModel model)
        {
            if (ModelState.IsValid)
            {

                // Base Variables
                double InterestRate = model.InterestRate;
                if (InterestRate > 1)
                    InterestRate = InterestRate / (12 * 100);

                double LoanAmount = model.LoanAmount;
                int AmortizationTerm = model.Month;
                List<AmotizeListModel> amotizeListModel = CalculateAmotize(InterestRate, LoanAmount, AmortizationTerm);
                Session["Data"] = amotizeListModel;
                return RedirectToAction("AmotizationList");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/AmortizationList

        [AllowAnonymous]
        public ActionResult AmotizationList(int page=1,int pageSize=5)
        {
            List<AmotizeListModel> amotizeListModel = (List<AmotizeListModel>) Session["Data"];
            PagedList<AmotizeListModel> model = new PagedList<AmotizeListModel>(amotizeListModel, page, pageSize);
            return View(model);
        }

        public List<AmotizeListModel> CalculateAmotize(double InterestRate, double LoanAmount, int AmortizationTerm)
        {
            // Iteration Variables
            double CurrentBalance = LoanAmount;
            double MonthlyInterest = 0;
            double CummulativeInterest = 0;
            double MonthlyPrincipal = 0;
            double CummulativePrincipal = 0;

            // Calculate the monthly payment and round it to 2 decimal places
            double MonthlyPayment = (LoanAmount * InterestRate) / (1 - 1 / Math.Pow((1 + InterestRate), (AmortizationTerm)));
            MonthlyPayment = Math.Round(MonthlyPayment, 2);

            // Storage Variable
            AmotizeListModel[] ap = new AmotizeListModel[AmortizationTerm];
            // Loop for amortization term (number of monthly payments)
            for (int j = 0; j < AmortizationTerm; j++)
            {

                // Calculate monthly cycle
                MonthlyInterest = CurrentBalance * InterestRate;
                MonthlyPrincipal = MonthlyPayment - MonthlyInterest;
                CurrentBalance = CurrentBalance - MonthlyPrincipal;



                if (j == AmortizationTerm - 1 && CurrentBalance != MonthlyPayment)
                {
                    // Adjust the last payment to make sure the final balance is 0
                    MonthlyPayment += CurrentBalance;
                    CurrentBalance = 0;
                }



                // Add to cummulative totals
                CummulativeInterest += MonthlyInterest;
                CummulativePrincipal += MonthlyPrincipal;



                ap[j] = new AmotizeListModel
                {
                    Month = j + 1,
                    Payment = Math.Round(MonthlyPayment, 2),
                    Balance = Math.Round(CurrentBalance, 2),
                    MonthlyInterest = Math.Round(MonthlyInterest, 2),
                    CummulativeInterest = Math.Round(CummulativeInterest, 2),
                    MonthlyPrincipal = Math.Round(MonthlyPrincipal, 2),
                    CummulativePrincipal = Math.Round(CummulativePrincipal, 2)
                };

            }

            List<AmotizeListModel> list = ap.ToList<AmotizeListModel>();
            return list;
        }


    }
}
