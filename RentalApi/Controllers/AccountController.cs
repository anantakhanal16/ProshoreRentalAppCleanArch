using ApplicationLayer.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using RentalApi.DTOs;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using PdfSharpCore;
using PdfSharpCore.Pdf;



namespace RentalApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register-house-seeker")]
        public async Task<IActionResult> RegisterHouseSeeker([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserRole = "HouseSeeker"
                };

                var result = await _authService.RegisterAsync(user, model.Password, user.UserRole);

                if (result)
                {
                    return Ok(new { Message = "House seeker registered successfully" });
                }
            }

            return BadRequest(new { Message = "Invalid registration data" });
        }

        [HttpPost("Register-as-broker")]
        public async Task<IActionResult> RegisterAsBroker([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserRole = "Broker"
                };

                var result = await _authService.RegisterAsync(user, model.Password, user.UserRole);

                if (result)
                {
                    return Ok(new { Message = "Registered successfully as broker" });
                }
                else
                {
                    return BadRequest(new { Message = "Error while registering" });
                }
            }

            return BadRequest(new { Message = "Invalid registration data" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var signInResult = await _authService.SignInAsync(model.Email, model.Password, model.RememberMe);

            if (signInResult)
            {
                return Ok("Login successful");
            }

            return BadRequest("Invalid login attempt");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();

            return Ok("Signout successful");
        }
        public class Transaction
        {
            public DateTime TransactionDate { get; set; }
            public string TransactionId { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public string CustomerName { get; set; }
            public string ProductName { get; set; }
            public string PaymentMethod { get; set; }
            public string Currency { get; set; }
            public string Category { get; set; } // Adding a category field
            public string Location { get; set; } // Adding a location field
            public bool IsApproved { get; set; } // Adding a flag for approval status
                                                 // Add more fields as needed
        }


        private IEnumerable<Transaction> GenerateDummyTransactions()
        {
            return new List<Transaction>
    {
        new Transaction
        {
            TransactionDate = DateTime.Now,
            TransactionId = Guid.NewGuid().ToString(),
            Amount = 1000.00m,
            Description = "Dummy transaction 1",
            CustomerName = "John Doe",
            ProductName = "Sample Product",
            PaymentMethod = "Credit Card",
            Currency = "USD",
            Category = "Shopping",
            Location = "Online",
            IsApproved = true
            // Set values for additional fields as needed
        },
        // Add more dummy transactions with different values for testing
    };
        }


        [HttpGet("generatepdf")]
        public async Task<IActionResult> GeneratePDF()
        {
            var document = new PdfDocument();

            var transactions = GenerateDummyTransactions();
            foreach (var transaction in transactions)
            {
                string htmlcontent = $@"
        <div style='width:50%; text-align:center; font-family: Arial, sans-serif;'>
            <h1 style='font-size: 24px; margin-bottom: 20px;'>Transaction Details</h1>
            <table style='width: 100%; max-width: 5.27in; border-collapse: collapse; margin-bottom: 20px;'>
                <tr>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Transaction ID</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Description</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Transaction Date</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Amount</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Customer Name</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Product Name</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Payment Method</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Currency</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Category</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Location</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Is Approved</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Is Approved</th>
                    <th style='border: 1px solid #000; padding: 8px; font-weight: bold; font-size: 16px;'>Is Approved</th>
                </tr>
                <tr>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.TransactionId}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.Description}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.TransactionDate.ToString("yyyy-MM-dd")}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.Amount.ToString("C")}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.CustomerName}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.ProductName}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.PaymentMethod}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.Currency}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.Category}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.Location}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.IsApproved}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.IsApproved}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.IsApproved}</td>
                    <td style='border: 1px solid #000; padding: 8px; font-size: 14px;'>{transaction.IsApproved}</td>
                </tr>
            </table>
        </div>
        ";

                PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);
            }


            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string Filename = "Invoice_" + "1" + ".pdf";
            return File(response, "application/pdf", Filename);
        }




    }
}



