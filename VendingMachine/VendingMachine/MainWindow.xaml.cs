using System;
using System.Linq;
using System.Windows;
using VendingMachine.Models;

namespace VendingMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Setup

        private string itemSelected = "";
        private int amountOutstanding = 0;
        private int changeToGive = 0;
        private VendingMachineModel vendingMachine = new VendingMachineModel();

        public MainWindow()
        {
            InitializeComponent();

            Item1Text.Text = ItemText(vendingMachine.ItemsStored[0]);
            Item2Text.Text = ItemText(vendingMachine.ItemsStored[1]);
            Item3Text.Text = ItemText(vendingMachine.ItemsStored[2]);
            Item4Text.Text = ItemText(vendingMachine.ItemsStored[3]);
            Item5Text.Text = ItemText(vendingMachine.ItemsStored[4]);
            Item6Text.Text = ItemText(vendingMachine.ItemsStored[5]);
            Item7Text.Text = ItemText(vendingMachine.ItemsStored[6]);
            Item8Text.Text = ItemText(vendingMachine.ItemsStored[7]);
            Item9Text.Text = ItemText(vendingMachine.ItemsStored[8]);

            Coin1Text.Text = vendingMachine.CoinsStored[0].Name;
            Coin2Text.Text = vendingMachine.CoinsStored[1].Name;
            Coin3Text.Text = vendingMachine.CoinsStored[2].Name;
            Coin4Text.Text = vendingMachine.CoinsStored[3].Name;
            Coin5Text.Text = vendingMachine.CoinsStored[4].Name;
            Coin6Text.Text = vendingMachine.CoinsStored[5].Name;
            Coin7Text.Text = vendingMachine.CoinsStored[6].Name;
            Coin8Text.Text = vendingMachine.CoinsStored[7].Name;
        }

        private string ItemText(Item item)
        {
            return item.Name + "\n \n" + BalanceFormat(item.Value);
        }

        #endregion Setup

        #region Click Events

        private void Item1_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[0]);
        }

        private void Item2_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[1]);
        }

        private void Item3_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[2]);
        }

        private void Item4_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[3]);
        }

        private void Item5_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[4]);
        }

        private void Item6_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[5]);
        }

        private void Item7_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[6]);
        }

        private void Item8_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[7]);
        }

        private void Item9_Click(object sender, RoutedEventArgs e)
        {
            SelectItem(vendingMachine.ItemsStored[8]);
        }

        private void Coin1_Click(object sender, RoutedEventArgs e)
        {
            InsertCoin(vendingMachine.CoinsStored[0]);
        }

        private void Coin2_Click(object sender, RoutedEventArgs e)
        {
            InsertCoin(vendingMachine.CoinsStored[1]);
        }

        private void Coin3_Click(object sender, RoutedEventArgs e)
        {
            InsertCoin(vendingMachine.CoinsStored[2]);
        }

        private void Coin4_Click(object sender, RoutedEventArgs e)
        {
            InsertCoin(vendingMachine.CoinsStored[3]);
        }

        private void Coin5_Click(object sender, RoutedEventArgs e)
        {
            InsertCoin(vendingMachine.CoinsStored[4]);
        }

        private void Coin6_Click(object sender, RoutedEventArgs e)
        {
            InsertCoin(vendingMachine.CoinsStored[5]);
        }

        private void Coin7_Click(object sender, RoutedEventArgs e)
        {
            InsertCoin(vendingMachine.CoinsStored[6]);
        }

        private void Coin8_Click(object sender, RoutedEventArgs e)
        {
            InsertCoin(vendingMachine.CoinsStored[7]);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Balance.Text = "";
            Change.Text = "";
            Chute.Text = "";
        }

        #endregion Click Events

        #region Functions

        private void BalanceDisplay(int amount)
        {
            var message = BalanceFormat(amount);

            Balance.Text = message;
        }

        private string BalanceFormat(int amount)
        {
            decimal amountInPounds = (decimal)amount / 100;
            return amountInPounds.ToString("c2");
        }

        private void DispenseChange()
        {
            foreach (var coinType in vendingMachine.CoinsStored.OrderByDescending(c => c.Value))
            {
                int count = changeToGive / coinType.Value;
                if (count > 0)
                {
                    if (count <= coinType.NumberInMachine)
                    {
                        changeToGive = changeToGive - (count * coinType.Value);
                        coinType.NumberInMachine = coinType.NumberInMachine - count;
                        Change.Text = Change.Text + PrintChange(count, coinType.Name);
                    }
                    else if (coinType.NumberInMachine > 0)
                    {
                        changeToGive = changeToGive - (coinType.NumberInMachine * coinType.Value);
                        Change.Text = Change.Text + PrintChange(coinType.NumberInMachine, coinType.Name);
                        coinType.NumberInMachine = 0;
                    }
                }
            }

            if (changeToGive > 0)
            {
                Change.Text = Change.Text + "Ran out of change. Please contact maintenance.";
            }

            Chute.Text = itemSelected;
            Balance.Text = "";
        }

        private string PrintChange(int count, string name)
        {
            var print = string.Format("{0} {1} coin", count, name);
            if (count > 1)
            {
                print = print + "s";
            }

            print = print + "\n";

            return print;
        }

        private void InsertCoin(Coin coin)
        {
            StoreCoin(coin);

            if (amountOutstanding <= 0)
            {
                Balance.Text = "Error";
            }
            else
            {
                amountOutstanding = amountOutstanding - coin.Value;
                BalanceDisplay(amountOutstanding);

                if (amountOutstanding < 0)
                {
                    changeToGive = Math.Abs(amountOutstanding);
                    DispenseChange();
                }
            }
        }

        private void SelectItem(Item item)
        {
            itemSelected = item.Name;
            Change.Text = "";
            Chute.Text = "";
            amountOutstanding = item.Value;
            BalanceDisplay(item.Value);
        }

        private void StoreCoin(Coin coin)
        {
            vendingMachine.CoinsStored.Single(c => c.Name == coin.Name).NumberInMachine++;
        }

        #endregion Functions
    }
}