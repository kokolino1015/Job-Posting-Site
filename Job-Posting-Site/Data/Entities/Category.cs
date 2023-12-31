﻿using Job_Posting_Site.Data.Entities.Account;

namespace Job_Posting_Site.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationUser Owner { get; set; }
        public bool IsDeleted { get; set; }
    }
}
