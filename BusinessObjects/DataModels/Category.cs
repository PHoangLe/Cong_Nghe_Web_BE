using System;
using System.Collections.Generic;

namespace BusinessObjects.DataAccess
{
    public partial class Category
    {
        public Category()
        {
            Foods = new HashSet<Food>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
