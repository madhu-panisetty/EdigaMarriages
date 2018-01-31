using EdigaMarriages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdigaMarriages.ViewModels
{
    public class ProfilesIndex
    {
        public List<MProfile> ProfilesList { get; set; }
        public Search SearchFilter { get; set; }
        public int TotalProfiles { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool IsAdmin { get; set; }
    }
}