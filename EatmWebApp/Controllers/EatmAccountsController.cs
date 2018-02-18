using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EatmWebApp.Models;

namespace EatmWebApp.Controllers
{
    public class EatmAccountsController : Controller
    {
        private ApplicationDbContext _context;
        Count myObject = new Count();
        EatmAccount myObject2 = new EatmAccount();

        public EatmAccountsController()
        {
            _context = new ApplicationDbContext();
        }
        //
        // GET: /EatmAccounts/
        [HttpGet]
        public ActionResult CustomerLogin()
        {
            return View("CustomerLogin");

        }
        [HttpPost]
        public ActionResult CustomerLogin(EatmAccount account)
        {
            var customer =
                _context.EatmACcounts.FirstOrDefault(
                    c => c.CardNumber == account.CardNumber && c.Password == account.Password);
            if (customer != null)
            {
                Session["id"] = account.CardNumber;
                ViewBag.Mymessage = ("Choose Your Transaction " + customer.Name);
                return RedirectToAction("Transaction");
            }

            else
            {
                return Content("Wrong");
            }
        }

        public ActionResult Transaction()
        {
            return View("Transaction");
        }

        public ActionResult Balance(int id)
        {
            var BalanceCheck =
                _context.EatmACcounts.Single(
                    c => c.CardNumber == id);
            if (BalanceCheck != null)
            {
                ViewBag.Balanceshow = ("Your  Balance is:" + BalanceCheck.Balance);
                return View("Balance");

            }
            else
            {
                return Content("wrong");
            }

        }

        public ActionResult DipositBalance()
        {
            return View("DipositBalance");
        }

        [HttpPost]
        public ActionResult DipositBalance(EatmAccount addbalance, int id)
        {
            if (addbalance.Balance >= 500)
            {
                var Diposit =
                    _context.EatmACcounts.Single(
                        c => c.CardNumber == id);

                //if (ModelState.IsValid)
                //{

                if (Diposit != null)
                {
                    Diposit.Balance = Diposit.Balance + addbalance.Balance;
                    _context.SaveChanges();
                    return RedirectToAction("Balance", new {id});
                }
                else
                {
                    return RedirectToAction("Transaction");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Diposit al least 500 tk or more");
            }
            
            return View(addbalance);

        }

        [HttpGet]
        public ActionResult Withdraw()
        {
            return View("Withdraw");
        }

        [HttpPost]
        public ActionResult Withdraw(EatmAccount account, int id, Count count)
        {
            int balance = Convert.ToInt32(Request.Form.Get("withdraw"));// To get Value from the Form view
            var currDate = DateTime.Now.ToShortDateString();
            var BalanceCheck =
               _context.EatmACcounts.Single(
                   c => c.CardNumber == id);


            var tCount = _context.Counts.Count(a => a.EatmAccountsId == BalanceCheck.CardNumber && a.Date.Day == DateTime.Today.Day && a.Date.Month == DateTime.Today.Month && a.Date.Year == DateTime.Today.Year);

            if (tCount < 3)
            {

                if (BalanceCheck != null && balance <= 1000)
                {

                    var BalanceWithdraw = BalanceCheck.Balance - balance;
                    // int i = 0;
                    if (500 <= BalanceWithdraw)
                    {
                        BalanceCheck.Balance = BalanceWithdraw;
                        count.EatmAccountsId = id;
                        count.Date = DateTime.Now;
                        _context.Counts.Add(count);
                        _context.SaveChanges();
                        ViewBag.Message = ("Your Current Balance is: " + " " + BalanceWithdraw);
                        return View("Withdraw");
                    }

                }
                else
                {
                    return Content("You can't Withdraw more than 1000" + "\n" + "Your Balance must have 500 Tk");
                }

            }
            else
            {
                ViewBag.Forecolor = System.Drawing.Color.Red;
                ViewBag.Message = " Sorry! 3 Time Transaction Complete";
                return View("Transaction");
            }
            return View("Transaction");

        }
        public ActionResult Exit(int id)
        {
            return View("CustomerLogin");
        }
    }
}
