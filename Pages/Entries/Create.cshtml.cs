using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System;

namespace Recycle.Pages.Entries
{
    public class CreateModel : PageModel
    {
        public Items items = new Items();
        public string errorMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            items.ItemNo = Request.Form["ItemNo"];
            items.Category = Request.Form["Category"];
            items.ItemName = Request.Form["ItemName"];

            if (string.IsNullOrWhiteSpace(items.ItemNo) || string.IsNullOrWhiteSpace(items.Category) || string.IsNullOrWhiteSpace(items.ItemName))
            {
                errorMessage = "All fields are required.";
                return;
            }
            //SQL connection
            try
            {
                string connectionString = "Data Source=LAPTOP-0P51T4VM\\SQLEXPRESS;Initial Catalog=Recycle;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Items (ItemNo, Category, ItemName) VALUES (@ItemNo, @Category, @ItemName)";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ItemNo", items.ItemNo);
                        command.Parameters.AddWithValue("@Category", items.Category);
                        command.Parameters.AddWithValue("@ItemName", items.ItemName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }

    public class Items
    {
        public string ItemNo { get; set; }
        public string Category { get; set; }
        public string ItemName { get; set; }
    }
}

