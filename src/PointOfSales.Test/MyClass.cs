using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace PointOfSales
{
	[TestFixture]
	public class MyClass
	{
		[Test]
		public void shouldDisplayPriceForProduct(){
			Dictionary<string, string> priceCatalog = new Dictionary<string, string>() {
				{"1212", "123,50 kr"}	
			};

			var display = Substitute.For<Display>();
			var sale = new Sale(display, priceCatalog);
			
			sale.onBarcodeReceived("1212");
			
			display.Received().show("123,50 kr");
		}
		
		[Test]
		public void shouldDisplayErrorWhenNoProduct() {
			Dictionary<string, string> priceCatalog = new Dictionary<string, string>() {
				{"iPhone", "Error: No such product"}	
			};

			var display = Substitute.For<Display>();
			var sale = new Sale(display, priceCatalog);

			sale.onBarcodeReceived("iPhone");
			display.Received().show("Error: No such product");
		}
		
		[Test]
		public void shouldDisplayErrorWhenBarcodeIsEmpty() {
			Dictionary<string, string> priceCatalog = new Dictionary<string, string>() {
				{"", "Error: Barcode is empty, fix your scanner please."}	
			};

			var display = Substitute.For<Display>();
			var sale = new Sale(display, priceCatalog);

			sale.onBarcodeReceived("");
			display.Received().show("Error: Barcode is empty, fix your scanner please.");
		}
		
		[Test]
		public void shouldDisplayPriceWithPrecisionOfTwo() {
			Dictionary<string, string> priceCatalog = new Dictionary<string, string>() {
				{"iPad", "122,00 kr"}	
			};

			var display = Substitute.For<Display>();
			var sale = new Sale(display, priceCatalog);
			
			sale.onBarcodeReceived("iPad");
			display.Received().show("122,00 kr");
		}
	}
	
	public interface Display{
		void show(string message);
	}
	
	public class Sale {
		private Display display;
		private Dictionary<string, string> catalog;
		
		public Sale(Display disp, Dictionary<string, string> cat) {
			this.display = disp;	
			this.catalog = cat;
		}

		public void onBarcodeReceived(string barcode){			
			display.show(catalog[barcode]);
		}
	}
}

