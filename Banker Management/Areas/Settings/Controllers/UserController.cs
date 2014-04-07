using BM.Models;
using BM.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BM.DataAccess;

namespace BM.Web.Areas.Settings.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        IRepository<User> repository;
        IUserRepository userRepository;
        public UserController()
        {
            repository = new Repository<User>();
            userRepository = new UserRepository();
        }
        //
        // GET: /Admin/User/

        public ActionResult Index()
        {
            return View(repository.Get<User>());//.Include(u => u.Address));
        }

        //
        // GET: /Admin/User/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = new User();
            user = repository.Get<User>().Where(u => Convert.ToInt32(u.Id) == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Admin/User/Create

        public ActionResult Create()
        {
            ViewBag.AssignedCompanies = repository.Get<Company>().ToList().Select(C => new SelectListItem
                                {
                                    Text = C.Name,
                                    Value = C.CompanyId.ToString()
                                }).ToList();
            return View();
        }

        //
        // POST: /Admin/User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, Guid[] selectedCompanies)
        {
            if (ModelState.IsValid)
            {
                userRepository.CreateUser(user, user.PasswordHash);
                User insertedUser = repository.Get<User>().Where(U => U.UserName.ToLower().Equals(user.UserName.ToLower())).FirstOrDefault();
                if (selectedCompanies != null)
                {
                    var selectedSpecialtyHS = new HashSet<Guid>(selectedCompanies);
                    var companyList = new List<Company>();
                    companyList = repository.Get<Company>().Where(C => selectedCompanies.Contains(C.CompanyId)).ToList();
                    insertedUser.Companies = companyList;
                    repository.Update<User>(insertedUser);
                    repository.SaveChanges();
                    ViewBag.AssignedCompanies = repository.Get<Company>().ToList().Select(C => new SelectListItem
                    {
                        Text = C.Name,
                        Value = C.CompanyId.ToString(),
                        Selected = (companyList.Where(c => c.CompanyId.ToString().Contains(C.CompanyId.ToString())).Count() > 0) ? true : false
                    }).ToList();
                }
                return RedirectToAction("GrantRolesToUser", new { id = user.UserName });

               // ModelState.AddModelError("", "This Location Already Exists");
            }
            return View(user);
        }

        /// <summary>
        /// An Ajax method to check if a username is unique.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckForUniqueUser(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);
            JsonResponse response = new JsonResponse();
            response.Exists = (user == null) ? false : true;

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/User/Edit/5
        private void PopulateAssignedComapnies(User user)
        {
            //var allComapnies = specialtyRepository.Get();
            //var userComapnies = new HashSet<int>(user.Comapnies.Select(c => c.ComapniesId));
            //var viewModel = new List<AssignedComapnies>();
            //foreach (var specialty in allComapnies)
            //{
            //    viewModel.Add(new AssignedComapnies
            //    {
            //        ComapniesId = specialty.ComapniesId,
            //        Name = specialty.Name,
            //        Assigned = userComapnies.Contains(specialty.ComapniesId)
            //    });
            //}
            //  ViewBag.AssignedComapnies = viewModel;
        }

        public ActionResult Edit(Guid id)
        {
            User user = new User();
            user = repository.GetById<User>(id);
            PopulateAssignedComapnies(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        //
        // POST: /Admin/User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, string[] selectedComapnies)
        {
            if (ModelState.IsValid)
            {
                // userRepository.Update(user, selectedComapnies);
                return RedirectToAction("Index");
            }
            PopulateAssignedComapnies(user);
            return View(user);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserViewModel LocalPasswordModel)
        {
            if (ModelState.IsValid)
            {
                //WebSecurity.ChangePassword(User.Identity.Name, LocalPasswordModel.OldPassword, LocalPasswordModel.NewPassword);
                return RedirectToAction("Index");
            }
            return View(LocalPasswordModel);
        }

        //
        // GET: /Admin/User/Delete/5

        public ActionResult Delete(Guid id)
        {
            User userprofile = repository.GetById<User>(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Admin/User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repository.Delete<User>(id);
            return RedirectToAction("Index");
        }

        #region Grant Users with Roles Methods

        /// <summary>
        /// Return two lists:
        ///   1)  a list of Roles not granted to the user
        ///   2)  a list of Roles granted to the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ActionResult GrantRolesToUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            GrantRolesToUserViewModel model = new GrantRolesToUserViewModel();
            model.UserName = id;
            model.AvailableRoles = (string.IsNullOrEmpty(id) ? new SelectList(Roles.GetAllRoles().Where(R => R != "Administrator")) : new SelectList( Roles.GetAllRoles().Where(R => R != "Administrator").Except(Roles.GetRolesForUser(id))));
            model.GrantedRoles = (string.IsNullOrEmpty(id) ? new SelectList(new string[] { }) : new SelectList(Roles.GetRolesForUser(id)));

            return View(model);
        }

        /// <summary>
        /// Grant the selected roles to the user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GrantRolesToUser(string userName, string roles)
        {
            JsonResponse response = new JsonResponse();

            if (string.IsNullOrEmpty(userName))
            {
                response.Success = false;
                response.Message = "The userName is missing.";
                return Json(response);
            }

            string[] roleNames = roles.Substring(0, roles.Length - 1).Split(',');

            if (roleNames.Length == 0)
            {
                response.Success = false;
                response.Message = "No roles have been granted to the user.";
                return Json(response);
            }

            try
            {
                Roles.AddUserToRoles(userName, roleNames);

                response.Success = true;
                response.Message = "The Role(s) has been GRANTED successfully to " + userName;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "There was a problem adding the user to the roles.";
            }

            return Json(response);
        }

        /// <summary>
        /// Revoke the selected roles for the user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleNames"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RevokeRolesForUser(string userName, string roles)
        {
            JsonResponse response = new JsonResponse();

            if (string.IsNullOrEmpty(userName))
            {
                response.Success = false;
                response.Message = "The userName is missing.";
                return Json(response);
            }

            if (string.IsNullOrEmpty(roles))
            {
                response.Success = false;
                response.Message = "Roles is missing";
                return Json(response);
            }

            string[] roleNames = roles.Substring(0, roles.Length - 1).Split(',');

            if (roleNames.Length == 0)
            {
                response.Success = false;
                response.Message = "No roles are selected to be revoked.";
                return Json(response);
            }

            try
            {
                Roles.RemoveUserFromRoles(userName, roleNames);

                response.Success = true;
                response.Message = "The Role(s) has been REVOKED successfully for " + userName;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "There was a problem revoking roles for the user.";
            }

            return Json(response);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}