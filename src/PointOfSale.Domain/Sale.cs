using System.Collections.Generic;

namespace PointOfSale.Domain
{
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