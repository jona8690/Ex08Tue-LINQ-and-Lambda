﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Examples {
	class Program {
		#region Class Definitions
		public class Customer {
			public string First { get; set; }
			public string Last { get; set; }
			public string State { get; set; }
			public double Price { get; set; }
			public string[] Purchases { get; set; }

			public override string ToString() {
				return this.First + " " + this.Last + " ("+ this.State +") - Price: " + this.Price;
			}
		}

		public class Distributor {
			public string Name { get; set; }
			public string State { get; set; }
		}

		public class CustDist {
			public string custName { get; set; }
			public string distName { get; set; }
		}

		public class CustDistGroup {
			public string custName { get; set; }
			public IEnumerable<string> distName { get; set; }
		}
		#endregion

		#region Create data sources

		static List<Customer> customers = new List<Customer>
		{
			new Customer {First = "Cailin", Last = "Alford", State = "GA", Price = 930.00, Purchases = new string[] {"Panel 625", "Panel 200"}},
			new Customer {First = "Theodore", Last = "Brock", State = "AR", Price = 2100.00, Purchases = new string[] {"12V Li"}},
			new Customer {First = "Jerry", Last = "Gill", State = "MI", Price = 585.80, Purchases = new string[] {"Bulb 23W", "Panel 625"}},
			new Customer {First = "Owens", Last = "Howell", State = "GA", Price = 512.00, Purchases = new string[] {"Panel 200", "Panel 180"}},
			new Customer {First = "Adena", Last = "Jenkins", State = "OR", Price = 2267.80, Purchases = new string[] {"Bulb 23W", "12V Li", "Panel 180"}},
			new Customer {First = "Medge", Last = "Ratliff", State = "GA", Price = 1034.00, Purchases = new string[] {"Panel 625"}},
			new Customer {First = "Sydney", Last = "Bartlett", State = "OR", Price = 2105.00, Purchases = new string[] {"12V Li", "AA NiMH"}},
			new Customer {First = "Malik", Last = "Faulkner", State = "MI", Price = 167.80, Purchases = new string[] {"Bulb 23W", "Panel 180"}},
			new Customer {First = "Serena", Last = "Malone", State = "GA", Price = 512.00, Purchases = new string[] {"Panel 180", "Panel 200"}},
			new Customer {First = "Hadley", Last = "Sosa", State = "OR", Price = 590.20, Purchases = new string[] {"Panel 625", "Bulb 23W", "Bulb 9W"}},
			new Customer {First = "Nash", Last = "Vasquez", State = "OR", Price = 10.20, Purchases = new string[] {"Bulb 23W", "Bulb 9W"}},
			new Customer {First = "Joshua", Last = "Delaney", State = "WA", Price = 350.00, Purchases = new string[] {"Panel 200"}}
		};

		static List<Distributor> distributors = new List<Distributor>
		{
			new Distributor {Name = "Edgepulse", State = "UT"},
			new Distributor {Name = "Jabbersphere", State = "GA"},
			new Distributor {Name = "Quaxo", State = "FL"},
			new Distributor {Name = "Yakijo", State = "OR"},
			new Distributor {Name = "Scaboo", State = "GA"},
			new Distributor {Name = "Innojam", State = "WA"},
			new Distributor {Name = "Edgetag", State = "WA"},
			new Distributor {Name = "Leexo", State = "HI"},
			new Distributor {Name = "Abata", State = "OR"},
			new Distributor {Name = "Vidoo", State = "TX"}
		};

		static double[] exchange = { 0.89, 0.65, 120.29 };

		static double[] ExchangedPrices = {827.70, 604.50, 111869.70,
										1869.00, 1,365.00, 252609.00,
										521.36, 380.77, 70465.88,
										455.68, 332.80, 61588.48,
										2018.34, 1474.07, 272793.66,
										920.26, 672.10, 124379.86,
										1873.45, 1368.25, 253210.45,
										149.34, 109.07, 20184.66,
										455.68, 332.80, 61588.48,
										525.28, 383.63, 70995.16,
										9.08, 6.63, 1226.96,
										311.50, 227.50, 42101.50};

		static string[] Purchases = {  "Panel 625", "Panel 200",
									"12V Li",
									"Bulb 23W", "Panel 625",
									"Panel 200", "Panel 180",
									"Bulb 23W", "12V Li", "Panel 180",
									"Panel 625",
									"12V Li", "AA NiMH",
									"Bulb 23W", "Panel 180",
									"Panel 180", "Panel 200",
									"Panel 625", "Bulb 23W", "Bulb 9W",
									"Bulb 23W", "Bulb 9W",
									"Panel 200"
								 };
		#endregion

		public class CustomAlphabeticalSorter : IComparer<string> {
			public int Compare(string X, string Y) {
				return string.Compare(X, Y);
			}
		}

		static void Main(string[] args) {
			Program p = new Program();
			p.Run();
		}

		private void Run() {
			bool Running = true;
			while (Running) {
				Console.Clear();
				Console.WriteLine("Lambda Syntax\n");
				Console.WriteLine("1. Ex 01 - using Where");
				Console.WriteLine("2. Ex 02 - using Select");
				Console.WriteLine("3. Ex 03");
				Console.WriteLine("4. Ex 04");
				Console.WriteLine("5. Ex 05");
				Console.WriteLine("\n0. Exit");

				int Menu;
				while (!int.TryParse(Console.ReadLine(), out Menu)) {
					Console.WriteLine("Please enter valid menu key!");
				}

				switch (Menu) {
					case 0: Running = false; break;
					case 1: Ex01(); break;
					case 2: Ex02(); break;
					case 3: Ex03(); break;
					case 4: Ex04(); break;
					case 5: Ex05(); break;
				}
			}
		}

		private void Ex01() {
			Console.Clear();
			Console.WriteLine("Exercise 1:");

			IEnumerable<double> expriceless1000 =
				from e in ExchangedPrices
				where e < 1000
				select e;

			Console.WriteLine("\nExchange Prices less 1000");
			foreach (double e in expriceless1000) {
				Console.WriteLine(e);
			}

			IEnumerable<Customer> gaCustomers =
				from c in customers
				where c.State == "GA"
				select c;

			Console.WriteLine("\nCustomers from Georgia");
			foreach (Customer c in gaCustomers) {
				Console.WriteLine("Customer: {0} {1}", c.First, c.Last);
				foreach (string purchase in c.Purchases) {
					Console.WriteLine(" - " + purchase);
				}
			}

			Console.ReadKey();
		}

		public void Ex02() {
			Console.Clear();
			IEnumerable<string> customerFirstnames =
				from c in customers
				select c.First;

			Console.WriteLine("\nCustomers Firstnames");
			foreach (string c in customerFirstnames) {
				Console.WriteLine(c);
			}

			IEnumerable<string> customerFullnames =
				from c in customers
				select c.First + " " + c.Last; // Hm, alternate way discoverd, yah.

			Console.WriteLine("\nCustomers Fullnames");
			foreach (string s in customerFullnames) {
				Console.WriteLine(s);
			}

			IEnumerable<string> bothStates =
			from c in customers
			from d in distributors
			where c.State == d.State
			select c.State;

			Console.WriteLine("\nCommon States");
			foreach (string s in bothStates) {
				Console.WriteLine(s);
			}

			Console.ReadKey();
		}

		private void Ex03() {
			Console.Clear();

			IEnumerable<Customer> first3Customers =
				customers.Take(3);
			Console.WriteLine("\nFirst 3 Customers:");
			foreach(Customer c in first3Customers) {
				Console.WriteLine(c.ToString());
			}

			IEnumerable<Customer> first3CustomersFromOR =
				customers
				.Where(c => c.State == "OR")
				.Take(3);
			Console.WriteLine("\nFirst 3 Customers from OR:");
			foreach (Customer c in first3CustomersFromOR) {
				Console.WriteLine(c.ToString());
			}

			Console.ReadKey();
		}

		private void Ex04() {
			Console.Clear();
			IEnumerable<Customer> customersAlphabetically =
				from c in customers
				orderby c.First
				select c;

			Console.WriteLine("\nCustomers Alphabetically");
			foreach(Customer c in customersAlphabetically) {
				Console.WriteLine(c.ToString());
			}

			IEnumerable<Customer> customersLngtLstname =
				from c in customers
				orderby c.Last.Length
				select c;
			Console.WriteLine("\nCustomers Length by Lastname");
			foreach (Customer c in customersLngtLstname) {
				Console.WriteLine(c.ToString());
			}

			IEnumerable<Customer> customersPrice =
				from c in customers
				orderby c.Price descending
				select c;
			Console.WriteLine("\nCustomers Price");
			foreach (Customer c in customersPrice) {
				Console.WriteLine(c.ToString());
			}

			IEnumerable<Customer> customerCustom =
				customers.OrderBy(c => c.First.Length)
				.ThenBy(c => c.Last, new CustomAlphabeticalSorter());

			Console.WriteLine("\nCustomers Custom");
			foreach (Customer c in customerCustom) {
				Console.WriteLine(c.ToString());
			}

			Console.ReadKey();
		}

		private void Ex05() {
			Console.Clear();

			var a =
				from c in customers
				group c.First by c.First[0] into g
				select new { Firstletter  = g.Key, Name = g };
			Console.WriteLine("\nCustomers groupbyed by firstname firstletter");
			foreach(var g in a) {
				Console.WriteLine("Customers firstname which start with: {0}", g.Firstletter);
				foreach(var n in g.Name) {
					Console.WriteLine(" - " + n);
				}
			}


			Console.ReadKey();
		}
	}
}
