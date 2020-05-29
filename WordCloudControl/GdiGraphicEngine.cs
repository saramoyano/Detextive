using System;
using Gma.CodeCloud.Controls.Geometry;
using Windows.Foundation;
using Windows.UI;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas;
using Windows.UI.Text;
using Windows.UI.Xaml.Media;

namespace Gma.CodeCloud.Controls
{
    public class GdiGraphicEngine : IGraphicEngine, IDisposable
    {
        private readonly CanvasDrawingSession m_DSession;

        private readonly int m_MinWordWeight;
        private readonly int m_MaxWordWeight;

        public FontStyle FontStyle { get; set; }
        public FontFamily FontFamily { get; set; }
        
        public Color[] Palette { get; private set; }
        public float MinFontSize { get; set; }
        public float MaxFontSize { get; set; }

        public GdiGraphicEngine(CanvasDrawingSession session, FontStyle fontStyle, FontFamily fontFamily, Color[] palette, float minFontSize, float maxFontSize, int minWordWeight, int maxWordWeight)
        {
            m_MinWordWeight = minWordWeight;
            m_MaxWordWeight = maxWordWeight;
            m_DSession = session;
            FontStyle = fontStyle;
            FontFamily = fontFamily;
            Palette = palette;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
        }

        public Size Measure(string text, int weight)
        {
            CanvasTextFormat fontFormat = GetFont(weight);

            CanvasTextLayout textLayout = new CanvasTextLayout(m_DSession, text, fontFormat, 0.0f, 0.0f);

            Size retSize = new Size(textLayout.LayoutBounds.Height, textLayout.LayoutBounds.Width);

            textLayout.Dispose();
            fontFormat.Dispose();

            return retSize;
        }

        public void Draw(LayoutItem layoutItem)
        {
            CanvasTextFormat fontFormat = GetFont(layoutItem.Word.Occurrences);
            Color color = GetPresudoRandomColorFromPalette(layoutItem);

            m_DSession.DrawText(layoutItem.Word.Text, layoutItem.Rectangle, color, fontFormat);

            fontFormat.Dispose();
        }

        public void DrawEmphasized(LayoutItem layoutItem)
        {
            CanvasTextFormat fontFormat = GetFont(layoutItem.Word.Occurrences);            
            Color color = GetPresudoRandomColorFromPalette(layoutItem);
            m_DSession.DrawText(layoutItem.Word.Text, layoutItem.Rectangle, Colors.LightGray, fontFormat);

            int offset = (int)(5 * fontFormat.FontSize/ MaxFontSize)+1;
            Rect smallerRect = new Rect((layoutItem.Rectangle.X + offset),(layoutItem.Rectangle.Y + offset),layoutItem.Rectangle.Width - (2* offset),layoutItem.Rectangle.Height - (2 * offset));

            m_DSession.DrawText(layoutItem.Word.Text, smallerRect, color, fontFormat);

            fontFormat.Dispose();
        }

        private CanvasTextFormat GetFont(int weight)
        {
            CanvasTextFormat txtformat = new CanvasTextFormat();

            if(weight <= m_MinWordWeight)
            {
                weight = m_MinWordWeight + 1;
            }

            float fontSize = (float)(weight - m_MinWordWeight) / (m_MaxWordWeight - m_MinWordWeight) * (MaxFontSize - MinFontSize) + MinFontSize;

            txtformat.FontFamily = this.FontFamily.Source;
            txtformat.FontStyle  = this.FontStyle;
            txtformat.FontSize   = fontSize;
           
            return txtformat;
        }

        private Color GetPresudoRandomColorFromPalette(LayoutItem layoutItem)
        {
            Color color = Palette[layoutItem.Word.Occurrences * layoutItem.Word.Text.Length % Palette.Length];
            return color;
        }

        public void Dispose()
        {
           m_DSession.Dispose();
        }
    }
}
