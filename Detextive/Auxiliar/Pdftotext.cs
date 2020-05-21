﻿using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detextive.Auxiliar
{
    class Pdftotext
    {


        public void ExtractTextFromPdf(string path)
        {

            using (var pdfReader = new PdfReader(path))
            using (var pdfDocument = new PdfDocument(pdfReader))
            {

                var stringBuilder = new StringBuilder();


                for (var i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                    stringBuilder.Append(PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i)));
                string[] lines = stringBuilder.ToString().Split('\n');
                foreach (string line in lines)
                {

                }
                //  return stringBuilder.ToString().Replace("\n", "\r\n");
            }
        }



    }
}