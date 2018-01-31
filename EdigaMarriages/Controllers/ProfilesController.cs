using EdigaMarriages.Models;
using EdigaMarriages.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdigaMarriages.Controllers
{
    public class ProfilesController : Controller
    {
        private MarriagesDB marriagesDB;

        public ProfilesController()
        {
            marriagesDB = new MarriagesDB();
        }

        private bool isAdmin(HttpRequestBase httpRequest)
        {
            HttpCookie aCookie = Request.Cookies["admin"];
            if (aCookie != null)
            {
                return Convert.ToBoolean(aCookie.Value);
            }
            else
            {
                return false;
            }
        }

        // GET: Matches
        public ActionResult Index(int page = 1, int pageSize = 10)
        {

            Search search = null;
            HttpCookie aCookie = Request.Cookies["search"];
            if (aCookie != null)
            {
                string searchString = HttpUtility.UrlDecode(aCookie.Value);
                search = Newtonsoft.Json.JsonConvert.DeserializeObject<Search>(searchString);
            }

            ProfilesIndex profilesIndex = new ProfilesIndex();
            profilesIndex.SearchFilter = search;
            profilesIndex.ProfilesList = marriagesDB.GetProfiles(page, pageSize, search);
            profilesIndex.TotalProfiles = marriagesDB.GetProfilesCount(search);
            profilesIndex.CurrentPage = page;
            profilesIndex.PageSize = pageSize;
            profilesIndex.IsAdmin = isAdmin(Request);

            return View(profilesIndex);
        }


        public ActionResult New()
        {
            MProfile profile = new MProfile();
            //profile.DateOfBirth = DateTime.Today.Date;
            return View(profile);
        }

        public ActionResult Edit(string profileID)
        {

            MProfile profile = marriagesDB.GetProfile(profileID);

            return View("New", profile);
        }

        public string Save(bool createNew)
        {
            MProfile profile;
            try
            {
                HttpCookie aCookie = Request.Cookies["profile"];
                if (aCookie != null)
                {
                    string profile2 = HttpUtility.UrlDecode(aCookie.Value);
                    profile = Newtonsoft.Json.JsonConvert.DeserializeObject<MProfile>(profile2);
                }
                else
                {
                    profile = new MProfile();
                }

                string response = marriagesDB.SaveProfile(profile, createNew);

                if (createNew)
                {
                    return "Saved pofile with ID " + response;
                }
                else
                {
                    return "Updated profile " + profile.ProfileID;
                }


            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }

        }

        public ActionResult Details(string profileID)
        {
            ViewBag.IsAdmin = isAdmin(Request);
            MProfile profile = marriagesDB.GetProfile(profileID);

            return View(profile);
        }

    }
}