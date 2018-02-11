using System.Collections.Generic;

namespace VendingMachine.Models
{
    public class VendingMachineModel
    {
        public List<Item> ItemsStored { get; set; }

        public List<Coins> CoinsStored { get; set; }

        public VendingMachineModel()
        {
            var defaultAmount = 100;

            CoinsStored = new List<Coins>
            {
                new Coins { Name = "£2", Value = 200, NumberInMachine = defaultAmount },
                new Coins { Name = "£1", Value = 100, NumberInMachine = defaultAmount },
                new Coins { Name = "50p", Value = 50, NumberInMachine = defaultAmount },
                new Coins { Name = "20p", Value = 20, NumberInMachine = defaultAmount },
                new Coins { Name = "10p", Value = 10, NumberInMachine = defaultAmount },
                new Coins { Name = "5p", Value = 5, NumberInMachine = defaultAmount },
                new Coins { Name = "2p", Value = 2, NumberInMachine = defaultAmount },
                new Coins { Name = "1p", Value = 1, NumberInMachine = defaultAmount }
            };

            ItemsStored = new List<Item>
            {
                new Item { Name = "Chocolate bar", Value = 69 },
                new Item { Name = "Penny sweet", Value = 1 },
                new Item { Name = "Chewing gum", Value = 39 },
                new Item { Name = "Crisps", Value = 42 },
                new Item { Name = "Cereal bar", Value = 88 },
                new Item { Name = "Fruit bag", Value = 149 },
                new Item { Name = "Cola", Value = 109 },
                new Item { Name = "Water", Value = 99 },
                new Item { Name = "Small gold ingot", Value = 32329 }
            };
        }
    }
}