using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using AODL.Document.Content.Tables;
using AODL.Document.Content.Text;
using AODL.Document.Styles;
using AODL.Document.TextDocuments;
using AODL.Document.Content;
using AODL.Document.Content.Text.Indexes;
using AODL.Document.Content.Draw;

using AODL.Document;
using System.Xml;
using System.Runtime.InteropServices;
using System.IO;

class GenerateQR
{
    
     public static void Main()
     {
	QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.L;
        var generator = new QRCodeGenerator();
	var data = generator.CreateQrCode("ST00020|12|Проверка связи", eccLevel);
            int pixelsPerModule = 20;
            string foreground = "#000000";
            string background = "#FFFFFF";

	using (var code = new QRCode(data))
                            {
                                using (var bitmap = code.GetGraphic(pixelsPerModule, foreground, background, true))
                                {
                                    bitmap.Save("qr.png", ImageFormat.Jpeg);
                                }
                            }
	TextDocument document = new TextDocument();
			document.New();
			//Create a standard paragraph using the ParagraphBuilder
			Paragraph paragraph = ParagraphBuilder.CreateStandardTextParagraph(document);


			Frame frame = new Frame(document, "frame1", "graphic1", @".\qr.png");
			frame.ZIndex = "1";
			paragraph.Content.Add(frame);

			//Add some simple text
			paragraph.TextContent.Add(new SimpleText(document, "Hello, world !"));
			//Add the paragraph to the document
			document.Content.Add(paragraph);
			//Save empty
			document.SaveTo("qr.odt"); 

    }
}