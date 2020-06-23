using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models{
    public class ApiDbContext : DbContext{
        public DbSet<Building> Building { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Campaign> Campaign { get; set; }

        public ApiDbContext() { }

        public ApiDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            List<Building> listBuildings = new List<Building>();
            listBuildings.Add(new Building { IdBuilding = 1, Street = "A", StreetNumber = 1, City = "Warszawa", Height = 50.5m});
            listBuildings.Add(new Building { IdBuilding = 2, Street = "B", StreetNumber = 2, City = "Warszawa", Height = 30.0m});
            listBuildings.Add(new Building { IdBuilding = 3, Street = "C", StreetNumber = 3, City = "Warszawa", Height = 70.5m});
            listBuildings.Add(new Building { IdBuilding = 4, Street = "D", StreetNumber = 4, City = "Warszawa", Height = 34.5m });
            modelBuilder.Entity<Building>().HasData(listBuildings);


            List<Client> listClients = new List<Client>();
            listClients.Add(new Client { IdClient = 1, FirstName = "Jan", LastName = "A", Email = "Jan@maiil.com", Phone = "100 200 300", Login = "JanA", Password = "abbbb"});
            listClients.Add(new Client { IdClient = 2, FirstName = "Adam", LastName = "B", Email = "Adam@maiil.com", Phone = "200 300 400", Login = "Adam1", Password = "abssss" });
            listClients.Add(new Client { IdClient = 3, FirstName = "Anna", LastName = "C", Email = "Anna@maiil.com", Phone = "900 888 111", Login = "Anna1", Password = "asdadabbb" });
            listClients.Add(new Client { IdClient = 4, FirstName = "Karol", LastName = "D", Email = "Karol@maiil.com", Phone = "000 333 444", Login = "Karol", Password = "abadsadxxbbb" });
            modelBuilder.Entity<Client>().HasData(listClients);

            List<Banner> listBanners = new List<Banner>();
            listBanners.Add(new Banner { IdAdvertisement = 1, Name = 1, Price = 100.99m, IdCampaing = 1, Area = 1000.0m });
            listBanners.Add(new Banner { IdAdvertisement = 2, Name = 2, Price = 2000.5m, IdCampaing = 2 , Area = 500.0m });
            listBanners.Add(new Banner { IdAdvertisement = 3, Name = 3, Price = 30000.0m, IdCampaing = 3, Area = 100.0m });
            listBanners.Add(new Banner { IdAdvertisement = 4, Name = 4, Price = 40000.5m, IdCampaing = 4, Area = 2500.0m });
            modelBuilder.Entity<Banner>().HasData(listBanners);

            List<Campaign> listCampaigns = new List<Campaign>();
            listCampaigns.Add(new Campaign { IdCampaign = 1, IdClient = 1, StartDate = DateTime.Parse("2020-3-1"), EndDate = DateTime.Parse("2020-3-29"), PricePerSquareMeter = 5000.0m, FromIdBuilding = 1, ToldBuilding = 2}) ;
            listCampaigns.Add(new Campaign { IdCampaign = 2, IdClient = 2, StartDate = DateTime.Parse("2020-4-1"), EndDate = DateTime.Parse("2020-5-1"), PricePerSquareMeter = 10000.5m, FromIdBuilding = 2, ToldBuilding = 3});
            listCampaigns.Add(new Campaign { IdCampaign = 3, IdClient = 3, StartDate = DateTime.Parse("2020-5-2"), EndDate = DateTime.Parse("2020-8-2"), PricePerSquareMeter = 20000.0m, FromIdBuilding = 3, ToldBuilding = 4});
            listCampaigns.Add(new Campaign { IdCampaign = 4, IdClient = 4, StartDate = DateTime.Parse("2020-8-14"), EndDate = DateTime.Parse("2021-3-2"), PricePerSquareMeter = 100000.99m, FromIdBuilding = 4, ToldBuilding = 1});
            modelBuilder.Entity<Campaign>().HasData(listCampaigns);
        }
    }
}
