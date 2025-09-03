using CrystalCarCare.Models;  // Add this namespace
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CrystalCarCare.Controllers
{
    public class UserProfileController : Controller  // Added inheritance
    {
        private static ProfileModel _currentUser = new ProfileModel
        {
            Id = 1,
            Name = "Sumaiya Aftab",
            Email = "sumaiya@example.com",
            Phone = "+8801714123390",
            ProfilePicture = "/Content/images/default-avatar.png",
            DateOfBirth = new DateTime(1990, 1, 1),
            Address = "Dhaka, Bangladesh",
            Bio = "Software developer with passion for web technologies."
        };

        // GET: UserProfile
        public ActionResult Index()
        {
            return View(_currentUser);
        }

        // GET: UserProfile/Edit
        public ActionResult Edit()
        {
            return View(_currentUser);
        }

        // POST: UserProfile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileModel model, HttpPostedFileBase profilePictureFile)
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
                        // Create directory if it doesn't exist
                        string profilesDirectory = Server.MapPath("~/Content/images/profiles/");
                        if (!Directory.Exists(profilesDirectory))
                        {
                            Directory.CreateDirectory(profilesDirectory);
                        }

                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePictureFile.FileName);
                        string path = Path.Combine(profilesDirectory, fileName);
                        profilePictureFile.SaveAs(path);
                        _currentUser.ProfilePicture = "/Content/images/profiles/" + fileName;
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

        // Add a test action for debugging
        public ActionResult Test()
        {
            return Content("UserProfile controller is working!");
        }
    }
}