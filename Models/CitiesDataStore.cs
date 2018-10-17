using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebAPI.Models
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<City> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<City>()
            {
                new City()
                {
                    Id = 1,
                    Name = "Atlanta",
                    Description = "It hot",
                    PointOfInterests = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Id = 1, 
                            Name = "Piedmont Park",
                            Description = "A cool park"
                        },
                        new PointOfInterest()
                        {
                            Id = 2,
                            Name = "Aquarium",
                            Description = "Lots of water"
                        }
                    }
                },
                new City()
                {
                    Id = 2,
                    Name = "Chicago",
                    Description = "It's windy",
                     PointOfInterests = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Id = 3,
                            Name = "Dummy Data",
                            Description = "A cool park"
                        },
                        new PointOfInterest()
                        {
                            Id = 4,
                            Name = "Windy Buildings",
                            Description = "It's windy here"
                        },
                        new PointOfInterest()
                        {
                            Id = 5,
                            Name = "Some Bean Thing",
                            Description = "Bean shaped I guess?"
                        }
                    }
                },
                new City()
                {
                    Id = 3,
                    Name = "New York",
                    Description = "It's big.",
                    PointOfInterests = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Id = 6,
                            Name = "Big Park",
                            Description = "A large park"
                        },
                        new PointOfInterest()
                        {
                            Id = 7,
                            Name = "Times Square",
                            Description = "A large square"
                        },
                        new PointOfInterest()
                        {
                            Id = 8,
                            Name = "Twin Towers",
                            Description = "Dummy description"
                        }
                    }
                }
            };

        }
    }
}
