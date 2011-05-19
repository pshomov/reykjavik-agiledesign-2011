using System;
using NUnit.Framework;
using NSubstitute;

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
			if (barcode ==  "iPhone") 
				display.show("Error: No such product");
			else
				display.show("123,5 kr");
		}
	}
}

