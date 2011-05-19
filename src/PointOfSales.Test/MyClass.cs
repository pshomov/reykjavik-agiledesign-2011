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
			Dictionary<string, float> priceCatalog = new Dictionary<string, float>() {
				{"1212", 123.5f}	
			};

			var display = Substitute.For<Display>();
			var sale = new Sale(display, priceCatalog);
			
			sale.onBarcodeReceived("1212");
			
			display.Received().show("123.50 kr");
		}
		
		[Test]
		public void shouldDisplayErrorWhenNoProduct() {
			Dictionary<string, float> priceCatalog = new Dictionary<string, float>() {
			};

			var display = Substitute.For<Display>();
			var sale = new Sale(display, priceCatalog);

			sale.onBarcodeReceived("iPhone");
			display.Received().show("Error: No such product");
		}
		
		[Test]
		public void shouldDisplayErrorWhenBarcodeIsEmpty() {
			Dictionary<string, float> priceCatalog = new Dictionary<string, float>() {
			};

			var display = Substitute.For<Display>();
			var sale = new Sale(display, priceCatalog);

			sale.onBarcodeReceived("");
			display.Received().show("Error: Barcode is empty, fix your scanner please.");
		}
		
		[Test]
		public void shouldDisplayPriceWithPrecisionOfTwo() {
			Dictionary<string, float> priceCatalog = new Dictionary<string, float>() {
				{"iPad", 122}
			};

			var display = Substitute.For<Display>();
			var sale = new Sale(display, priceCatalog);
			
			sale.onBarcodeReceived("iPad");
			display.Received().show("122.00 kr");
		}
	}
	
	public interface Display{
		void show(string message);
	}
	
	public class Sale {
		private Display display;
		private Dictionary<string, float> catalog;
		
		public Sale(Display disp, Dictionary<string, float> cat) {
			this.display = disp;	
			this.catalog = cat;
		}

		public void onBarcodeReceived(string barcode){			
			if (barcode == "") 
				display.show("Error: Barcode is empty, fix your scanner please.");
			else
				try{
				display.show(string.Format("{0:F2} kr", catalog[barcode]));
			} catch (KeyNotFoundException){
				display.show("Error: No such product");
			}
		}
	}
}

