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
        public void shouldDisplayPriceForProduct()
        {
            var productCatalog = Substitute.For<ProductCatalog>();
            var display = Substitute.For<Display>();
            productCatalog.lookupPriceForBarcode("1212").Returns(123.5f);
            var sale = new Sale(display, productCatalog);

            sale.onBarcodeReceived("1212");

            display.Received().show("123.50 kr");
        }

        [Test]
        public void shouldDisplayErrorWhenNoProduct()
        {
            var productCatalog = Substitute.For<ProductCatalog>();
            productCatalog.WhenForAnyArgs(catalog => catalog.lookupPriceForBarcode("")).Do(
                info => { throw new ProductNotFoundException(); });
            var display = Substitute.For<Display>();
            var sale = new Sale(display, productCatalog);

            sale.onBarcodeReceived("iPhone");
            display.Received().show("Error: No such product");
        }

        [Test]
        public void shouldDisplayErrorWhenBarcodeIsEmpty()
        {
            var productCatalog = Substitute.For<ProductCatalog>();

            var display = Substitute.For<Display>();
            var sale = new Sale(display, productCatalog);

            sale.onBarcodeReceived("");
            display.Received().show("Error: Barcode is empty, fix your scanner please.");
        }

        [Test]
        public void shouldDisplayPriceWithPrecisionOfTwo()
        {
            var productCatalog = Substitute.For<ProductCatalog>();

            productCatalog.lookupPriceForBarcode("iPad").Returns(122);

            var display = Substitute.For<Display>();
            var sale = new Sale(display, productCatalog);

            sale.onBarcodeReceived("iPad");
            display.Received().show("122.00 kr");
        }
    }
}