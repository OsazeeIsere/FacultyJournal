﻿namespace FacultyJournal.Controllers
{
    public class AdministrationController: Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new()
                {
                    Name = model.Name
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return View("Error");
            }
            var model = new EditRoleVM
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleVM model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return View("Error");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUserRole(string id)
        {
            ViewBag.roleId = id;
            var role = await roleManager.FindByIdAsync(id);
            ViewBag.roleName = role.Name.ToString();
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id: {id} cannot be found";
                return View("Error");
            }
            var model = new List<UserRoleVM>();

            foreach (var user in userManager.Users.ToList())
            {
                var userRoleVM = new UserRoleVM
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    //IsSelected = true

                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;
                }
                model.Add(userRoleVM);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserRole(List<UserRoleVM> model, string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return View("Error");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("index", "administration");

                };

            }
            return RedirectToAction("Edit", new { Id = id });
        }

        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);

        }
    }
}
