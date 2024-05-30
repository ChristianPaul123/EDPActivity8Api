using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;


namespace CsharpApi
{
    public partial class Main : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Main()
        {
            InitializeComponent();
        }

        private async void btnGet_Click_1(object sender, EventArgs e)
        {
            try
            {
                Display.Clear();
                HttpResponseMessage response = await client.GetAsync("http://localhost/myphpapi/api.php");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Display.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnPost_Click_1(object sender, EventArgs e)
        {
            var newBook = new
            {
                book_title = Title_box.Text, // Example field, use txtBookTitle.Text if you have that field
                book_author = Author_box.Text, // Example author_id
                book_category = Category_box.Text, // Example category_id
                book_number = Number_box.Text, // Example book number
                book_published = Published_box.Text, // Example published date
                book_quantity = Quantity_box.Text, // Example quantity
                date_added = DateTime.Now.ToString("yyyy-MM-dd") // Example date added
            };
            string json = JsonConvert.SerializeObject(newBook);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost/myphpapi/api.php", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Display.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private async void btnPost_Click(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}
