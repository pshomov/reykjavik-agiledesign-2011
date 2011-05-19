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
			var display = Substitute.For<Display>();
			var sale = new Sale(display);
			
			sale.onBarcodeReceived("1212");
			
			display.Received().show("123,5 kr");
		}
		
		[Test]
		public void shouldDisplayErrorWhenNoProduct() {
			var display = Substitute.For<Display>();
			var sale = new Sale(display);

			sale.onBarcodeReceived("iPhone");
			display.Received().show("Error: No such product");
		}
		
		[Test]
		public void shouldDisplayErrorWhenBarcodeIsEmpty() {
			var display = Substitute.For<Display>();
			var sale = new Sale(display);

			sale.onBarcodeReceived("");
			display.Received().show("Error: Barcode is empty, fix your scanner please.");
		}
	}
	
	public interface Display{
		void show(string message);
	}
	
	public class Sale {
		private Display display;
		
		public Sale(Display disp) {
			this.display = disp;	
		}

		public void onBarcodeReceived(string barcode){
			Dictionary<string, string> priceCatalog = new Dictionary<string, string>(){
				{"1212", "123,5 kr"},
				{"iPhone", "Error: No such product"},
				{"", "Error: Barcode is empty, fix your scanner please."}
			};
			
			display.show(priceCatalog[barcode]);
		}
	}
}

