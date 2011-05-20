using System;
using System.Collections.Generic;

namespace PointOfSale.Domain
{
    public class Sale {
        private Display display;
        private readonly ProductCatalog productCatalog;

        public Sale(Display disp, ProductCatalog productCatalog)
        {
            this.display = disp;
            this.productCatalog = productCatalog;
        }

        public void onBarcodeReceived(string barcode){			
            if (barcode == "") 
                display.show("Error: Barcode is empty, fix your scanner please.");
            else
            {
                float lookupPriceForBarcode;
                try
                {
                    lookupPriceForBarcode = productCatalog.lookupPriceForBarcode(barcode);
                }
                catch (ProductNotFoundException){
                    display.show("Error: No such product");
                    return;
                } catch (Exception){
                    display.show("Error: Something went real bad with the product catalog.");
                    return;
                }
                try
                {
                    display.show(string.Format("{0:F2} kr", lookupPriceForBarcode));
                }
                catch (Exception e)
                {
                    throw new DisplayDriverError(e);
                }
            }
        }
    }
}

public interface ProductCatalog
{
    float lookupPriceForBarcode(string barcode);
}
