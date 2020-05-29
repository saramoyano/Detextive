using Gma.CodeCloud.Controls;
using Gma.CodeCloud.Controls.Geometry;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace WordCloudControl
{
    public partial class CloudControl : UserControl
    {
        private IEnumerable<IWord> m_Words;
        readonly Color[] m_DefaultPalette = new[] { Colors.DarkRed, Colors.DarkBlue, Colors.DarkGreen, Colors.Navy, Colors.DarkCyan, Colors.DarkOrange, Colors.DarkGoldenrod, Colors.DarkKhaki, Colors.Blue, Colors.Red, Colors.Green };
        private Color[] m_Palette;
        private LayoutType m_LayoutType;

        private int m_MaxFontSize;
        private int m_MinFontSize;
        private ILayout m_Layout;
        private Color m_BackColor;
        private LayoutItem m_ItemUderMouse;
        private int m_MinWordWeight;
        private int m_MaxWordWeight;

        private CanvasRenderTarget _DrawingCanvas = null;

        public CloudControl()
        {
            this.InitializeComponent();

            m_MinWordWeight = 0;
            m_MaxWordWeight = 0;

            MaxFontSize = 68;
            MinFontSize = 10;

            m_Palette = m_DefaultPalette;
            m_BackColor = Colors.White;
            m_LayoutType = LayoutType.Spiral;

            try {
                CanvasDevice device = CanvasDevice.GetSharedDevice();

                //Size restrictions descriped in : http://microsoft.github.io/Win2D/html/P_Microsoft_Graphics_Canvas_CanvasDevice_MaximumBitmapSizeInPixels.htm 

                float useHeight = (float)Win2DCanvas.ActualHeight > device.MaximumBitmapSizeInPixels ? device.MaximumBitmapSizeInPixels : (float)Win2DCanvas.ActualHeight;
                float useWidth = (float)Win2DCanvas.ActualWidth > device.MaximumBitmapSizeInPixels ? device.MaximumBitmapSizeInPixels : (float)Win2DCanvas.ActualWidth;

                _DrawingCanvas = new CanvasRenderTarget(device, useWidth, useHeight, 96);
            }catch (Exception ex)
            {
                Debug.WriteLine("CloudControl constructor exception : " + ex.Message);
            }
        }

        private void canvasGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CanvasDevice device = CanvasDevice.GetSharedDevice();

            //Size restrictions descriped in : http://microsoft.github.io/Win2D/html/P_Microsoft_Graphics_Canvas_CanvasDevice_MaximumBitmapSizeInPixels.htm 
            float useHeight = (float)Win2DCanvas.ActualHeight > device.MaximumBitmapSizeInPixels ? device.MaximumBitmapSizeInPixels : (float)Win2DCanvas.ActualHeight;
            float useWidth = (float)Win2DCanvas.ActualWidth > device.MaximumBitmapSizeInPixels ? device.MaximumBitmapSizeInPixels : (float)Win2DCanvas.ActualWidth;

            _DrawingCanvas = new CanvasRenderTarget(device, useWidth, useHeight, 96);

            BuildLayout();
            PaintWords();
        }

        private void Win2DCanvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            if(_DrawingCanvas != null)
            {
                args.DrawingSession.DrawImage(_DrawingCanvas, new Rect(new Point(0, 0), _DrawingCanvas.Size), new Rect(new Point(0, 0), _DrawingCanvas.Size));
            }
        }

        protected void Invalidate()
        {
            //todo, anything else needed here ?
            PaintWords();
        }

        protected void PaintWords()
        {
            if (m_Words == null) { return; }
            if (m_Layout == null) { return; }

            IEnumerable<LayoutItem> wordsToRedraw = m_Layout.GetWordsInArea(new Rect(0,0,this.ActualWidth,this.ActualHeight));

            using (CanvasDrawingSession ds = _DrawingCanvas.CreateDrawingSession())
            {
                ds.Clear(m_BackColor);

                using (IGraphicEngine graphicEngine = new GdiGraphicEngine(ds, FontStyle, FontFamily, m_Palette, MinFontSize, MaxFontSize, m_MinWordWeight, m_MaxWordWeight))
                {
                    foreach (LayoutItem currentItem in wordsToRedraw)
                    {
                        String hummaa=  currentItem.Word.Text;

                        if (m_ItemUderMouse == currentItem)
                        {
                            graphicEngine.DrawEmphasized(currentItem);
                        }
                        else
                        {
                            graphicEngine.Draw(currentItem);
                        }
                    }
                }
            }

            Win2DCanvas.Invalidate();
        }

        private void BuildLayout()
        {
            if (m_Words == null) { return; }

            using (CanvasDrawingSession ds = _DrawingCanvas.CreateDrawingSession())
            {
                IGraphicEngine graphicEngine = new GdiGraphicEngine(ds, FontStyle, FontFamily, m_Palette, MinFontSize, MaxFontSize, m_MinWordWeight, m_MaxWordWeight);
                
                m_Layout = LayoutFactory.CrateLayout(m_LayoutType, new Size(this.ActualWidth,this.ActualHeight));
                m_Layout.Arrange(m_Words, graphicEngine);
            }
        }

        public LayoutType LayoutType
        {
            get { return m_LayoutType; }
            set
            {
                if (value == m_LayoutType)
                {
                    return;
                }

                m_LayoutType = value;
                BuildLayout();
                Invalidate();
            }
        }

        private void Win2DCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            LayoutItem nextItemUnderMouse;
            Point mousePositionRelativeToControl = e.GetCurrentPoint(Win2DCanvas).Position;            
            this.TryGetItemAtLocation(mousePositionRelativeToControl, out nextItemUnderMouse);
            if (nextItemUnderMouse != m_ItemUderMouse)
            {
                if (nextItemUnderMouse != null)
                {
                    this.Invalidate();
                }
                if (m_ItemUderMouse != null)
                {
                    this.Invalidate();
                }
                m_ItemUderMouse = nextItemUnderMouse;
            }
        }
        private static Rect RectangleGrow(Rect original, int growByPixels)
        {
            return new Rect(
                (int)(original.X - growByPixels),
                (int)(original.Y - growByPixels),
                (int)(original.Width + growByPixels + 1),
                (int)(original.Height + growByPixels + 1));
        }


        public Color BackColor
        {
            get
            {
                return m_BackColor;
            }
            set
            {
                if (m_BackColor == value)
                {
                    return;
                }
                m_BackColor = value;
                Invalidate();
            }
        }

        public int MaxFontSize
        {
            get { return m_MaxFontSize; }
            set
            {
                m_MaxFontSize = value;
                BuildLayout();
                Invalidate();
            }
        }

        public int MinFontSize
        {
            get { return m_MinFontSize; }
            set
            {
                m_MinFontSize = value;
                BuildLayout();
                Invalidate();
            }
        }

        public Color[] Palette
        {
            get { return m_Palette; }
            set
            {
                m_Palette = value;
                BuildLayout();
                Invalidate();
            }
        }

        public IEnumerable<IWord> WeightedWords
        {
            get { return m_Words; }
            set
            {
                m_Words = value;
                if (value == null) { return; }

                IWord first = m_Words.First();
                if (first != null)
                {
                    m_MaxWordWeight = first.Occurrences;
                    m_MinWordWeight = m_Words.Last().Occurrences;
                }

                BuildLayout();
                Invalidate();
            }
        }

        public IEnumerable<LayoutItem> GetItemsInArea(Rect area)
        {
            if (m_Layout == null)
            {
                return new LayoutItem[] { };
            }

            return m_Layout.GetWordsInArea(area);
        }

        public bool TryGetItemAtLocation(Point location, out LayoutItem foundItem)
        {
            foundItem = null;
            IEnumerable<LayoutItem> itemsInArea = GetItemsInArea(new Rect(location, new Size(0, 0)));
            foreach (LayoutItem item in itemsInArea)
            {
                foundItem = item;
                return true;
            }
            return false;
        }  
    }
}

