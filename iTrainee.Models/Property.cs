using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Models
{
    public class Property : Base
    {
        public string Header { get; set; }
        public string Image { get; set; }
        public bool NewItem { get; set; }
        public Property[] Items { get; set; }
        public static Property[] GetData()
        {
            return new Property[]
            {
                new Property
                {
                    Header = "Electronics",
                    Items = new Property[]
                    {
                        new Property { Header="Trimmers/Shavers" },
                        new Property { Header="Tablets" },
                        new Property { Header="Phones",
                            Items = new Property[] {
                                new Property { Header="Apple" },
                                new Property { Header="Motorola", NewItem=true },
                                new Property { Header="Nokia" },
                                new Property { Header="Samsung" }}
                        },
                        new Property { Header="Speakers", NewItem=true },
                        new Property { Header="Monitors" }
                    }
                },
                new Property{
                    Header = "Toys",
                    Items = new Property[]{
                        new Property{ Header = "Shopkins" },
                        new Property{ Header = "Train Sets" },
                        new Property{ Header = "Science Kit", NewItem = true },
                        new Property{ Header = "Play-Doh" },
                        new Property{ Header = "Crayola" }
                    }
                },
                new Property{
                    Header = "Home",
                    Items = new Property[] {
                        new Property{ Header = "Coffeee Maker" },
                        new Property{ Header = "Breadmaker", NewItem = true },
                        new Property{ Header = "Solar Panel", NewItem = true },
                        new Property{ Header = "Work Table" },
                        new Property{ Header = "Propane Grill" }
                    }
                }
            };
        }
    }
}
