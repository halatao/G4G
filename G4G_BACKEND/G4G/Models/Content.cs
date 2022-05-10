﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace G4G.Models
{
    public partial class Content
    {
        public Content()
        {
            Comment = new HashSet<Comment>();
        }

        public int IdContent { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public int? Views { get; set; }
        public DateTime Posted { get; set; }
        public int AccountIdAccount { get; set; }
        public string AccountUsername { get; set; }
        public int SubcategoryIdSubcategory { get; set; }

        public virtual Account Account { get; set; }
        public virtual SubCategory SubcategoryIdSubcategoryNavigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}