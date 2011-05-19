using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using PointOfSale.Domain;

namespace PointOfSales
{
	[TestFixture]
	public class SaleReceivesBarcode
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
}

