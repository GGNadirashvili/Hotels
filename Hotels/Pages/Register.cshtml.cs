using Hotels.Data;
using Hotels.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotels.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext db;

        [BindProperty]
        public Register? Register { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Guid customerGuid = Guid.NewGuid();

                await CreateCustomer(customerGuid.ToString());

                var user = new IdentityUser()
                {
                    UserName = Register!.Email,
                    Email = Register.Email,
                    EmailConfirmed = true,
                    Id = customerGuid.ToString()
                };

                var result = await userManager.CreateAsync(user, Register.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
        private async Task CreateCustomer(string customerGuid)
        {
            var customer = new Customer
            {
                Name = Register!.Name,
                LastName = Register.LastName,
                Email = Register.Email,
                Number = Register.Number,
                CustomerGuid = customerGuid
            };

            db.Customers.Add(customer);
            await db.SaveChangesAsync();
        }
    }
}
