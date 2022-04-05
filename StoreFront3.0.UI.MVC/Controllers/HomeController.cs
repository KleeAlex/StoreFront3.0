
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Data;
using System.Data.Entity;
using System.Net;
using StoreFront3._0.UI.MVC;
using StoreFront3._0.UI.MVC.Models;
using StoreFront.DATA.EF;
using MailKit.Net.Smtp;
using MimeKit;
using System.Configuration;
using System.Net.Mail;

namespace StoreFront.Controllers
{
    public class HomeController : Controller
    {

        private DCAccessoriesEntities1 db = new DCAccessoriesEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Shop()
        {
            ViewBag.Message = "View our Wares!";

            return View();
        }




        #region Custom Add-to-Cart Functionality

        public ActionResult AddToCart(int qty, int productID)
        {
            //Create an empty shell for the LOCAL shopping cart variable
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if the Session shopping cart exists. If so, use it to populate the local version
            if (Session["cart"] != null)
            {
                //Session shopping cart exists. Put its items in the local version, which is easier to work with
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //We need to UNBOX the Session object to its smaller, more specific type -- Explicit casting
            }
            else
            {
                //If the Session cart doesn't exist yet, we need to instantiate it to get started
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }//After this if/else, we now have a local cart that's ready to add things to it

            //Find the product they referenced by its ID
            Product product = db.Products.Where(p => p.ProductID == productID).FirstOrDefault();

            if (product == null)
            {
                //If given a bad ID, return the user to some other page to try again.
                //Alternatively, we could throw some kind of error, which we will 
                //discuss further in Module 6.
                return RedirectToAction("Index");
            }
            else
            {
                //If the Book IS valid, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //Put the item in the local cart. If they already have that product as a
                //cart item, the instead we will update the quantity. This is a big part
                //of why we have the dictionary.
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }

                //Now update the SESSION version of the cart so we can maintain that info between requests
                Session["cart"] = shoppingCart; //No explicit casting needed here

            }

            //Send them to View their Cart Items
            return RedirectToAction("Index", "ShoppingCart");

        }

        #endregion


        #region Contact using MailKit

        //1. INSTALL MAILKIT VIA NUGET PACKAGE MANAGER
        //2. ADD using MailKit.Net.Smtp;
        //3. ADD using MimeKit;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //When a class has validation attributes, that validation should be checked
            //BEFORE attempting to process any of the data they provided.

            if (!ModelState.IsValid)
            {
                //Send them back to the form. We can pass the object to the view 
                //so the form will contain the original information they provided.

                return View(cvm);
            }

            //Create the format for the message content we will receive from the contact form
            string message = $"You have received a new email from your site's contact form!<br/>" +
                $"Sender: {cvm.Name}<br/>Email: {cvm.Email}<br/>Subject: {cvm.Subject}<br/>" +
                $"Message: {cvm.Message}";

            //MIME - Multipurpose Internet Mail Extensions - Allows email to contain
            //information other than ASCII, including audio, video, images, and HTML

            //Create a MimeMessage object to assist with storing/transporting the email
            //information from the contact form.
            var mm = new MimeMessage();

            //Even though the user is the one attempting to send a message to us, the actual sender 
            //of the email is the email user we set up with our hosting provider.
            //We can access the credentials for this email user from our AppSecretKeys.config file.
            mm.From.Add(new MailboxAddress(ConfigurationManager.AppSettings["EmailUser"].ToString()));

            //The recipient of this email will be our personal email address, also stored in AppSecretKeys.config.
            mm.To.Add(new MailboxAddress(ConfigurationManager.AppSettings["EmailTo"].ToString()));

            //The subject will be the one provided by the user, which we stored in our cvm object.
            mm.Subject = cvm.Subject;

            //The body of the message will be formatted with the string we created above.
            mm.Body = new TextPart("HTML") { Text = message };

            //We can set the priority of the message as "urgent" so it will be flagged in our email client.
            mm.Priority = MessagePriority.Urgent;

            //We can also add the user's provided email address to the list of ReplyTo addresses
            //so our replies can be sent directly to them instead of the email user on our hosting provider.
            mm.ReplyTo.Add(new MailboxAddress(cvm.Email));

            //The using directive will create the SmtpClient object used to send the email.
            //Once all of the code inside of the using directive's scope has been executed,
            //it will close any open connections and dispose of the object for us.
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                //First, try with the below line commented out. If it doesn't work, uncomment the line
                //below and try again.
                //client.SslProtocols = System.Security.Authentication.SslProtocols.None;

                //Connect to the mail server using credentials in our AppSecretKeys.config.
                client.Connect(ConfigurationManager.AppSettings["EmailClient"].ToString());

                //Log in to the mail server using the credentials for our email user.
                client.Authenticate(

                    //Username
                    ConfigurationManager.AppSettings["EmailUser"].ToString(),

                    //Password
                    ConfigurationManager.AppSettings["EmailPass"].ToString()

                    );

                //It's possible the mail server may be down when the user attempts to contact us, 
                //so we can "encapsulate" our code to send the message in a try/catch.
                try
                {
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    //If there is an issue, we can return the user to the View with their form 
                    //information intact and present an error message.
                    ViewBag.ErrorMessage = $"There was an error processing your request. Please " +
                        $"try again later.<br/>Error Message: {ex.StackTrace}";

                    return View(cvm);
                }

            }

            //If all goes well, return a View that displays a confirmation to the user
            //that their email was sent.

            return View("EmailConfirmation", cvm);
        }

        #endregion

    }
}