namespace DataAccess.DTO
{
    public class BookDTO
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public float Cost { get; set; }
        public int Page { get; set; }
        public string Author { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string BookImageURL { get; set; }
        public string BookISBN { get; set; }
        public string Description { get; set; }      
        public int SellerID { get; set; }
        public string SellerName { get; set; }

    }
}
