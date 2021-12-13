# BarCodeGenerator

I was recently asked by someone migrating a report from SSRS to Power BI how to display barcodes in a Power BI Report.  In SSRS the way this is typically acheived is by installing a custom font on the SSRS server and leveraging the font to display the codes.  While this approach works well in SSRS, it requires you to have control of the server where the reports run.   Since Power BI is a cloud service, you don't have the ability to install your own fonts.  Therefore, in order to acheive a similar result in Power BI it requires you to get a little more creative.

As I see it there are really 3 key ways to acheive this capability:
1. Generate the bar codes in a custom visual.  While this approach will work, building and publishing a custom visual is a fairly sophisticated undertaking.  You can find more about this here:  https://docs.microsoft.com/en-us/power-bi/developer/visuals/develop-circle-card#:~:text=Tutorial%3A%20Develop%20a%20Power%20BI%20circle%20card%20visual,6%20Configure%20the%20visual%20to%20consume%20data.%20
1. Pre generate the barcodes as images and use them as "ImageURL" fields in the model.  While this procedure works, it requires you to proactively generate the image files and store them, which implies a level of coorination with upstream applications that generate the data used in your report. 
1. Generate an image file dynamically using data already in your model.  This has the benefit of not requiring pre generation of images and also not requiring the complexity of a custom visual.

For the purposes of this example I opted for the 3rd approach.  To acheive this I built a simple Azure Function that leverages BarcodeLib (www.barcodelib.com) to take an input value, generate a barcode, and return the results as a byte array.  To leverage this function I create a custom field in the Power BI model where I concatenate the url to the published function with the value to encode as a barcode.  This field is marked as an "Image URL" in the model.

The included solution includes the folloiwng:
* The Azure Function module that leverages BarcodeLib to generate and return the barcode as an image.
* A simple pbix file illustrating how to generate the custom column that calls the azure function.
