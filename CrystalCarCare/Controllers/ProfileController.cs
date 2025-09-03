using CrystalCarCare.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CrystalCarCare.Controllers
{
    public class ProfileController : Controller
    {
        private static UserProfile _currentUser = new UserProfile
        {
            Id = 1,
            Name = "Sumaiya Aftab",
            Email = "example@example.com",
            Phone = "+880 1XXXXXXXXX",
            ProfilePicture = "/Content/images/default-avatar.png", // FIXED PATH
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "Dhaka, Bangladesh",
            Bio = "Software developer with passion for web technologies."
        };

        // GET: Profile
        public ActionResult Index()
        {
            return View(_currentUser);
        }

        // GET: Profile/Edit
        public ActionResult Edit()
        {
            return View(_currentUser);
        }

        // POST: Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile model, HttpPostedFileBase profilePictureFile)
        {
            if (ModelState.IsValid)
            {
                // Update user profile
                _currentUser.Name = model.Name;
                _currentUser.Email = model.Email;
                _currentUser.Phone = model.Phone;
                _currentUser.DateOfBirth = model.DateOfBirth;
                _currentUser.Address = model.Address;
                _currentUser.Bio = model.Bio;

                // Handle profile picture upload
                if (profilePictureFile != null && profilePictureFile.ContentLength > 0)
                {
                    try
                    {
                        // Create directory if it doesn't exist - FIXED PATH
                        string profilesDirectory = Server.MapPath("~/Content/images/profiles/");
                        if (!Directory.Exists(profilesDirectory))
                        {
                            Directory.CreateDirectory(profilesDirectory);
                        }

                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePictureFile.FileName);
                        string path = Path.Combine(profilesDirectory, fileName);
                        profilePictureFile.SaveAs(path);
                        _currentUser.ProfilePicture = "/Content/images/profiles/" + fileName; // FIXED PATH
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error uploading image: " + ex.Message);
                    }
                }

                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // ADD THIS ACTION FOR TESTING
        public ActionResult Test()
        {
            return Content("Profile controller is working! Accessible at: /Profile/Test");
        }
    }
}