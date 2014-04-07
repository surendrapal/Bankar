using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BM.Common.PDF
{

    public class ITextEvents : PdfPageEventHelper
    {

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;


        #region Fields
        private PdfPCell _header;
        private PdfPCell _footer;
        #endregion

        #region Properties
        public PdfPCell Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public PdfPCell Footer
        {
            get { return _footer; }
            set { _footer = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
                //handle exception here
            }
            catch (System.IO.IOException ioe)
            {
                //handle exception here
            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            
            //Create PdfTable object
            PdfPTable headerTable = new PdfPTable(1);
            PdfPTable footerTable = new PdfPTable(1);
            String text = "Page " + writer.PageNumber + " of ";
            headerTable.AddCell(_header);
            footerTable.AddCell(_footer);
            //Add paging to header
            {
                //cb.BeginText();
                //cb.SetFontAndSize(bf, 12);
                //cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
                //cb.ShowText(text);
                //cb.EndText();
                //float len = bf.GetWidthPoint(text, 12);
                ////Adds "12" in Page 1 of 12
                //cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));
            }
            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(100), document.PageSize.GetBottom(10));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(100) + len, document.PageSize.GetBottom(10));
            }

            headerTable.TotalWidth = document.PageSize.Width-80;
            headerTable.WidthPercentage = 100;            
            headerTable.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);

            footerTable.TotalWidth = document.PageSize.Width - 80;
            footerTable.WidthPercentage = 100;
            footerTable.WriteSelectedRows(0, -1, 40, document.PageSize.GetBottom(50), writer.DirectContent);

            

            
            //Move the pointer and draw line to separate header section from rest of page
            cb.MoveTo(40, document.PageSize.Height - 100);
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(50));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            //headerTemplate.BeginText();
            //headerTemplate.SetFontAndSize(bf, 12);
            //headerTemplate.SetTextMatrix(0, 0);
            //headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            //headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();


        }
    }
}