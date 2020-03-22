using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Design;

namespace Main
{
    public class Class1
    {
        class Program
        {
            static void Main(string[] args)
            {
                while (true)
                {
                    Op();
                    switch (Console.ReadLine())
                    {
                        case "0":
                            return;
                        case "1":
                            TestPerson();
                            break;
                        case "2":
                            TesTOneToMany();
                            break;
                        case "3":
                            TesTManyToMany();
                            break;
                        default:
                            Console.WriteLine("invalid op");
                            break;
                    }
                }
            }

            static void Op()
            {
                Console.WriteLine("0.Exit");
                Console.WriteLine("1.Person");
                Console.WriteLine("2. Orders and Customer");
                Console.WriteLine("3. Albums and Artists");
                Console.WriteLine("Enter number of operation");
            }

            static void TestPerson()
            {
                using var context = new Model1Container();
                var p = new Person();
                Console.Write("First Name:");
                p.FirstName = Console.ReadLine();
                Console.Write("Last Name:");
                p.LastName = Console.ReadLine();
                Console.Write("Midle Name:");
                p.MidleName = Console.ReadLine();
                Console.Write("Telephon Number:");
                p.Telephone = Console.ReadLine();
                context.PersonSet.Add(p);
                context.SaveChanges();
                Console.WriteLine();
                Console.WriteLine("Table Persons");
                var items = context.PersonSet;
                foreach (var x in items.ToList())
                    Console.WriteLine("{0} {1}", x.Id, x.FirstName);
            }

            static void TesTOneToMany()
            {
                Console.WriteLine("One to many association");
                using var context = new Model1Container();
                Customer c = new Customer();
                Console.WriteLine("Customer:");
                Console.Write("Name:");
                c.Name = Console.ReadLine();
                Console.Write("City:");
                c.City = Console.ReadLine();
                context.CustomerSet.Add(c);

                Console.WriteLine();
                Console.Write("Number of orders:");
                var ordersSize = int.Parse(Console.ReadLine());
                for (int i = 0; i < ordersSize; i++)
                {
                    Console.Write(i + " Value:");
                    var o = new Orders();
                    o.TotalValue = int.Parse(Console.ReadLine());
                    o.Date = DateTime.Now;
                    ;
                    o.Customer = c;
                    context.OrdersSet.Add(o);
                }

                context.SaveChanges();
                Console.WriteLine();
                Console.WriteLine("Customer and Orders");
                var items = context.CustomerSet;
                foreach (var x in items)
                {
                    Console.WriteLine("Customer : {0}, {1}, {2}",
                        x.Id, x.Name, x.City);
                    foreach (var ox in x.Orders)
                        Console.WriteLine("\tOrders: {0}, {1}, {2}",
                            ox.Id, ox.Date, ox.TotalValue);
                }
            }

            static void TesTManyToMany()
            {
                Console.WriteLine("Many to many association");
                using var context = new Model1Container();
                Console.WriteLine("How many albums:");
                var sizeAlbums = int.Parse(Console.ReadLine());
                var listAlbums = new List<Album>();
                for (int i = 0; i < sizeAlbums; i++)
                {
                    Console.Write(i + " Album Name: ");
                    listAlbums.Add(new Album()
                    {
                        AlbumName = Console.ReadLine()
                    });
                }


                Console.Write("How many artist:");
                var sizeArtist = int.Parse(Console.ReadLine());
                var listArtist = new List<Artist>();
                for (int i = 0; i < sizeArtist; i++)
                {
                    var a = new Artist();

                    Console.Write(i + " Artist FirstName: ");
                    a.First = Console.ReadLine();

                    Console.Write(i + " Artist LastName: ");
                    a.LastName = Console.ReadLine();

                    listArtist.Add(a);
                    Console.WriteLine();
                }


                Console.WriteLine("Create association");
                Console.Write("How many:");
                var associations = int.Parse(Console.ReadLine());
                for (int i = 0; i < associations; i++)
                {
                    Console.Write("Artist nr:");
                    var a = listArtist[int.Parse(Console.ReadLine())];
                    Console.Write("Album nr:");
                    if (a.Album == null)
                        a.Album = new List<Album>();
                    a.Album.Add(listAlbums[int.Parse(Console.ReadLine())]);
                    Console.WriteLine();
                }

                foreach (var artist in listArtist)
                {
                    context.ArtistSet.Add(artist);
                }

                context.SaveChanges();
                Console.WriteLine();
                Console.WriteLine("Customer and Orders");
                var items = context.ArtistSet.Include(artist => artist.Album);
                foreach (var x in items)
                {
                    Console.WriteLine("Artist  : {0}, {1}, {2}",
                        x.Id, x.First, x.LastName);
                    foreach (var ox in x.Album)
                        Console.WriteLine("\tAlbums: {0}, {1}",
                            ox.AlbumId, ox.AlbumName);
                }
            }
        }
    }
}