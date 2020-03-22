using System;
using System.Linq;
using System.Windows.Forms;
using ModelSelfReferences.Case4;
using ModelSelfReferences.Case5;

namespace ModelSelfReferences
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InsertCase1(object sender, EventArgs e)
        {
            using var _context = new Context();
            var initial1 = new SelfReference()
            {
                Name = "initial1"
            };
            var initial2 = new SelfReference()
            {
                Name = "initial1"
            };
            var initial3 = new SelfReference()
            {
                Name = "initial1"
            };

            initial1.ParentSelfReference = initial2;
            initial2.ParentSelfReference = initial3;
            _context.SelfReferences.Add(initial1);
            _context.SaveChanges();
        }

        private void InsertCase2(object sender, EventArgs e)
        {
            using var context = new Context();
            var product = new Product()
            {
                SKU = 147,
                Description = "Expandable Hydration Pack",
                Price = 19.97M,
                ImageURL = "/pack147.jpg"
            };

            context.Products.Add(product);
            product = new Product
            {
                SKU = 186,
                Description = "Range Field Pack",
                Price = 98.97M,
                ImageURL = "/noimage.jp"
            };
            context.Products.Add(product);
            product = new Product
            {
                SKU = 202,
                Description = "Small Deployment Back Pack",
                Price = 29.97M,
                ImageURL = "/pack202.jpg"
            };
            context.Products.Add(product);
            context.SaveChanges();
        }

        private void InsertCase3(object sender, EventArgs e)
        {
            byte[] thumbBits = new byte[100];
            byte[] fullBits = new byte[2000];
            using (var context = new Context())
            {
                var photo = new Photograph
                {
                    Title = "My Dog",
                    ThumbnailBits = thumbBits
                };
                var fullImage = new PhotographFullImage
                {
                    HighResolutionBits =
                        fullBits
                };
                photo.PhotographFullImage = fullImage;
                context.Photographs.Add(photo);
                context.SaveChanges();
            }
        }

        private void InsertCase4(object sender, EventArgs e)
        {
            using var context = new Context();
            var business = new Business
            {
                Name = "Corner Dry Cleaning",
                LicenseNumber = "100x1"
            };
            context.Businesses.Add(business);
            var retail = new Retail
            {
                Name = "Shop and Save", LicenseNumber =
                    "200C",
                Address = "101 Main", City = "Anytown",
                State = "TX", ZIPCode = "76106"
            };
            context.Businesses.Add(retail);
            var web = new ECommerce
            {
                Name = "BuyNow.com", LicenseNumber =
                    "300AB",
                URL = "www.buynow.com"
            };
            context.Businesses.Add(web);
            context.SaveChanges();
        }

        private void InsertCase5(object sender, EventArgs e)
        {
            using var context = new Context();
            var fte = new FullTimeEmployee
            {
                FirstName = "Jane", LastName =
                    "Doe",
                Salary = 71500M
            };
            context.Employees.Add(fte);
            fte = new FullTimeEmployee
            {
                FirstName = "John", LastName = "Smith",
                Salary = 62500M
            };
            context.Employees.Add(fte);
            var hourly = new HourlyEmployee
            {
                FirstName = "Tom", LastName =
                    "Jones",
                Wage = 8.75M
            };
            context.Employees.Add(hourly);
            context.SaveChanges();
        }

        private void LoadCase1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            using var context = new Context();
            listView1.Columns.Clear();
            listView1.Columns.Add("Name");
            listView1.Columns.Add("ParentSelfReferenceId");
            foreach (var self in context.SelfReferences.ToList())
            {
                {
                    string[] row =
                    {
                        self.Name, self.ParentSelfReferenceId.ToString()
                    };
                    var lvi = new ListViewItem(row);
                    listView1.Items.Add(lvi);
                }
            }
        }

        private void LoadCase2(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("SKU");
            listView1.Columns.Add("Description");
            listView1.Columns.Add("Price");
            listView1.Columns.Add("URL");
            using var context = new Context();
            foreach (var p in context.Products)
            {
                string[] row =
                {
                    p.SKU.ToString(), p.Description, p.Price.ToString("C"), p.ImageURL
                };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
            }
        }

        private void LoadCase3(object sender, EventArgs e)
        {
            using var context = new Context();
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Id");
            listView1.Columns.Add("Title");
            listView1.Columns.Add("ThumbnailBits Length");
            listView1.Columns.Add("PhotographFullImage Lenght");

            foreach (var photo in context.Photographs)
            {
                context.Entry(photo)
                    .Reference(p => p.PhotographFullImage)
                    .Load();
                string[] row =
                {
                    photo.PhotoId.ToString(), photo.Title, photo.ThumbnailBits.Length.ToString(),
                    photo.PhotographFullImage.HighResolutionBits.Length.ToString()
                };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
            }
        }

        private void LoadCase4(object sender, EventArgs e)
        {
            using var context = new Context();
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Type");
            listView1.Columns.Add("name");
            listView1.Columns.Add("LicenseNumber");
            listView1.Columns.Add("Address");
            listView1.Columns.Add("City");
            listView1.Columns.Add("State");
            listView1.Columns.Add("Zip");
            listView1.Columns.Add("url");
            foreach (var b in context.Businesses)
            {
                string[] row =
                {
                    "Businesses", b.Name, b.LicenseNumber
                };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
            }

            foreach (var r in context.Businesses.OfType<Retail>())
            {
                string[] row =
                {
                    "Retail Businesses", r.Name, r.LicenseNumber, r.Address, r.City, r.State, r.ZIPCode
                };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
            }

            foreach (var ee in context.Businesses.OfType<ECommerce>())
            {
                string[] row =
                {
                    "eCommerce Businesses", ee.Name, ee.LicenseNumber, "", "", "", "", ee.URL
                };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
            }
        }

        private void LoadCase5(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            using var context = new Context();
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("First name");
            listView1.Columns.Add("last name");
            listView1.Columns.Add("type");
            listView1.Columns.Add("type");
            foreach (var emp in context.Employees)
            {
                bool fullTime = emp is HourlyEmployee ? false : true;
                string[] row =
                {
                    emp.FirstName, emp.LastName, "emp is HourlyEmployee ?", fullTime ? "Full Time" : "Hourly"
                };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
            }

            foreach (var fte in context.Employees.OfType<FullTimeEmployee>())
            {
                string[] row =
                {
                    fte.FirstName, fte.LastName, "---Full Time--"
                };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
            }

            foreach (var hourly in context.Employees.OfType<HourlyEmployee>())
            {
                string[] row =
                {
                    hourly.FirstName, hourly.LastName, "Hourly"
                };
                var lvi = new ListViewItem(row);
                listView1.Items.Add(lvi);
            }
        }
    }
}