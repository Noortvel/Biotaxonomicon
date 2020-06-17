using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biotaxonomicon.Models.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Biotaxonomicon.Pages.Cabinet.Tag
{
    public class EditModel : PageModel
    {
        public Models.Tag Tag;
        public string WarningInfo
        {
            private set;
            get;
        } = "";
        public string SuccesInfo
        {
            private set;
            get;
        } = "";
        public IActionResult OnGet(int id)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            Tag = Models.Tag.GetById(id);
            if(Tag == null)
            {
                return RedirectToPage("/Auth/Login");
            }
            return Page();
        }
        public IActionResult OnPost(int? id, string latinNameInput, string russianNameInput)
        {
            if (!HttpContext.Session.Keys.Contains("auid"))
            {
                return RedirectToPage("/Auth/Login");
            }
            if (id.HasValue && latinNameInput != null && russianNameInput != null)
            {
                var tag = new Models.Tag { Id = id.Value, LatinName = latinNameInput, RussianName = russianNameInput };
                try
                {
                    tag.Update();
                }
                catch (InfoException e)
                {
                    WarningInfo = e.Message;
                }
                if(WarningInfo == "")
                    SuccesInfo = StringResources.StringResources.Instance.SuccesCreated;
            }
            return RedirectToPage("./Index");
        }
    }
}